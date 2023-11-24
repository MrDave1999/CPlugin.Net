using CPlugin.Net;
using Newtonsoft.Json;
using TestProject.Contracts;
using TestProject.JsonPlugin;

[assembly: Plugin(typeof(JsonPluginCommand))]

namespace TestProject.JsonPlugin;

public class JsonPluginCommand : ICommand
{
    public string Name => nameof(JsonPluginCommand);
    public string Execute()
    {
        var version = typeof(JsonPluginCommand)
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
