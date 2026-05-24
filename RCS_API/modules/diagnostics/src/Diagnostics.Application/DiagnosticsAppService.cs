using Diagnostics.Localization;
using Volo.Abp.Application.Services;

namespace Diagnostics;

public abstract class DiagnosticsAppService : ApplicationService
{
    protected DiagnosticsAppService()
    {
        LocalizationResource = typeof(DiagnosticsResource);
        ObjectMapperContext = typeof(DiagnosticsApplicationModule);
    }
}
