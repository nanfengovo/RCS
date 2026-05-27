using Volo.Abp.Modularity;

namespace Device;

/* Inherit from this class for your domain layer tests. */
public abstract class DeviceDomainTestBase<TStartupModule> : DeviceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
