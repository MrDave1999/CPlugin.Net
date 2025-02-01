using System;
using System.Collections.Generic;
using System.IO;

namespace CPlugin.Net;

/// <summary>
/// Represents a configuration source to obtain the plugin files.
/// </summary>
public abstract class CPluginConfigurationBase
{
    /// <summary>
    /// Gets the full path to each plugin file from a configuration source.
    /// </summary>
    /// <returns>
    /// A collection of plugin files that also contains the paths;
    /// <para>or</para>
    /// Returns an empty enumerable when the plugin files could not be obtained.
    /// <para>This method never returns <c>null</c>.</para>
    /// </returns>
    /// <remarks>
    /// Plugin files must be in the <c>plugins</c> directory of the current directory 
    /// where the host application is running.
    /// <para>Each plugin file must have a <c>.dll</c> extension and must be in its own directory.</para>
    /// <para>Example:</para>
    /// <c>/HostApp/bin/Debug/net7.0/plugins/MyPlugin1/MyPlugin1.dll</c>
    /// </remarks>
    public abstract IEnumerable<string> GetPluginFiles();

    /// <summary>
    /// Gets the full path to each plugin file from a configuration source.
    /// </summary>
    /// <returns>
    /// A collection of plugin files that also contains the paths;
    /// <para>or</para>
    /// Returns an empty enumerable when the plugin files could not be obtained.
    /// <para>This method never returns <c>null</c>.</para>
    /// </returns>
    /// <remarks>
    /// Plugin files must be in the <c>plugins</c> directory of the current directory 
    /// where the host application is running.
    /// <para>Each plugin file must have a <c>.dll</c> extension and must be in its own directory.</para>
    /// <para>Example:</para>
    /// <c>/HostApp/bin/Debug/net7.0/plugins/MyPlugin1/MyPlugin1.dll</c>
    /// </remarks>
    public abstract IEnumerable<PluginConfig> GetPluginConfigFiles();

    /// <summary>
    /// Gets the full path of a plugin file.
    /// </summary>
    /// <remarks>
    /// If the plugin name does not have the <c>.dll</c> extension, it is added by default.
    /// </remarks>
    /// <param name="pluginFile">The name of a plugin file.</param>
    /// <returns>The full path of a plugin file.</returns>
    protected static string GetPluginPath(string pluginFile)
    {
        bool hasDllExtension = Path
            .GetExtension(pluginFile)
            .Equals(".dll");
        pluginFile = hasDllExtension ? pluginFile : pluginFile + ".dll";
        // Example: MyPlugin1
        var pluginDirectory = Path.GetFileNameWithoutExtension(pluginFile);
        // Example: /home/admin/HostApplication/bin/Debug/net7.0/plugins/MyPlugin1
        var basePath = Path.Combine(AppContext.BaseDirectory, "plugins", pluginDirectory);
        // Example: /home/admin/HostApplication/bin/Debug/net7.0/plugins/MyPlugin1/MyPlugin1.dll
        var pluginPath = Path.Combine(basePath, pluginFile);
        return pluginPath;
    }
}
