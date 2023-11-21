namespace Example.PersonPlugin;

public interface IPersonService
{
    Result<IEnumerable<Person>> GetAll();
}

public class PersonService : IPersonService
{
    private readonly List<Person> _persons;
    public PersonService(List<Person> persons) => _persons = persons;

    public Result<IEnumerable<Person>> GetAll()
    {
        if (_persons.Count == 0)
            return Result.Success<IEnumerable<Person>>(_persons, "No results");

        return Result.Success<IEnumerable<Person>>(_persons);
    }
}
