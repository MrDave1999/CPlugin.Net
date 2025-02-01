namespace CPlugin.Net;

/// <summary>
/// Represents a configuration to get the plugin files from an environment variable.
/// </summary>
/// <remarks>
/// The variable must be called <c>PLUGINS</c> and its value must be a string separated by spaces or new lines.
/// <para>Example:</para>
/// <c>PLUGINS=MyPlugin1.dll MyPlugin2.dll</c>
/// <para>if you have plugins with dependencies, you can do this:</para>
/// <c>
/// PLUGINS="
///	MyPlugin1.dll->MyPlugin2.dll,MyPlugin3.dll
/// MyPlugin2.dll
/// MyPlugin3.dll
///"
/// </c>
/// </remarks>
public class CPluginEnvConfiguration : CPluginConfigurationBase
{
    private static readonly string[] s_separator = [" ", "\t", "\r\n", "\n", "\r"];

    /// <summary>
    ///  Initializes a new instance of the <see cref="CPluginEnvConfiguration"/> class.
    /// </summary>
    public CPluginEnvConfiguration() { }

    /// <inheritdoc />
    public override IEnumerable<string> GetPluginFiles()
    {
        var retrievedValue = Environment.GetEnvironmentVariable("PLUGINS");
        if(retrievedValue is null)
            return [];

        var pluginFiles = retrievedValue
            .Split(s_separator, StringSplitOptions.None)
            .Where(pluginFile => !string.IsNullOrWhiteSpace(pluginFile))
            .Select(GetPluginPath);

        return pluginFiles;
    }

    public override IEnumerable<PluginConfig> GetPluginConfigFiles()
    {
        var retrievedValue = Environment.GetEnvironmentVariable("PLUGINS");
        if (retrievedValue is null)
            return [];

        var pluginFiles = retrievedValue
            .Split(s_separator, StringSplitOptions.None)
            .Where(pluginFile => !string.IsNullOrWhiteSpace(pluginFile))
            .ToList();

        return pluginFiles.Select(p =>
        {
            var str = p.Split("->");
            var dependsOn = str.Length == 1 ? [] : str[1].Split(",");

            return new PluginConfig
            {
                Name = GetPluginPath(str[0]),
                DependsOn = [.. dependsOn]
            };
        });
    }
}
