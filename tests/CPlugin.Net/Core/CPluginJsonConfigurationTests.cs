namespace CPlugin.Net.Tests.Core;

public class CPluginJsonConfigurationTests
{
    [Test]
    public void GetPluginFiles_WhenPluginFilesArePresent_ShouldReturnsFullPaths()
    {
        // Arrange
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("./Resources/app.json")
            .Build();
        var jsonConfiguration = new CPluginJsonConfiguration(configurationRoot);
        var basePath = AppContext.BaseDirectory;
        var expectedPaths = new[]
        {
            Path.Combine(basePath, "plugins", "TestProject.JsonPlugin", "TestProject.JsonPlugin.dll"),
            Path.Combine(basePath, "plugins", "TestProject.OldJsonPlugin", "TestProject.OldJsonPlugin.dll"),
            Path.Combine(basePath, "plugins", "TestProject.WebPlugin", "TestProject.WebPlugin.dll")
        };

        // Act
        var actual = jsonConfiguration.GetPluginFiles().ToList();

        // Assert
        actual.Should().BeEquivalentTo(expectedPaths);
    }

    [Test]
    public void GetPluginFiles_WhenPluginFilesAreNotPresent_ShouldReturnsEmptyEnumerable()
    {
        // Arrange
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("./Resources/empty.json")
            .Build();
        var jsonConfiguration = new CPluginJsonConfiguration(configurationRoot);

        // Act
        var actual = jsonConfiguration.GetPluginFiles().ToList();

        // Assert
        actual.Should().BeEmpty();
    }

    [Test]
    public void GetPluginFiles_WhenPluginsSectionIsNotPresent_ShouldReturnsEmptyEnumerable()
    {
        // Arrange
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("./Resources/config.json")
            .Build();
        var jsonConfiguration = new CPluginJsonConfiguration(configurationRoot);

        // Act
        var actual = jsonConfiguration.GetPluginFiles().ToList();

        // Assert
        actual.Should().BeEmpty();
    }

    [Test]
    public void GetPluginFiles_WhenPluginFileDoesNotHaveDllExtension_ShouldThrowArgumentException()
    {
        // Arrange
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("./Resources/setting.json")
            .Build();
        var jsonConfiguration = new CPluginJsonConfiguration(configurationRoot);

        // Act
        Action act = () => jsonConfiguration.GetPluginFiles().ToList();

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Constructor_WhenArgumentIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        IConfiguration configuration = default;

        // Act
        Action act = () =>
        {
            var config = new CPluginJsonConfiguration(configuration);
        };

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}
