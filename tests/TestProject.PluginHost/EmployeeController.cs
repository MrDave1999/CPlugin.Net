using Microsoft.AspNetCore.Mvc;

namespace TestProject.PluginHost;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    public List<Employee> GetAll()
    {
        return new()
        {
            new() { Id = 1, Name = "Bob",   Role = "admin" },
            new() { Id = 2, Name = "Alice", Role = "manager" }
        };
    }
}
