using Volo.Abp.Modularity;

namespace Diagnostics;

/* Inherit from this class for your application layer tests. */
public abstract class DiagnosticsApplicationTestBase<TStartupModule> : DiagnosticsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
