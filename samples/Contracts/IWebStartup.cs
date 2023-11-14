using Microsoft.Extensions.DependencyInjection;

namespace Contracts;

public interface IWebStartup
{
    string Name { get; }
    string Description { get; }
    void ConfigureServices(IServiceCollection services);
}
