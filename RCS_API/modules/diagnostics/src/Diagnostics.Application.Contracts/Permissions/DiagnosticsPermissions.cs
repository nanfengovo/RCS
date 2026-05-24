using Volo.Abp.Reflection;

namespace Diagnostics.Permissions;

public class DiagnosticsPermissions
{
    public const string GroupName = "Diagnostics";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DiagnosticsPermissions));
    }
}
