using Volo.Abp.Modularity;

namespace Wms;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class WmsDomainTestBase<TStartupModule> : WmsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
