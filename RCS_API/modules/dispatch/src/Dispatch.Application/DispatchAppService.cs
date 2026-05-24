using Dispatch.Localization;
using Volo.Abp.Application.Services;

namespace Dispatch;

public abstract class DispatchAppService : ApplicationService
{
    protected DispatchAppService()
    {
        LocalizationResource = typeof(DispatchResource);
        ObjectMapperContext = typeof(DispatchApplicationModule);
    }
}
