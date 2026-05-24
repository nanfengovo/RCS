using Volo.Abp.Modularity;

namespace Wms;

[DependsOn(
    typeof(WmsApplicationModule),
    typeof(WmsDomainTestModule)
    )]
public class WmsApplicationTestModule : AbpModule
{

}
