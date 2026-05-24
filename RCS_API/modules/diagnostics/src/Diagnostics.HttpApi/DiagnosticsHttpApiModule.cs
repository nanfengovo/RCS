using Localization.Resources.AbpUi;
using Diagnostics.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Diagnostics;

[DependsOn(
    typeof(DiagnosticsApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class DiagnosticsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DiagnosticsHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DiagnosticsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
