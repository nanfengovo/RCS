using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Device.EntityFrameworkCore;

[DependsOn(
    typeof(DeviceDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DeviceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<DeviceDbContext>(options =>
        {
            options.AddDefaultRepositories<IDeviceDbContext>(includeAllEntities: true);
            
            /* Add custom repositories here. Example:
            * options.AddRepository<Question, EfCoreQuestionRepository>();
            */
        });
    }
}
