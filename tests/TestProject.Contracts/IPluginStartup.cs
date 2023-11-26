using Microsoft.Extensions.DependencyInjection;

namespace TestProject.Contracts;

public interface IPluginStartup
{
    void ConfigureServices(IServiceCollection services);
}
