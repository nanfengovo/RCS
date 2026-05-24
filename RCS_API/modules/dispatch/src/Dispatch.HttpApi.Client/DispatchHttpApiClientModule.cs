using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dispatch;

[DependsOn(
    typeof(DispatchApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DispatchHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DispatchApplicationContractsModule).Assembly,
            DispatchRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DispatchHttpApiClientModule>();
        });

    }
}
