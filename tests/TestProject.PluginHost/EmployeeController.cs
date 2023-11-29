using Microsoft.AspNetCore.Mvc;
using SimpleResults;
using TestProject.Contracts;

namespace TestProject.PluginHost;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEnumerable<IPluginStartup> _startups;
    public EmployeeController(IEnumerable<IPluginStartup> startups) => _startups = startups;

    [HttpGet]
    public List<Employee> GetAll() => new()
    {
        new() 
        { 
            Id   = _startups.Count(), 
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

    [HttpPost]
    public Result Create() => Result.Success();
}
