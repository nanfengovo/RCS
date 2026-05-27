using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dispatch.EntityFrameworkCore;

[ConnectionStringName(DispatchDbProperties.ConnectionStringName)]
public interface IDispatchDbContext : IEfCoreDbContext
{
}
