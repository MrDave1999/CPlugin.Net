using System;
using System.Reflection;
using System.Runtime.Loader;

namespace CPlugin.Net;

internal class PluginLoadContext : AssemblyLoadContext
{
    private static readonly Dictionary<string, Version> s_defaultAssemblies;

    static PluginLoadContext()
    {
        s_defaultAssemblies = Default
            .Assemblies
            .ToDictionary(
                assembly => assembly.GetName().Name, 
                assembly => assembly.GetName().Version);
    }

    private readonly AssemblyDependencyResolver _resolver;

    public PluginLoadContext(string pluginPath)
    {
        _resolver = new AssemblyDependencyResolver(pluginPath);
    }

    protected override Assembly Load(AssemblyName assemblyName)
    {
        // This is to avoid loading the CPlugin.Net.Attributes.dll assembly in the custom context.
        // This should be loaded in the default context.
        if (assemblyName.Name == "CPlugin.Net.Attributes")
            return default;

        // This is to avoid loading the same version of an assembly.
        // If the assembly exists, it means that the default load context already has it.
        bool assemblyExists = s_defaultAssemblies.TryGetValue(assemblyName.Name, out Version version);
        if (assemblyExists && assemblyName.Version.ToString() == version.ToString())
        {
            return default;
        }

        var assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
        return assemblyPath is null ? default : LoadFromAssemblyPath(assemblyPath);
    }

    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        var libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        return libraryPath is null ? IntPtr.Zero : LoadUnmanagedDllFromPath(libraryPath);
    }
}
