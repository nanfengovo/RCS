using Volo.Abp.Reflection;

namespace Wms.Permissions;

public class WmsPermissions
{
    public const string GroupName = "Wms";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(WmsPermissions));
    }
}
