using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Wms.EntityFrameworkCore;

[ConnectionStringName(WmsDbProperties.ConnectionStringName)]
public interface IWmsDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
