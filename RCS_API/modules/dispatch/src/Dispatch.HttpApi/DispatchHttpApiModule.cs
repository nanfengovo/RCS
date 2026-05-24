using Localization.Resources.AbpUi;
using Dispatch.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Dispatch;

[DependsOn(
    typeof(DispatchApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class DispatchHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DispatchHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DispatchResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
