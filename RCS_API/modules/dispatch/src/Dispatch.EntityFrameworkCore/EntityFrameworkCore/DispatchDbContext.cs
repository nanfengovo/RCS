using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dispatch.EntityFrameworkCore;

[ConnectionStringName(DispatchDbProperties.ConnectionStringName)]
public class DispatchDbContext : AbpDbContext<DispatchDbContext>, IDispatchDbContext
{

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
