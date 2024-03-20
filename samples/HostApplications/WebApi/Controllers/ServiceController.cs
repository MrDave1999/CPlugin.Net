namespace Example.HostWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController
{
    [HttpGet]
    public ActionResult<string> Get(IEnumerable<ITestService> services)
        => services.First().Execute();
}
