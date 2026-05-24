using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Diagnostics;

[DependsOn(
    typeof(DiagnosticsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class DiagnosticsApplicationContractsModule : AbpModule
{

}
