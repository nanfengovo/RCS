using Volo.Abp.Modularity;

namespace Wms;

/* Inherit from this class for your application layer tests. */
public abstract class WmsApplicationTestBase<TStartupModule> : WmsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
