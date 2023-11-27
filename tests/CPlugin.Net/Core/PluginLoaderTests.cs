namespace CPlugin.Net.Tests.Core;

public class PluginLoaderTests
{
    private class Info
    {
        public string Version { get; set; }
    }

    [Test]
    public void Load_WhenPluginsAreLoaded_ShouldReturnsEnumerable()
    {
        // Arrange
        var value =
        """
        TestProject.OldJsonPlugin.dll
        TestProject.JsonPlugin.dll
        """;
        Environment.SetEnvironmentVariable("PLUGINS", value);
        var envConfiguration = new CPluginEnvConfiguration();
        int expectedCommands = 2;
        var expectedVersions = new[]
        {
            "Version=12.0.0.0",
            "Version=13.0.0.0"
        };

        // Act
        PluginLoader.Load(envConfiguration);
        var commands = TypeFinder.FindSubtypesOf<ICommand>();
        var versions = commands.Select(command =>
        {
            var json = command.Execute();
            var version = JsonConvert.DeserializeObject<Info>(json).Version;
            return version.Split(",")[1].Trim();
        }).ToList();

        // Asserts
        commands.Should().HaveCount(expectedCommands);
        versions.Should().BeEquivalentTo(expectedVersions);
    }
}
