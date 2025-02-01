using CPlugin.Net.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CPlugin.Net;

/// <summary>
/// Represents the loader of plug-in assemblies.
/// </summary>
public static class PluginLoader
{
    private readonly static ConcurrentDictionary<string, Assembly> s_assemblies = new();

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

    /// <summary>
    /// Loads plugins and their dependencies from a specified configuration source.
    /// The plugin list can be retrieved from a JSON file, an environment variable (.env), or another configuration source.
    /// This method ensures that all required dependencies are resolved before loading a plugin.
    /// </summary>
    /// <param name="configuration">
    /// A configuration source that provides the list of plugin files and their dependencies.
    /// </param>
    /// <remarks>
    /// This method is idempotent, meaning that if it is called multiple times, 
    /// it will not reload assemblies that have already been loaded.
    /// If a plugin depends on another plugin that is missing, a <see cref="PluginNotFoundException"/> is thrown.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="configuration"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="PluginNotFoundException">
    /// Thrown when a required plugin dependency is missing.
    /// </exception>
    public static void LoadPluginsWithDependencies(CPluginConfigurationBase configuration)

    {
        ArgumentNullException.ThrowIfNull(configuration);
        var pluginConfigs = configuration.GetPluginConfigFiles();
        foreach (var pluginConfig in pluginConfigs)
        {
            if (pluginConfig.DependsOn?.Count > 0)
            {
                foreach (var dependency in pluginConfig.DependsOn)
                {
                    if (!pluginConfigs.Any(pc => pc.Name.Contains(dependency)))
                    {
                        string pluginName = Path.GetFileName(pluginConfig.Name);
                        throw new PluginNotFoundException(dependency, pluginName);
                    }
                }
            }

            Assembly currentAssembly = FindAssembly(pluginConfig.Name);
            if (currentAssembly is null)
                LoadAssembly(pluginConfig.Name);
        }
    }

    private static void LoadAssembly(string assemblyFile)
    {
        var loadContext = new PluginLoadContext(assemblyFile);
        var assemblyName = AssemblyName.GetAssemblyName(assemblyFile);
        var currentAssembly = loadContext.LoadFromAssemblyName(assemblyName);
        s_assemblies.TryAdd(assemblyFile, currentAssembly);
        PluginLogger.DefaultLogInformation(currentAssembly.GetName().Name, currentAssembly.FullName);
    }

    private static Assembly FindAssembly(string assemblyFile)
    {
        s_assemblies.TryGetValue(assemblyFile, out Assembly assembly);
        return assembly;
    }
}
