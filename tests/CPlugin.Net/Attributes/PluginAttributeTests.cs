namespace CPlugin.Net.Tests.Attributes;

public class PluginAttributeTests
{
    [Test]
    public void Constructor_WhenArgumentIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        Type pluginType = default;

        // Act
        Action act = () =>
        {
            var attr = new PluginAttribute(pluginType);
        };

        // Assert
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(pluginType));
    }

    [TestCaseSource(typeof(PluginAttributeTestCases))]
    public void Constructor_WhenTypeIsNotInstantiable_ShouldThrowArgumentException(Type pluginType)
    {
        // Act
        Action act = () =>
        {
            var attr = new PluginAttribute(pluginType);
        };

        // Assert
        act.Should()
           .Throw<ArgumentException>()
           .WithParameterName(nameof(pluginType));
    }

    [Test]
    public void Constructor_WhenTypeIsInstantiable_ShouldNotThrowException()
    {
        // Arrange
        Type expected = typeof(Example);

        // Act
        var actual = new PluginAttribute(expected);

        // Assert
        actual.PluginType.Should().Be(expected);
    }

    private class Example { }
}
