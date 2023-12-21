namespace Example.Test;

public class PluginLoaderTest
{
    private class GetPluginInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
    }

    [Test]
    public void Load_WhenPluginsAreFound_ShouldBeLoadedIntoMemory()
    {
        // Arrange
        var plugins =
        """
        Example.JsonPlugin.dll
        Example.OldJsonPlugin.dll
        """;
        Environment.SetEnvironmentVariable("PLUGINS", plugins);
        var envConfiguration = new CPluginEnvConfiguration();
        var expectedInfo = new GetPluginInfo[]
        {
            new() 
            { 
                Name = "json", 
                Description = "Outputs JSON value.", 
                Version = "Newtonsoft.Json, Version=13.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed"
            },
            new() 
            { 
                Name = "oldjson", 
                Description = "Outputs JSON value.", 
                Version = "Newtonsoft.Json, Version=12.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed"
            }
        };

        // Act
        PluginLoader.Load(envConfiguration);
        var commands = TypeFinder.FindSubtypesOf<ICommand>();
        var actualInfo = commands.Select(command => new GetPluginInfo
        {
            Name = command.Name,
            Description = command.Description,
            Version = command.Version
        }).ToArray();

        // Assert
        actualInfo.Should().BeEquivalentTo(expectedInfo);
    }
}
