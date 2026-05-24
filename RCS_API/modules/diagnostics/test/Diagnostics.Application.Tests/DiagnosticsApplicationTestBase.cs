using Volo.Abp.Modularity;

namespace Diagnostics;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class DiagnosticsApplicationTestBase<TStartupModule> : DiagnosticsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
