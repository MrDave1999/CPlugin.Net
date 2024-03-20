var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var jsonConfiguration = new CPluginJsonConfiguration(builder.Configuration);
// Loads the plugins from the appsettings.json file.
PluginLoader.Load(jsonConfiguration);
var startups = TypeFinder.FindSubtypesOf<IWebStartup>();
foreach(IWebStartup startup in startups)
{
    startup.ConfigureServices(builder.Services);
}

IMvcBuilder mvcBuilder = builder.Services.AddControllers();
foreach (Assembly assembly in PluginLoader.Assemblies)
{
    // This allows to register the controllers for each loaded assembly.
    mvcBuilder.PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
}

builder.Services.AddSubtypesOf<ITestService>(ServiceLifetime.Transient);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // See https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1607
    options.CustomSchemaIds(type => $"{type.Name}_{Guid.NewGuid()}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization("en");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
