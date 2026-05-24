using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dispatch.EntityFrameworkCore;

[DependsOn(
    typeof(DispatchDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DispatchEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<DispatchDbContext>(options =>
        {
            options.AddDefaultRepositories<IDispatchDbContext>(includeAllEntities: true);
            
            /* Add custom repositories here. Example:
            * options.AddRepository<Question, EfCoreQuestionRepository>();
            */
        });
    }
}
