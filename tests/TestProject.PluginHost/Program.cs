using CPlugin.Net;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using SimpleResults;
using TestProject.Contracts;

var builder = WebApplication.CreateBuilder(args);

Environment.SetEnvironmentVariable("PLUGINS", "TestProject.WebPlugin.dll");
var envConfiguration = new CPluginEnvConfiguration();
PluginLoader.Load(envConfiguration);

// Add services to the container.

var abstracts = TypeFinder.FindSubtypesOf<AbstractStartup>();
foreach (var @abstract in abstracts)
    @abstract.ConfigureServices(builder.Services);

var interfaces = TypeFinder.FindSubtypesOf<IPluginStartup>();
foreach (var @interface in interfaces)
    @interface.ConfigureServices(builder.Services);
builder.Services.AddSingleton(interfaces);

var mvcBuilder = builder.Services.AddControllers();
foreach (var assembly in PluginLoader.Assemblies)
{
    // This allows to register the controllers for each loaded assembly.
    mvcBuilder.PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
}
mvcBuilder.AddMvcOptions(options =>
{
    options.Filters.Add<TranslateResultToActionResultAttribute>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

// This class used in the integration test project.
public partial class Program { }
