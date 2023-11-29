using Microsoft.AspNetCore.Mvc;
using SimpleResults;

namespace TestProject.PluginHost;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly List<Employee> _employees;

    public EmployeeController(List<Employee> employees)
    {
        _employees = employees;
    }

    [HttpGet]
    public List<Employee> GetAll() => _employees;

    [HttpPost]
    public Result Create([FromBody]Employee employee)
    {
        _employees.Add(employee);
        return Result.CreatedResource();
    }
}
