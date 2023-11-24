using SimpleResults;

namespace TestProject.WebPlugin;

public class UserService
{
    private readonly List<User> _users;
    public UserService(List<User> users) => _users = users;

    public ListedResult<User> GetAll()
        => _users.Count == 0 ? Result.Failure() : Result.ObtainedResources(_users);
}
