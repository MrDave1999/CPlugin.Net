namespace Example.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController
{
    private readonly List<Person> _persons;
    public PeopleController(List<Person> persons) => _persons = persons;

    [HttpGet]
    public Result<List<Person>> GetAll() => Result.Success(_persons);

    [HttpPost]
    public Result Create() => Result.Success();
}
