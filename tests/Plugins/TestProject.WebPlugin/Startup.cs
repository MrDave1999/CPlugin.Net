using CPlugin.Net;
using TestProject.Contracts;
using TestProject.WebPlugin;

[assembly: Plugin(typeof(StartupImpl))]
[assembly: Plugin(typeof(Startup))]

namespace TestProject.WebPlugin;

public class StartupImpl : AbstractStartup
{
    public override void ConfigureServices(IServiceCollection services)
    {
        var users = new List<User>
        {
            new() { Name = "Bob",   Password = "password11" },
            new() { Name = "Alice", Password = "password22" },
            new() { Name = "Dave",  Password = "password33" },
        };
        services.AddSingleton(users);
    }
}

public class Startup : IPluginStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<UserService>();
    }
}
