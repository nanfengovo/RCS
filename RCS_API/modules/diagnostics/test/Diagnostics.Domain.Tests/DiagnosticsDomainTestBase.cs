using Volo.Abp.Modularity;

namespace Diagnostics;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class DiagnosticsDomainTestBase<TStartupModule> : DiagnosticsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
