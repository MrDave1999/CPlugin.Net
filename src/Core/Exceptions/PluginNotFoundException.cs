namespace CPlugin.Net.Exceptions;
/// <summary>
/// Represents an exception that is thrown when a plugin is not found.
/// </summary>
/// <param name="missingPlugin"> The missing plugin. </param>
/// <param name="dependentPlugin"> The dependent plugin. </param>
public class PluginNotFoundException(string missingPlugin, string dependentPlugin) 
    : Exception($"The plugin '{dependentPlugin}' depends on '{missingPlugin}', but '{missingPlugin}' was not found.")
{
}
