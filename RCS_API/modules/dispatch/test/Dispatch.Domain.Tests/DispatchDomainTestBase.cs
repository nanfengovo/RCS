using Volo.Abp.Modularity;

namespace Dispatch;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class DispatchDomainTestBase<TStartupModule> : DispatchTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
