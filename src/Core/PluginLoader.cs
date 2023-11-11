using System.Reflection;

namespace CPlugin.Net;

/// <summary>
/// Represents the loader of plug-in assemblies.
/// </summary>
public static class PluginLoader
{
    private static IEnumerable<string> s_assemblyFiles;
    private readonly static Dictionary<string, Assembly> s_assemblies;

    /// <summary>
    /// Gets the plugin assemblies.
    /// </summary>
    public static IEnumerable<Assembly> Assemblies => s_assemblies.Values;

    static PluginLoader()
    {
        s_assemblyFiles = Enumerable.Empty<string>();
        s_assemblies = new();
    }

    /// <summary>
    /// Sets a configuration source for the loader.
    /// </summary>
    /// <remarks>This allows to get the plugin files from a json or .env file.</remarks>
    /// <param name="configuration">A configuration source for the loader.</param>
    public static void SetConfiguration(CPluginConfigurationBase configuration)
        => s_assemblyFiles = configuration.GetPluginFiles();

    /// <summary>
    /// Loads the plugins together with the specified contract.
    /// </summary>
    /// <typeparam name="TContract">
    /// The type of contract shared between the host application and the plugins.
    /// </typeparam>
    /// <returns>
    /// An instance of type <see cref="IEnumerable{TContract}"/> that contains the instances
    /// that implement the contract specified by <typeparamref name="TContract"/>.
    /// </returns>
    public static IEnumerable<TContract> Load<TContract>() where TContract : class
    {
        var contracts = new List<TContract>();
        foreach (string assemblyFile in s_assemblyFiles)
        {
            Assembly currentAssembly = FindAssembly(assemblyFile);
            currentAssembly ??= LoadAssembly(assemblyFile);
            var pluginAttributes = currentAssembly.GetCustomAttributes<PluginAttribute>();
            if (!pluginAttributes.Any())
            {
                var message = $"'{currentAssembly.GetName().Name}' plugin does not use the '{nameof(PluginAttribute)}' attribute.";
                throw new InvalidOperationException(message);
            }
            foreach (PluginAttribute pluginAttribute in pluginAttributes)
            {
                Type type = pluginAttribute.PluginType;
                if (typeof(TContract).IsAssignableFrom(type))
                {
                    var contract = (TContract)Activator.CreateInstance(type);
                    contracts.Add(contract);
                }
            }
        }
        return contracts;
    }

    private static Assembly LoadAssembly(string assemblyFile)
    {
        var loadContext = new PluginLoadContext(assemblyFile);
        var assemblyName = AssemblyName.GetAssemblyName(assemblyFile);
        var currentAssembly = loadContext.LoadFromAssemblyName(assemblyName);
        s_assemblies.Add(assemblyFile, currentAssembly);
        PluginLogger.DefaultLogInformation(currentAssembly.GetName().Name, currentAssembly.FullName);
        return currentAssembly;
    }

    private static Assembly FindAssembly(string assemblyFile)
    {
        s_assemblies.TryGetValue(assemblyFile, out Assembly assembly);
        return assembly;
    }
}
