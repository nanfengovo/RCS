using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dispatch;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class DispatchInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DispatchInstallerModule>();
        });
    }
}
