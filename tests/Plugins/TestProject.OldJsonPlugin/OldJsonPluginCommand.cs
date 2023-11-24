using CPlugin.Net;
using Newtonsoft.Json;
using TestProject.Contracts;
using TestProject.OldJsonPlugin;

[assembly: Plugin(typeof(OldJsonPluginCommand))]

namespace TestProject.OldJsonPlugin;

public class OldJsonPluginCommand : ICommand
{
    public string Name => nameof(OldJsonPluginCommand);
    public string Execute()
    {
        var version = typeof(OldJsonPluginCommand)
            .Assembly
            .GetName()
            .Version
            .ToString();

        var value = new
        {
            Version = version,
        };
        return JsonConvert.SerializeObject(value, Formatting.Indented);
    }
}
