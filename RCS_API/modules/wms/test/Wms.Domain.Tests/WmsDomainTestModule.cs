using Volo.Abp.Modularity;

namespace Wms;

[DependsOn(
    typeof(WmsDomainModule),
    typeof(WmsTestBaseModule)
)]
public class WmsDomainTestModule : AbpModule
{

}
