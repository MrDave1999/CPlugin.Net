namespace CPlugin.Net;
public class PluginConfig
{
    public string Name { get; set; } = string.Empty;
    public List<string> DependsOn { get; set; } = [];
}
