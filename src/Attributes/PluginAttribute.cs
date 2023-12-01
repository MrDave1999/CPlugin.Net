using System;

namespace CPlugin.Net;

/// <summary>
/// This attribute is required so that the type finder can create the instance of the subtype that implements the contract.
/// </summary>
/// <remarks>
/// Example:
/// <para>
/// <c>IPluginStartup</c> represents the contract and can reside in its own project called <c>App.Contracts</c>.
/// </para>
/// <para>Each plugin must implement the contract in this way:</para> 
/// <c>class Startup : IPluginStartup { }</c>
/// <para>And then add this line before the namespace declaration:</para>
/// <c>[assembly: Plugin(typeof(Startup))]</c>
/// </remarks>
[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
public class PluginAttribute : Attribute
{
    /// <summary>
    /// Gets an instance of type <see cref="Type"/> that implements the contract.
    /// </summary>
    public Type PluginType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PluginAttribute"/> class.
    /// </summary>
    /// <param name="pluginType">
    /// An instance of type <see cref="Type"/> that implements the contract.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <c>pluginType</c> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <c>pluginType</c> is not instantiable.
    /// </exception>
    public PluginAttribute(Type pluginType)
    {
        if (pluginType is null)
        {
            throw new ArgumentNullException(nameof(pluginType));
        }

        if(pluginType.IsInterface || pluginType.IsAbstract)
        {
            var message = 
                $"'{pluginType.FullName}' type must not be an interface or an abstract class. " +
                $"'{pluginType.FullName}' type must be instantiable.";
            throw new ArgumentException(message, nameof(pluginType));
        }

        PluginType = pluginType;
    }
}
