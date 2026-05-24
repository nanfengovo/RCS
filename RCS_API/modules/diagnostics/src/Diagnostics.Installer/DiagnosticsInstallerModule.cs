using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Diagnostics;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class DiagnosticsInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DiagnosticsInstallerModule>();
        });
    }
}
