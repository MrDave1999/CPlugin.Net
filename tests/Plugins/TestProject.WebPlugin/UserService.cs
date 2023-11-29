using SimpleResults;

namespace TestProject.WebPlugin;

public class UserService
{
    private readonly List<User> _users;
    public UserService(List<User> users) => _users = users;

    public ListedResult<User> GetAll()
        => _users.Count == 0 ? Result.Failure() : Result.ObtainedResources(_users);

    public Result Create(string name, string password)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            return Result.Invalid("Required fields: name, password.");

        _users.Add(new User { Name = name, Password = password });
        return Result.CreatedResource();
    }

    public Result Clear()
    {
        _users.Clear();
        return Result.Success();
    }
}
