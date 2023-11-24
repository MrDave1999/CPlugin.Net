using TestProject.Contracts;

namespace TestProject.HelloPlugin;

public class HelloCommand : ICommand
{
    public string Name => nameof(HelloCommand);

    public string Execute()
    {
        return "Hello Word!";
    }
}
