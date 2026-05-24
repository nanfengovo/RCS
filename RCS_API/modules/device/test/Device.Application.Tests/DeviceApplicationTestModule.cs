using Volo.Abp.Modularity;

namespace Device;

[DependsOn(
    typeof(DeviceApplicationModule),
    typeof(DeviceDomainTestModule)
    )]
public class DeviceApplicationTestModule : AbpModule
{

}
