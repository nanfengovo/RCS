using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Dispatch;

[DependsOn(
    typeof(DispatchDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class DispatchApplicationContractsModule : AbpModule
{

}
