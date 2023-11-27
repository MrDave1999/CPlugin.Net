using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace CPlugin.Net;

/// <summary>
/// Represents a type finder for plugins loaded by <see cref="PluginLoader"/>.
/// </summary>
public static class TypeFinder
{
    /// <summary>
    /// Finds subtypes that implement the contract specified by <typeparamref name="TSupertype"/> 
    /// using the assemblies loaded by <see cref="PluginLoader"/>.
    /// </summary>
    /// <typeparam name="TSupertype">
    /// The type of contract (base type) shared between the host application and the plugins.
    /// </typeparam>
    /// <remarks>
    /// This method uses the <see cref="PluginAttribute"/> type to create an instance of the subtype, so plugins must use it.
    /// </remarks>
    /// <returns>
    /// An instance of type <see cref="IEnumerable{TSuperType}"/> that allows iterating over the instances
    /// that implement the contract specified by <typeparamref name="TSupertype"/>.
    /// <para>or</para>
    /// Returns an empty enumerable if the <typeparamref name="TSupertype"/> does not have any subtype, 
    /// or if no assembly uses <see cref="PluginAttribute"/>.
    /// <para>This method never returns <c>null</c>.</para>
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <c>assemblies</c> is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// <c>TSupertype</c> is not an interface or an abstract class.
    /// </exception>
    public static IEnumerable<TSupertype> FindSubtypesOf<TSupertype>() where TSupertype : class
        => FindSubtypesOf<TSupertype>(PluginLoader.Assemblies);

    // This method is only to be used for testing.
    // This way you don't have to depend on the plugin loader when testing.
    internal static IEnumerable<TSupertype> FindSubtypesOf<TSupertype>(IEnumerable<Assembly> assemblies) 
        where TSupertype : class
    {
        if (assemblies is null)
            throw new ArgumentNullException(nameof(assemblies));

        if(typeof(TSupertype).IsAbstract || typeof(TSupertype).IsInterface)
            return GetSubtypesOf<TSupertype>(assemblies);

        throw new InvalidOperationException($"'{typeof(TSupertype).FullName}' type must be abstract or an interface.");
    }

    private static IEnumerable<TSupertype> GetSubtypesOf<TSupertype>(IEnumerable<Assembly> assemblies) 
        where TSupertype : class
    {
        foreach (Assembly assembly in assemblies)
        {
            var pluginAttributes = assembly.GetCustomAttributes<PluginAttribute>();
            foreach (PluginAttribute pluginAttribute in pluginAttributes)
            {
                Type type = pluginAttribute.PluginType;
                if (typeof(TSupertype).IsAssignableFrom(type))
                    yield return (TSupertype)Activator.CreateInstance(type);
            }
        }
    }
}
