using Localization.Resources.AbpUi;
using Device.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Device;

[DependsOn(
    typeof(DeviceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class DeviceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DeviceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DeviceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
