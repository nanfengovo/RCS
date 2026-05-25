using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Diagnostics.EntityFrameworkCore;

[DependsOn(
    typeof(DiagnosticsDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DiagnosticsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<DiagnosticsDbContext>(options =>
        {
            options.AddDefaultRepositories<IDiagnosticsDbContext>(includeAllEntities: true);

            /* Add custom repositories here. Example:
            * options.AddRepository<Question, EfCoreQuestionRepository>();
            */
        });
    }
}
