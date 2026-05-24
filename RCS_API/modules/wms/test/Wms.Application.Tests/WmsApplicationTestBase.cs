using Volo.Abp.Modularity;

namespace Wms;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class WmsApplicationTestBase<TStartupModule> : WmsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
