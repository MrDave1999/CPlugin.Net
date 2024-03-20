namespace Example.HostWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController
{
    [HttpGet]
    public ActionResult<string> Get(ITestService service)
        => service.Execute();
}
