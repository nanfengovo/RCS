using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Device;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DeviceDomainSharedModule)
)]
public class DeviceDomainModule : AbpModule
{

}
