using Microsoft.Extensions.DependencyInjection;

namespace TestProject.Contracts;

public interface IPluginStartup
{
    public string Name { get; }
    void ConfigureServices(IServiceCollection services);
}
