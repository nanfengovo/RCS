using Volo.Abp.Modularity;

namespace Diagnostics;

[DependsOn(
    typeof(DiagnosticsApplicationModule),
    typeof(DiagnosticsDomainTestModule)
    )]
public class DiagnosticsApplicationTestModule : AbpModule
{

}
