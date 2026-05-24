using Wms.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Wms;

public abstract class WmsController : AbpControllerBase
{
    protected WmsController()
    {
        LocalizationResource = typeof(WmsResource);
    }
}
