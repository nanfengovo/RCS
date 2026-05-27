using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Diagnostics.EntityFrameworkCore;

public static class DiagnosticsDbContextModelCreatingExtensions
{
    public static void ConfigureDiagnostics(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
