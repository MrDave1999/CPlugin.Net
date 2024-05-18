namespace Example.HostWebApi;

/// <summary>
/// Allows to identify the internal controllers.
/// <para>
/// See 
/// <see href="https://github.com/dotnet/aspnetcore/blob/main/src/Mvc/Mvc.Core/src/Controllers/ControllerFeatureProvider.cs">
/// ControllerFeatureProvider.cs
/// </see>
/// </para>
/// </summary>
public class InternalControllerFeatureProvider : ControllerFeatureProvider
{
    private const string ControllerTypeNameSuffix = "Controller";

    protected override bool IsController(TypeInfo typeInfo)
    {
        if (!typeInfo.IsClass)
            return false;

        if (typeInfo.IsAbstract)
            return false;

        if (typeInfo.ContainsGenericParameters)
            return false;

        if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
            return false;

        if (!typeInfo.Name.EndsWith(ControllerTypeNameSuffix, StringComparison.OrdinalIgnoreCase) &&
            !typeInfo.IsDefined(typeof(ControllerAttribute)))
            return false;

        return true;
    }
}
