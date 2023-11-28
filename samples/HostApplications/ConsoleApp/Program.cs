// See https://aka.ms/new-console-template for more information
new EnvLoader().Load();
Console.WriteLine("PLUGINS=" + EnvReader.Instance["PLUGINS"]);
Console.WriteLine();

var envConfiguration = new CPluginEnvConfiguration();
// Loads the plugins from the .env file.
PluginLoader.Load(envConfiguration);
var commands = TypeFinder.FindSubtypesOf<ICommand>();
foreach(ICommand command in commands)
{
    command.Execute();
}
