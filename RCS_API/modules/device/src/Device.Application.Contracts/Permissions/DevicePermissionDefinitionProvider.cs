using Device.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Device.Permissions;

public class DevicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DevicePermissions.GroupName, L("Permission:Device"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DeviceResource>(name);
    }
}
