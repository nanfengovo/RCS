using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Wms.EntityFrameworkCore;

[DependsOn(
    typeof(WmsDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class WmsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<WmsDbContext>(options =>
        {
            options.AddDefaultRepositories<IWmsDbContext>(includeAllEntities: true);

            /* Add custom repositories here. Example:
            * options.AddRepository<Question, EfCoreQuestionRepository>();
            */
        });
    }
}
