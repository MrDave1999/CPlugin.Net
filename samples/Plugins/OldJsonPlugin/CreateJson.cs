[assembly: Plugin(typeof(CreateJson))]

namespace Example.OldJsonPlugin;

public class CreateJson : ICommand
{
    public string Name => "oldjson";
    public string Description => "Outputs JSON value.";
    public string Version => typeof(JsonConvert).Assembly.FullName;

    private class Info
    {
        public string JsonVersion;
        public string JsonLocation;
        public string Machine;
        public DateTime Date;
    }

    public int Execute()
    {
        Assembly jsonAssembly = typeof(JsonConvert).Assembly;
        var info = new Info
        {
            JsonVersion  = jsonAssembly.FullName,
            JsonLocation = jsonAssembly.Location,
            Machine      = Environment.MachineName,
            Date         = DateTime.Now
        };
        Console.WriteLine(JsonConvert.SerializeObject(info, Formatting.Indented));
        return 0;
    }
}
