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

    [TestCase(typeof(IExample))]
    [TestCase(typeof(ExampleBase))]
    [TestCase(typeof(AbstractExample))]
    public void Constructor_WhenPluginTypeIsAbstractOrInterface_ShouldThrowArgumentException(Type pluginType)
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

    [TestCase(typeof(StructExample))]
    [TestCase(typeof(EnumExample))]
    [TestCase(typeof(RecordStructExample))]
    [TestCase(typeof(int))]
    [TestCase(typeof(decimal))]
    public void Constructor_WhenPluginTypeIsNotInstantiableClass_ShouldThrowArgumentException(Type pluginType)
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
    public void Constructor_WhenPluginTypeIsInstantiableClass_ShouldNotThrowException()
    {
        // Arrange
        Type expectedPluginType = typeof(Example);

        // Act
        var actual = new PluginAttribute(expectedPluginType);

        // Assert
        actual.PluginType.Should().Be(expectedPluginType);
    }
}
