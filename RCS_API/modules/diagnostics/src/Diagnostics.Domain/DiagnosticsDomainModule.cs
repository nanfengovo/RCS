using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Diagnostics;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DiagnosticsDomainSharedModule)
)]
public class DiagnosticsDomainModule : AbpModule
{

}
