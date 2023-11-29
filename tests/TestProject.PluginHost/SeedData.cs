namespace TestProject.PluginHost;

public static class SeedData
{
    public static IServiceCollection AddSeedData(this IServiceCollection services)
    {
        List<Employee> _employees = new()
        {
            new()
            {
                Id   = 1,
                Name = "Bob",
                Role = "admin"
            },
            new()
            {
                Id   = 2,
                Name = "Alice",
                Role = "manager"
            }
        };
        services.AddSingleton(_employees);
        return services;
    }
}
