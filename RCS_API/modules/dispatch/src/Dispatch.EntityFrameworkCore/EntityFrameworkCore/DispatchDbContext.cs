using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dispatch.EntityFrameworkCore;

[ConnectionStringName(DispatchDbProperties.ConnectionStringName)]
public class DispatchDbContext : AbpDbContext<DispatchDbContext>, IDispatchDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DispatchDbContext(DbContextOptions<DispatchDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDispatch();
    }
}
