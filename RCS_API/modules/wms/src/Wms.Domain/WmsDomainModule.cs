using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Wms;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(WmsDomainSharedModule)
)]
public class WmsDomainModule : AbpModule
{

}
