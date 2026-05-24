using Volo.Abp.Modularity;

namespace Diagnostics;

[DependsOn(
    typeof(DiagnosticsDomainModule),
    typeof(DiagnosticsTestBaseModule)
)]
public class DiagnosticsDomainTestModule : AbpModule
{

}
