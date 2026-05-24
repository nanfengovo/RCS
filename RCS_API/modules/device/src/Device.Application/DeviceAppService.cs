using Device.Localization;
using Volo.Abp.Application.Services;

namespace Device;

public abstract class DeviceAppService : ApplicationService
{
    protected DeviceAppService()
    {
        LocalizationResource = typeof(DeviceResource);
        ObjectMapperContext = typeof(DeviceApplicationModule);
    }
}
