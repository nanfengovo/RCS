using Diagnostics.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Diagnostics.Permissions;

public class DiagnosticsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DiagnosticsPermissions.GroupName, L("Permission:Diagnostics"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DiagnosticsResource>(name);
    }
}
