using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Wms.Locations;

namespace Wms.EntityFrameworkCore;

[ConnectionStringName(WmsDbProperties.ConnectionStringName)]
public class WmsDbContext : AbpDbContext<WmsDbContext>, IWmsDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DbSet<Location> Locations { get; set; }

    public WmsDbContext(DbContextOptions<WmsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureWms();
    }
}
