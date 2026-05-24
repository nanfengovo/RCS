using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Device.EntityFrameworkCore;

[ConnectionStringName(DeviceDbProperties.ConnectionStringName)]
public interface IDeviceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
