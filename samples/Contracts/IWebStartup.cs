using Microsoft.Extensions.DependencyInjection;

namespace Example.Contracts;

public interface IWebStartup
{
    string Name { get; }
    string Description { get; }
    void ConfigureServices(IServiceCollection services);
}
