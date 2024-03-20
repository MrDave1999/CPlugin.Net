[assembly: Plugin(typeof(ServiceTest1))]
[assembly: Plugin(typeof(ServiceTest2))]
[assembly: Plugin(typeof(ServiceTestExample))]

namespace CPlugin.Net.Tests.Core;

public class ServiceTestBase { }
public class ServiceTestExample : ServiceTestBase { }
public abstract class ServiceTestAbstract { }
public interface IServiceTest
{
    int Execute();
}

public class ServiceTest1 : IServiceTest
{
    public int Execute() => 1;
}
public class ServiceTest2 : IServiceTest
{
    public int Execute() => 1;
}

public class CPluginServiceCollectionExtensionsTests
{
    [TestCase(ServiceLifetime.Singleton)]
    [TestCase(ServiceLifetime.Transient)]
    [TestCase(ServiceLifetime.Scoped)]
    public void AddSubtypesOf_WhenSupertypeHasSubtypes_ShouldAddsSubtypesToServiceCollection(
        ServiceLifetime serviceLifetime)
    {
        // Arrange
        var services = new ServiceCollection();
        var assemblies = new[]
        {
            typeof(CPluginServiceCollectionExtensionsTests).Assembly
        };
        int[] expectedValues = { 1, 1 };

        // Act
        services.AddSubtypesOf<IServiceTest>(assemblies, serviceLifetime);
        using var serviceProvider = services.BuildServiceProvider();
        var testingServices = serviceProvider.GetServices<IServiceTest>();

        int[] values = testingServices
            .Select(service => service.Execute())
            .ToArray();

        // Assert
        values.Should().BeEquivalentTo(expectedValues);
    }

    [Test]
    public void AddSubtypesOf_WhenLifetimeIsInvalid_ShouldThrowNotSupportedException()
    {
        // Arrange
        var services = new ServiceCollection();
        var assemblies = new[]
        {
            typeof(CPluginServiceCollectionExtensionsTests).Assembly
        };
        var serviceLifetime = (ServiceLifetime)500;

        // Act
        Action act = () => services.AddSubtypesOf<IServiceTest>(assemblies, serviceLifetime);

        // Assert
        act.Should().Throw<NotSupportedException>();
    }

    [Test]
    public void AddSubtypesOf_WhenSupertypeDoesNotHaveSubtypes_ShouldNotAddServicesToContainer()
    {
        // Arrange
        var services = new ServiceCollection();
        var assemblies = new[]
        {
            typeof(CPluginServiceCollectionExtensionsTests).Assembly
        };
        var serviceLifetime = ServiceLifetime.Transient;

        // Act
        services.AddSubtypesOf<ServiceTestAbstract>(assemblies, serviceLifetime);
        using var serviceProvider = services.BuildServiceProvider();
        var testingServices = serviceProvider.GetServices<ServiceTestAbstract>();

        // Assert
        testingServices.Should().BeEmpty();
    }

    [Test]
    public void AddSubtypesOf_WhenThereAreNoAssembliesLoaded_ShouldNotAddServicesToContainer()
    {
        // Arrange
        var services = new ServiceCollection();
        var assemblies = Enumerable.Empty<Assembly>();
        var serviceLifetime = ServiceLifetime.Transient;

        // Act
        services.AddSubtypesOf<IServiceTest>(assemblies, serviceLifetime);
        using var serviceProvider = services.BuildServiceProvider();
        var testingServices = serviceProvider.GetServices<IServiceTest>();

        // Assert
        testingServices.Should().BeEmpty();
    }

    [Test]
    public void AddSubtypesOf_WhenNoAssemblyUsesPluginAttribute_ShouldNotAddServicesToContainer()
    {
        // Arrange
        var services = new ServiceCollection();
        var assemblies = new[]
        {
            typeof(TestProject.PluginHost.Employee).Assembly,
            typeof(TestProject.Contracts.ICommand).Assembly
        };
        var serviceLifetime = ServiceLifetime.Transient;

        // Act
        services.AddSubtypesOf<IServiceTest>(assemblies, serviceLifetime);
        using var serviceProvider = services.BuildServiceProvider();
        var testingServices = serviceProvider.GetServices<IServiceTest>();

        // Assert
        testingServices.Should().BeEmpty();
    }

    [Test]
    public void AddSubtypesOf_WhenSupertypeIsNotInterfaceOrAbstractClass_ShouldNotThrowException()
    {
        // Arrange
        var services = new ServiceCollection();
        var assemblies = new[]
        {
            typeof(CPluginServiceCollectionExtensionsTests).Assembly
        };
        var serviceLifetime = ServiceLifetime.Transient;

        // Act
        services.AddSubtypesOf<ServiceTestBase>(assemblies, serviceLifetime);
        using var serviceProvider = services.BuildServiceProvider();
        var testingServices = serviceProvider.GetServices<ServiceTestBase>();

        // Assert
        testingServices.Should().HaveCount(1);
    }

    [Test]
    public void AddSubtypesOf_WhenArgumentIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        IEnumerable<Assembly> assemblies = default;
        var serviceLifetime = ServiceLifetime.Transient;

        // Act
        Action act = () => services.AddSubtypesOf<ServiceTestBase>(assemblies, serviceLifetime);

        // Assert
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(assemblies));
    }
}
