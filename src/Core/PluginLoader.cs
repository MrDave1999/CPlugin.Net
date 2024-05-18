using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CPlugin.Net;

/// <summary>
/// Represents the loader of plug-in assemblies.
/// </summary>
public static class PluginLoader
{
    private readonly static Dictionary<string, Assembly> s_assemblies = new();

    /// <summary>
    /// Gets the plugin assemblies.
    /// </summary>
    public static IEnumerable<Assembly> Assemblies => s_assemblies.Values;

    /// <summary>
    /// Loads plugins from a configuration source specified by the <c>configuration</c> parameter.
    /// This means that the plugin names can be obtained from a json or .env file.
    /// </summary>
    /// <param name="configuration">A configuration source to get the plugin files.</param>
    /// <remarks>
    /// This method is idempotent, so if this method is called N times, 
    /// it will not reload assemblies that have already been loaded.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// <c>configuration</c> is <c>null</c>.
    /// </exception>
    public static void Load(CPluginConfigurationBase configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var assemblyFiles = configuration.GetPluginFiles();
        foreach (string assemblyFile in assemblyFiles)
        {
            Assembly currentAssembly = FindAssembly(assemblyFile);
            if(currentAssembly is null)
                LoadAssembly(assemblyFile);
        }
    }

    private static void LoadAssembly(string assemblyFile)
    {
        var loadContext = new PluginLoadContext(assemblyFile);
        var assemblyName = AssemblyName.GetAssemblyName(assemblyFile);
        var currentAssembly = loadContext.LoadFromAssemblyName(assemblyName);
        s_assemblies.Add(assemblyFile, currentAssembly);
        PluginLogger.DefaultLogInformation(currentAssembly.GetName().Name, currentAssembly.FullName);
    }

    private static Assembly FindAssembly(string assemblyFile)
    {
        s_assemblies.TryGetValue(assemblyFile, out Assembly assembly);
        return assembly;
    }
}
