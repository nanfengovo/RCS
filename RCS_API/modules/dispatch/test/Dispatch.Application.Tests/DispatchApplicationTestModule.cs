using Volo.Abp.Modularity;

namespace Dispatch;

[DependsOn(
    typeof(DispatchApplicationModule),
    typeof(DispatchDomainTestModule)
    )]
public class DispatchApplicationTestModule : AbpModule
{

}
