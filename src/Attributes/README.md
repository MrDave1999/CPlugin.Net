# CPlugin.Net.Attributes

A simple library that includes the [PluginAttribute](https://mrdave1999.github.io/CPlugin.Net/api/CPlugin.Net.PluginAttribute.html) type to be used by plugins.

### Example

`IPluginStartup` represents the contract and can reside in its own project called `MyApp.Contracts`.

Each plugin must implement the contract in this way:
```cs
public class Startup : IPluginStartup 
{ 

}
```

And then add this line before the namespace declaration:
```cs
[assembly: Plugin(typeof(Startup))]
```

**Complete example:**
```cs
using Project.MyPlugin1;
using MyApp.Contracts;
using CPlugin.Net;

[assembly: Plugin(typeof(Startup))]
namespace Project.MyPlugin1;

public class Startup : IPluginStartup 
{ 

}
```