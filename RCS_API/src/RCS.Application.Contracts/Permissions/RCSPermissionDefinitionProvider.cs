using RCS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace RCS.Permissions;

public class RCSPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(RCSPermissions.GroupName);

        var booksPermission = myGroup.AddPermission(RCSPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(RCSPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(RCSPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(RCSPermissions.Books.Delete, L("Permission:Books.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(RCSPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<RCSResource>(name);
    }
}
