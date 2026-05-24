using Wms.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Wms.Permissions;

public class WmsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(WmsPermissions.GroupName, L("Permission:Wms"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WmsResource>(name);
    }
}
