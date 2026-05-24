using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Wms;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class WmsInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WmsInstallerModule>();
        });
    }
}
