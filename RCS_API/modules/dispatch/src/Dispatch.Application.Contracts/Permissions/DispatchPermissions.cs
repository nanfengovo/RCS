using Volo.Abp.Reflection;

namespace Dispatch.Permissions;

public class DispatchPermissions
{
    public const string GroupName = "Dispatch";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DispatchPermissions));
    }
}
