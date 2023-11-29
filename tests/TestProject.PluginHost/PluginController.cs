using Microsoft.AspNetCore.Mvc;
using TestProject.Contracts;

namespace TestProject.PluginHost;

[ApiController]
[Route("[controller]")]
public class PluginController : ControllerBase
{
    private readonly IEnumerable<IPluginStartup> _startups;
    public PluginController(IEnumerable<IPluginStartup> startups) => _startups = startups;

    [HttpGet]
    public string[] GetInfo()
        => _startups.Select(p => p.Name).ToArray();
}
