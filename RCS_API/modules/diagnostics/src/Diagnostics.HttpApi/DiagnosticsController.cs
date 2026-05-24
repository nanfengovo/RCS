using Diagnostics.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Diagnostics;

public abstract class DiagnosticsController : AbpControllerBase
{
    protected DiagnosticsController()
    {
        LocalizationResource = typeof(DiagnosticsResource);
    }
}
