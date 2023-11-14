// See https://aka.ms/new-console-template for more information
new EnvLoader().Load();
Console.WriteLine("PLUGINS=" + EnvReader.Instance["PLUGINS"]);
Console.WriteLine();

var envConfiguration = new CPluginEnvConfiguration();
PluginLoader.SetConfiguration(envConfiguration);
// Loads the plugins from the .env file.
var contracts = PluginLoader.Load<ICommand>();
foreach(ICommand contract in contracts)
{
    contract.Execute();
}
