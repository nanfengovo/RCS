using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Diagnostics.EntityFrameworkCore;

[ConnectionStringName(DiagnosticsDbProperties.ConnectionStringName)]
public interface IDiagnosticsDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
