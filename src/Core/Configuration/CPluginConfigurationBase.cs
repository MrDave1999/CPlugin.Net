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
    /// A collection of plugin files that also contains the paths.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Failed to extract plugin names.
    /// </exception>
    /// <remarks>
    /// Plugin files must be in the <c>plugins</c> directory from current directory 
    /// where the host application is running.
    /// <para>Example:</para>
    /// <c>/HostApp/bin/Debug/net7.0/plugins/MyPlugin1/MyPlugin1.dll</c>
    /// </remarks>
    public abstract IEnumerable<string> GetPluginFiles();

    protected string GetPluginPath(string pluginFile)
    {
        bool isNotPlugin = !Path.GetExtension(pluginFile).Equals(".dll");
        if (isNotPlugin)
        {
            var message =
            $"""
            '{pluginFile}' plug-in must have the extension .dll
            Please check your configuration file.
            """;
            throw new ArgumentException(message, nameof(pluginFile));
        }

        // Example: MyPlugin1
        var pluginDirectory = Path.GetFileNameWithoutExtension(pluginFile);
        // Example: /home/admin/HostApplication/bin/Debug/net7.0/plugins/MyPlugin1
        var basePath = Path.Combine(AppContext.BaseDirectory, "plugins", pluginDirectory);
        // Example: /home/admin/HostApplication/bin/Debug/net7.0/plugins/MyPlugin1/MyPlugin1.dll
        var pluginPath = Path.Combine(basePath, pluginFile);
        return pluginPath;
    }
}
