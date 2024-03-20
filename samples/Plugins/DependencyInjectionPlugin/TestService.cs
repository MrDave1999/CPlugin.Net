[assembly: Plugin(typeof(TestService))]

namespace Example.DependencyInjectionPlugin;

public class TestService : ITestService
{
    private readonly ILogger<TestService> _logger;
    private readonly IConfiguration _configuration;

    public TestService(
        ILogger<TestService> logger, 
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public string Execute()
    {
        _logger.LogInformation("TestService");
        return _configuration["ServiceName"];
    }
}
