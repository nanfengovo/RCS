using Volo.Abp.Reflection;

namespace Device.Permissions;

public class DevicePermissions
{
    public const string GroupName = "Device";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DevicePermissions));
    }
}
