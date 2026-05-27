using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Wms.Locations;

namespace Wms.EntityFrameworkCore;

public static class WmsDbContextModelCreatingExtensions
{
    public static void ConfigureWms(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Location>(b =>
        {
            b.ToTable(WmsDbProperties.DbTablePrefix + "Locations", WmsDbProperties.DbSchema);
            b.ConfigureByConvention();

            b.HasIndex(x => x.LocationCode).IsUnique();
            b.Property(x => x.LocationCode).IsRequired().HasMaxLength(64);
            b.Property(x => x.AssociatedDeviceCode).HasMaxLength(128);

            //将值对象(LocationCoordinate)展开拍平成数据库列
            b.OwnsOne(x => x.LocationCoordinate, c =>
            {
                c.Property(p => p.Row).HasColumnName("Row");
                c.Property(p => p.Column).HasColumnName("Column");
                c.Property(p => p.Layer).HasColumnName("Layer");
            });
        });
    }
}
