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
        PluginLoader.SetConfiguration(envConfiguration);
        var expected = new[]
        {
            "Version=12.0.0.0",
            "Version=13.0.0.0"
        };

        // Act
        var commands = PluginLoader.Load<ICommand>();
        _ = PluginLoader.Load<ICommand>();
        _ = PluginLoader.Load<IPluginStartup>();
        var versions = commands.Select(command =>
        {
            var json = command.Execute();
            var version = JsonConvert.DeserializeObject<Info>(json).Version;
            return version.Split(",")[1].Trim();
        }).ToList();

        // Asserts
        commands.Should().HaveCount(2);
        PluginLoader.Assemblies.Should().HaveCount(2);
        versions.Should().BeEquivalentTo(expected);
    }
}
