using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Wms.Locations.Events;

namespace Wms.Locations.Handlers
{
    public class LocationAuditLogHandler : ILocalEventHandler<LocationLockedEto>, ITransientDependency
    {
        private readonly ILogger<LocationAuditLogHandler> _logger;
        private readonly IAuditingManager _auditingManager;

        public LocationAuditLogHandler(ILogger<LocationAuditLogHandler> logger, IAuditingManager auditingManager)
        {
            _logger = logger;
            _auditingManager = auditingManager;
        }

        public async Task HandleEventAsync(LocationLockedEto eventData)
        {
            // 1. 记文本日志 (Serilog 会把它写进控制台和 txt 文件)
            _logger.LogInformation(
                "【WMS业务日志】库位 {LocationId} 已被任务 {TaskId} 锁定。时间: {Time}", 
                eventData.LocationId, eventData.TaskId, eventData.OccurredTime);

            // 2. 记数据库审计日志 (写进你刚才截图的 AbpAuditLogActions 表里)
            if (_auditingManager.Current != null)
            {
                _auditingManager.Current.Log.Actions.Add(new AuditLogActionInfo
                {
                    ServiceName = "WMS模块-库位管理",
                    MethodName = "锁定入库事件",
                    Parameters = $"{{ 'LocationId': '{eventData.LocationId}' }}",
                    ExecutionTime = eventData.OccurredTime,
                    ExecutionDuration = 0
                });
            }
            
            await Task.CompletedTask;
        }
    }
}