#if PLUGIN

[assembly: Plugin(typeof(Startup))]

namespace Example.ProgramPlugin;

public class Startup : IWebStartup
{
    public string Name => "summary";
    public string Description => "Expose endpoints.";

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddServices();
    }
}
#endif