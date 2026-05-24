using Dispatch.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dispatch;

public abstract class DispatchController : AbpControllerBase
{
    protected DispatchController()
    {
        LocalizationResource = typeof(DispatchResource);
    }
}
