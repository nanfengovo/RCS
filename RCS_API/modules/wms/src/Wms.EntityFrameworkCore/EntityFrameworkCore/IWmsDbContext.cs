using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Wms.EntityFrameworkCore;

[ConnectionStringName(WmsDbProperties.ConnectionStringName)]
public interface IWmsDbContext : IEfCoreDbContext
{
}
