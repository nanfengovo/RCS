using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Device.EntityFrameworkCore;

[ConnectionStringName(DeviceDbProperties.ConnectionStringName)]
public class DeviceDbContext : AbpDbContext<DeviceDbContext>, IDeviceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DeviceDbContext(DbContextOptions<DeviceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDevice();
    }
}
