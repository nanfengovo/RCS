using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Device;

[DependsOn(
    typeof(DeviceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DeviceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DeviceApplicationContractsModule).Assembly,
            DeviceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DeviceHttpApiClientModule>();
        });

    }
}
