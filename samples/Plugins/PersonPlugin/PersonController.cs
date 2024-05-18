namespace Example.PersonPlugin;

[ApiController]
[Route("[controller]")]
internal class PersonController : ControllerBase
{
    [HttpGet] 
    public Result<IEnumerable<Person>> GetAll(IPersonService service)
        => service.GetAll();

    [HttpPost]
    public ActionResult<Result> Create() => Result.CreatedResource().ToActionResult();
}
