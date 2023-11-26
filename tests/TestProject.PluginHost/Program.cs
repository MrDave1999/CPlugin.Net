using CPlugin.Net;
using DotEnv.Core;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using TestProject.Contracts;

var builder = WebApplication.CreateBuilder(args);

var env =
"""
PLUGINS=TestProject.WebPlugin.dll
""";
new EnvParser().Parse(env);

var envConfiguration = new CPluginEnvConfiguration();
PluginLoader.SetConfiguration(envConfiguration);

// Add services to the container.

var abstracts = PluginLoader.Load<AbstractStartup>();
foreach (var @abstract in abstracts)
    @abstract.ConfigureServices(builder.Services);

var interfaces = PluginLoader.Load<IPluginStartup>();
foreach (var @interface in interfaces)
    @interface.ConfigureServices(builder.Services);
builder.Services.AddSingleton(interfaces);

var mvcBuilder = builder.Services.AddControllers();
foreach (var assembly in PluginLoader.Assemblies)
{
    // This allows to register the controllers for each loaded assembly.
    mvcBuilder.PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
}

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
