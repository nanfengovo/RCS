using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Device.EntityFrameworkCore;

public static class DeviceDbContextModelCreatingExtensions
{
    public static void ConfigureDevice(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
