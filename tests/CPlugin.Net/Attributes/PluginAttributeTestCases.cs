namespace CPlugin.Net.Tests.Attributes;

public class PluginAttributeTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return typeof(IExample);
        yield return typeof(ExampleBase);
        yield return typeof(AbstractExample);
        yield return typeof(EnumExample);
        yield return typeof(int);
    }
}

public interface IExample { }
public abstract class ExampleBase { }
public abstract class AbstractExample : IExample { }
public enum EnumExample { }
