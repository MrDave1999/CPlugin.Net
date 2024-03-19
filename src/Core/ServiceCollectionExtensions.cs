using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CPlugin.Net;

/// <summary>
/// Extension methods for adding services to an <see cref="IServiceCollection"/>.
/// </summary>
public static class CPluginServiceCollectionExtensions
{
    /// <summary>
    /// Adds the subtypes that implement the contract specified by <typeparamref name="TSupertype"/> 
    /// to the service collection, using the assemblies loaded by <see cref="PluginLoader"/>.
    /// </summary>
    /// <typeparam name="TSupertype">
    /// The type of contract (base type) shared between the host application and the plugins.
    /// </typeparam>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to add the service to.
    /// </param>
    /// <param name="serviceLifetime">
    /// Specifies the lifetime of the services to be added to the service collection.
    /// </param>
    /// <remarks>
    /// This method uses the <see cref="PluginAttribute"/> type to add the implementations of the contract 
    /// to the service collection, so plugins must use it.
    /// </remarks>
    /// <returns>
    /// A reference to this instance after the operation has completed.
    /// </returns>
    public static IServiceCollection AddSubtypesOf<TSupertype>(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime) where TSupertype : class
        => services.AddSubtypesOf<TSupertype>(PluginLoader.Assemblies, serviceLifetime);

    // This method is only to be used for testing.
    // This way you don't have to depend on the plugin loader when testing.
    internal static IServiceCollection AddSubtypesOf<TSupertype>(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        ServiceLifetime serviceLifetime) where TSupertype : class
    {
        if (assemblies is null)
            throw new ArgumentNullException(nameof(assemblies));

        foreach (Assembly assembly in assemblies)
        {
            var pluginAttributes = assembly.GetCustomAttributes<PluginAttribute>();
            foreach (PluginAttribute pluginAttribute in pluginAttributes)
            {
                Type implementationType = pluginAttribute.PluginType;
                if (typeof(TSupertype).IsAssignableFrom(implementationType))
                {
                    services.AddService(
                        serviceType: typeof(TSupertype), 
                        implementationType, 
                        serviceLifetime);
                }
            }
        }

        return services;
    }

    private static IServiceCollection AddService(
        this IServiceCollection services,
        Type serviceType,
        Type implementationType,
        ServiceLifetime serviceLifetime) => serviceLifetime switch
        {
            ServiceLifetime.Singleton => services.AddSingleton(serviceType, implementationType),
            ServiceLifetime.Transient => services.AddTransient(serviceType, implementationType),
            ServiceLifetime.Scoped    => services.AddScoped(serviceType, implementationType),
            _ => throw new NotSupportedException($"Lifetime '{serviceLifetime}' is not supported.")
        };
}
