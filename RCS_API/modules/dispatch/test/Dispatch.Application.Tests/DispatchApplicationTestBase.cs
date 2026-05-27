using Volo.Abp.Modularity;

namespace Dispatch;

/* Inherit from this class for your application layer tests. */
public abstract class DispatchApplicationTestBase<TStartupModule> : DispatchTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
