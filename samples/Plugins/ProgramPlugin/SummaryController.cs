namespace Example.ProgramPlugin;

[ApiController]
[Route("[controller]")]
public class SummaryController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> Get(SummaryService service)
        => service.GetAll();
}
