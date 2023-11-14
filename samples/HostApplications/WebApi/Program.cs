var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var jsonConfiguration = new CPluginJsonConfiguration(builder.Configuration);
PluginLoader.SetConfiguration(jsonConfiguration);
var contracts = PluginLoader.Load<IWebStartup>();
foreach(IWebStartup contract in contracts)
{
    contract.ConfigureServices(builder.Services);
}

IMvcBuilder mvcBuilder = builder.Services.AddControllers();
foreach (Assembly assembly in PluginLoader.Assemblies)
{
    // This allows to register the controllers for each loaded assembly.
    mvcBuilder.PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
