using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Dispatch.EntityFrameworkCore;

public static class DispatchDbContextModelCreatingExtensions
{
    public static void ConfigureDispatch(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
