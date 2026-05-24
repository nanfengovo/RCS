using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Diagnostics;

[DependsOn(
    typeof(DiagnosticsApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DiagnosticsHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DiagnosticsApplicationContractsModule).Assembly,
            DiagnosticsRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DiagnosticsHttpApiClientModule>();
        });

    }
}
