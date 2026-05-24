using Device.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Device;

public abstract class DeviceController : AbpControllerBase
{
    protected DeviceController()
    {
        LocalizationResource = typeof(DeviceResource);
    }
}
