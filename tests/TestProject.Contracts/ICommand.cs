namespace TestProject.Contracts;

public interface ICommand
{
    public string Name { get; }
    string Execute();
}
