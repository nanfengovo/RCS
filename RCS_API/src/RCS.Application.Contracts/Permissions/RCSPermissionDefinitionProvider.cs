using RCS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace RCS.Permissions;

public class RCSPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(RCSPermissions.GroupName);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<RCSResource>(name);
    }
}
