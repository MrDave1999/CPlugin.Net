namespace CPlugin.Net.Tests.Attributes;

public class PluginAttributeTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return typeof(IExample);
        yield return typeof(ExampleBase);
        yield return typeof(AbstractExample);
    }
}

public interface IExample { }
public abstract class ExampleBase { }
public abstract class AbstractExample : IExample { }
