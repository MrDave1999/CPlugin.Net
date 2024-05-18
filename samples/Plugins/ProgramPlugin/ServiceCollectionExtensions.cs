namespace Example.ProgramPlugin;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddSingleton<SummaryService>();

        return services;
    }
}
