using Volo.Abp.Modularity;

namespace Dispatch;

[DependsOn(
    typeof(DispatchDomainModule),
    typeof(DispatchTestBaseModule)
)]
public class DispatchDomainTestModule : AbpModule
{

}
