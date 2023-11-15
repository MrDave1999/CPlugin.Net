using System;
using System.Collections.Generic;
using System.Linq;

namespace CPlugin.Net;

/// <summary>
/// Represents a configuration to get the plugin files from an environment variable.
/// </summary>
/// <remarks>
/// The variable must be called <c>PLUGINS</c> and its value must be a string separated by spaces.
/// <para>Example:</para>
/// <c>PLUGINS=MyPlugin1.dll MyPlugin2.dll</c>
/// </remarks>
public class CPluginEnvConfiguration : CPluginConfigurationBase
{
    /// <summary>
    ///  Initializes a new instance of the <see cref="CPluginEnvConfiguration"/> class.
    /// </summary>
    public CPluginEnvConfiguration() { }

    /// <inheritdoc />
    public override IEnumerable<string> GetPluginFiles()
    {
        var retrievedValue = Environment.GetEnvironmentVariable("PLUGINS");
        if(retrievedValue is null)
            return Enumerable.Empty<string>();

        var pluginFiles = retrievedValue
            .Split(" ")
            .Where(pluginFile => !string.IsNullOrWhiteSpace(pluginFile))
            .Select(GetPluginPath);

        return pluginFiles;
    }
}
