using Dispatch.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dispatch.Permissions;

public class DispatchPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DispatchPermissions.GroupName, L("Permission:Dispatch"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DispatchResource>(name);
    }
}
