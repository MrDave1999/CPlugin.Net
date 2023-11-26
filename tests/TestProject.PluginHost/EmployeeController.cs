using Microsoft.AspNetCore.Mvc;
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
        new() { Id = 1, Name = "Bob",   Role = "admin" },
        new() { Id = 2, Name = "Alice", Role = "manager" }
    };

    [HttpGet("count-startup")]
    public int CountStartup() => _startups.Count();
}
