using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Wms;

[DependsOn(
    typeof(WmsApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class WmsHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(WmsApplicationContractsModule).Assembly,
            WmsRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WmsHttpApiClientModule>();
        });

    }
}
