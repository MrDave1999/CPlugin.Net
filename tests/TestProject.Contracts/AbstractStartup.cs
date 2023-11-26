using Microsoft.Extensions.DependencyInjection;

namespace TestProject.Contracts;

public abstract class AbstractStartup
{
    public virtual void ConfigureServices(IServiceCollection services) { }
}
