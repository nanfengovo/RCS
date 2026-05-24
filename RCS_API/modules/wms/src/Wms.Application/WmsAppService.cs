using Wms.Localization;
using Volo.Abp.Application.Services;

namespace Wms;

public abstract class WmsAppService : ApplicationService
{
    protected WmsAppService()
    {
        LocalizationResource = typeof(WmsResource);
        ObjectMapperContext = typeof(WmsApplicationModule);
    }
}
