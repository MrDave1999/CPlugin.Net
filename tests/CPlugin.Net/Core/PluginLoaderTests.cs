using CPlugin.Net.Exceptions;

namespace CPlugin.Net.Tests.Core;

public class PluginLoaderTests
{
    private class Info
    {
        public string Version { get; set; }
    }

    [Test]
    public void Load_WhenPluginsAreFound_ShouldBeLoadedIntoMemory()
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
            "Newtonsoft.Json, Version=13.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed",
            "Newtonsoft.Json, Version=12.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed"
        };

        // Act
        PluginLoader.Load(envConfiguration);
        var commands = TypeFinder.FindSubtypesOf<ICommand>();
        var versions = commands.Select(command =>
        {
            var json = command.Execute();
            return JsonConvert.DeserializeObject<Info>(json).Version;
        }).ToArray();

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

    [Test]
    public void LoadPluginsWithDependencies_WhenPluginsAreFound_ShouldBeLoadedIntoMemory()
    {
        // Arrange
        var value =
        """
        TestProject.OldJsonPlugin.dll->TestProject.JsonPlugin.dll
        TestProject.JsonPlugin.dll
        """;
        Environment.SetEnvironmentVariable("PLUGINS", value);
        var envConfiguration = new CPluginEnvConfiguration();
        int expectedAssemblies = 2;

        // Act
        PluginLoader.LoadPluginsWithDependencies(envConfiguration);

        AppDomain
            .CurrentDomain
            .GetAssemblies()
            .Where(assembly => assembly.GetName().Name == "TestProject.OldJsonPlugin" 
                || assembly.GetName().Name == "TestProject.JsonPlugin")
            .Count()
            .Should()
            .Be(expectedAssemblies);
    }

    [Test]
    public void LoadPluginsWithDependencies_WhenDependencyIsNotFound_ShouldThrowPluginNotFoundException()
    {
        // Arrange
        var dependentPlugin = "TestProject.JsonPlugin.dll";
        var missingPlugin = "TestProject.OldJsonPlugin.dll";
        var value = $"{dependentPlugin}->{missingPlugin}";
        Environment.SetEnvironmentVariable("PLUGINS", value);
        var envConfiguration = new CPluginEnvConfiguration();

        // Act
        Action act = () => PluginLoader.LoadPluginsWithDependencies(envConfiguration);

        // Assert
        act.Should()
           .Throw<PluginNotFoundException>()
           .WithMessage($"The plugin '{dependentPlugin}' depends on '{missingPlugin}', but '{missingPlugin}' was not found.");
    }
}
