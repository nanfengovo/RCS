using Volo.Abp.Modularity;

namespace Diagnostics;

/* Inherit from this class for your domain layer tests. */
public abstract class DiagnosticsDomainTestBase<TStartupModule> : DiagnosticsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
