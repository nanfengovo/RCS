using Volo.Abp.Modularity;

namespace Wms;

/* Inherit from this class for your domain layer tests. */
public abstract class WmsDomainTestBase<TStartupModule> : WmsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
