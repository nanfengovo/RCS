using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Diagnostics.EntityFrameworkCore;

[ConnectionStringName(DiagnosticsDbProperties.ConnectionStringName)]
public class DiagnosticsDbContext : AbpDbContext<DiagnosticsDbContext>, IDiagnosticsDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DiagnosticsDbContext(DbContextOptions<DiagnosticsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDiagnostics();
    }
}
