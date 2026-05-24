using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Device;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class DeviceInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DeviceInstallerModule>();
        });
    }
}
