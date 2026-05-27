using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Device.EntityFrameworkCore;

[ConnectionStringName(DeviceDbProperties.ConnectionStringName)]
public interface IDeviceDbContext : IEfCoreDbContext
{
}
