using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Diagnostics.EntityFrameworkCore;

[ConnectionStringName(DiagnosticsDbProperties.ConnectionStringName)]
public interface IDiagnosticsDbContext : IEfCoreDbContext
{
}
