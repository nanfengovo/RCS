using Volo.Abp.Modularity;

namespace Device;

[DependsOn(
    typeof(DeviceDomainModule),
    typeof(DeviceTestBaseModule)
)]
public class DeviceDomainTestModule : AbpModule
{

}
