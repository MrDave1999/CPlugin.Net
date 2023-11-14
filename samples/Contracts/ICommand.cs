namespace Contracts;

public interface ICommand
{
    string Name { get; }
    string Description { get; }
    int Execute();
}
