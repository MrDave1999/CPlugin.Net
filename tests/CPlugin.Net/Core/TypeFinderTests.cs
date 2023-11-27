[assembly: Plugin(typeof(Runner1))]
[assembly: Plugin(typeof(Runner2))]

namespace CPlugin.Net.Tests.Core;

public class ExampleTest { }
public abstract class RunnerBase { }
public interface IRunner 
{ 
    int Run();
}
public class Runner1 : IRunner
{
    public int Run() => 1;
}
public class Runner2 : IRunner
{
    public int Run() => 1;
}


public class TypeFinderTests
{
    [Test]
    public void FindSubtypesOf_WhenSubtypesArePresent_ShouldReturnsInstancesOfSubtypes()
    {
        // Arrange
        var assemblies = new[]
        {
            typeof(TypeFinderTests).Assembly
        };
        int expectedValue = 1;

        // Act
        var runners = TypeFinder.FindSubtypesOf<IRunner>(assemblies);

        // Asserts
        runners.Should().HaveCount(2);
        foreach(IRunner runner in runners)
        {
            int value = runner.Run();
            value.Should().Be(expectedValue);
        }
    }

    [Test]
    public void FindSubtypesOf_WhenSupertypeDoesNotHaveSubtypes_ShouldReturnsEmptyEnumerable()
    {
        // Arrange
        var assemblies = new[]
        {
            typeof(TypeFinderTests).Assembly
        };

        // Act
        var runners = TypeFinder.FindSubtypesOf<RunnerBase>(assemblies);

        // Assert
        runners.Should().BeEmpty();
    }

    [Test]
    public void FindSubtypesOf_WhenThereAreNoAssembliesLoaded_ShouldReturnsEmptyEnumerable()
    {
        // Arrange
        var assemblies = Enumerable.Empty<Assembly>();

        // Act
        var runners = TypeFinder.FindSubtypesOf<IRunner>(assemblies);

        // Assert
        runners.Should().BeEmpty();
    }

    [Test]
    public void FindSubtypesOf_WhenNoAssemblyUsesPluginAttribute_ShouldReturnsEmptyEnumerable()
    {
        // Arrange
        var assemblies = new[]
        {
            typeof(TestProject.PluginHost.Employee).Assembly,
            typeof(TestProject.Contracts.ICommand).Assembly
        };

        // Act
        var runners = TypeFinder.FindSubtypesOf<IRunner>(assemblies);

        // Assert
        runners.Should().BeEmpty();
    }

    [Test]
    public void FindSubtypesOf_WhenSupertypeIsNotInterfaceOrAbstractClass_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var assemblies = new[]
        {
            typeof(TypeFinderTests).Assembly
        };

        // Act
        Action act = () =>
        {
            var subtypes = TypeFinder.FindSubtypesOf<ExampleTest>(assemblies);
        };

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void FindSubtypesOf_WhenArgumentIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        IEnumerable<Assembly> assemblies = default;

        // Act
        Action act = () =>
        {
            var subtypes = TypeFinder.FindSubtypesOf<IRunner>(assemblies);
        };

        // Assert
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(assemblies));
    }
}
