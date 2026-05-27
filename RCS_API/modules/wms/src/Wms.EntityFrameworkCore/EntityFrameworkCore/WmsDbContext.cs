using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Wms.Locations;

namespace Wms.EntityFrameworkCore;

[ConnectionStringName(WmsDbProperties.ConnectionStringName)]
public class WmsDbContext : AbpDbContext<WmsDbContext>, IWmsDbContext
{

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
