﻿[assembly: Plugin(typeof(CreateJson))]

namespace Example.JsonPlugin;

internal class CreateJson : ICommand
{
    public string Name => "json";
    public string Description => "Outputs JSON value.";
    public string Version => typeof(JsonConvert).Assembly.FullName;

    private class Info
    {
        public string JsonVersion;
        public string JsonLocation;
        public string Machine;
        public string User;
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
            User         = Environment.UserName,
            Date         = DateTime.Now
        };
        Console.WriteLine(JsonConvert.SerializeObject(info, Formatting.Indented));
        return 0;
    }
}
