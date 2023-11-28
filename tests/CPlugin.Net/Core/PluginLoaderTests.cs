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
            // Example: Newtonsoft.Json, Version=12.0.0.0, Culture=neutral, PublicKeyToken=null[0]
            var version = JsonConvert.DeserializeObject<Info>(json).Version;
            // Example: Version=12.0.0.0
            return version.Split(",")[1].Trim();
        }).ToList();

        // Asserts
        commands.Should().HaveCount(expectedCommands);
        versions.Should().BeEquivalentTo(expectedVersions);
    }

    [Test]
    public void Load_WhenArgumentIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        CPluginEnvConfiguration configuration = default;

        // Act
        Action act = () => PluginLoader.Load(configuration);

        // Assert
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(configuration));
    }

    [Test]
    public void Load_WhenMethodIsCalledMultipleTimes_ShouldNotLoadSamePluginsIntoMemory()
    {
        // Arrange
        var value = "TestProject.HelloPlugin.dll";
        Environment.SetEnvironmentVariable("PLUGINS", value);
        var envConfiguration = new CPluginEnvConfiguration();
        int expectedAssemblies = 1;

        // Act
        for (int i = 0; i < 10; i++) 
        {
            // This operation is idempotent.
            PluginLoader.Load(envConfiguration);
        }

        // Assert
        AppDomain
            .CurrentDomain
            .GetAssemblies()
            .Where(assembly => assembly.GetName().Name == "TestProject.HelloPlugin")
            .Count()
            .Should()
            .Be(expectedAssemblies);
    }
}
