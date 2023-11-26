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
        var value = new
        {
            Version = typeof(JsonConvert).Assembly.FullName
        };
        return JsonConvert.SerializeObject(value, Formatting.Indented);
    }
}
