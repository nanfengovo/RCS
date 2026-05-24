using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dispatch;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DispatchDomainSharedModule)
)]
public class DispatchDomainModule : AbpModule
{

}
