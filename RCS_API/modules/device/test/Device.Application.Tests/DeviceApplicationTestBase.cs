using Volo.Abp.Modularity;

namespace Device;

/* Inherit from this class for your application layer tests. */
public abstract class DeviceApplicationTestBase<TStartupModule> : DeviceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
