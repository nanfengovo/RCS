using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Wms.Locations
{
    public class Location : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 库位编码，全局唯一
        /// </summary>
        public string LocationCode { get; private set;} = default!;

        /// <summary>
        /// 库位类型
        /// </summary>
        public LocationType LocationType { get; private set;}

        /// <summary>
        /// 所属库区 （跨聚合使用）
        /// </summary>
        public Guid ZoneId { get; private set;}

        /// <summary>
        /// 库位坐标 (行、列、层)
        /// </summary>
        public LocationCoordinate LocationCoordinate { get; private set;}   

        /// <summary>
        /// 库位逻辑状态
        /// </summary>
        public LocationStatus LocationStatus { get; private set;}

        /// <summary>
        /// 库位物理状态
        /// </summary>
        public PhysicalSensorStatus? PhysicalSensorStatus { get; private set;}

        /// <summary>
        /// 绑定的底层设备暗号 (用于和 Device 模块做事件驱动联动)
        /// </summary>
        public string? AssociatedDeviceCode { get; private set;}

        /// <summary>
        /// 绑定的载体 ID
        /// </summary>
        public Guid? CarrierId { get; private set;}

        /// <summary>
        /// 当前锁定该库位的任务 ID
        /// 允许为空 (当库位处于 Empty 或 Occupied 时，此字段应为 null)
        /// </summary>
        public Guid? LockingTaskId { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// EF Core 需要一个无参构造函数
        /// </summary>
        private Location()
        {
            LocationCoordinate = null!;
        }

        public Location(
            Guid id,
            string locationCode,
            LocationType locationType,
            Guid zoneId,
            LocationCoordinate locationCoordinate,
            LocationStatus locationStatus,
            PhysicalSensorStatus? physicalSensorStatus = null,
            string? associatedDeviceCode = null,
            Guid? carrierId = null,
            Guid? lockingTaskId = null,
            bool isActive = true
            ) : base(id)
        {
            LocationCode = locationCode;
            LocationType = locationType;
            ZoneId = zoneId;
            LocationCoordinate = locationCoordinate;
            LocationStatus = locationStatus;
            PhysicalSensorStatus = physicalSensorStatus;
            AssociatedDeviceCode = associatedDeviceCode;
            CarrierId = carrierId;
            LockingTaskId = lockingTaskId;
            IsActive = isActive;
        }

        // --- 领域行为（充血模型）---

        /// <summary>
        /// 锁定库位用于入库任务
        /// </summary>
        /// <param name="taskId"></param>
        public void LockForInbound(Guid taskId)
        {
            this.LockingTaskId = taskId;
            if(LocationStatus != LocationStatus.Empty || !IsActive)
            {
                throw new BusinessException(WmsErrorCodes.LocationNotAvailableForInbound)
                .WithData("LocationCode", LocationCode)
                .WithData("currentStatus",LocationStatus);
            }

            LocationStatus = LocationStatus.InboundLocked;
        }

        /// <summary>
        /// 完成占用
        /// </summary>
        /// <param name="carrierId"></param>
        public void CompleteInbound(Guid carrierId)
        {
            if(LocationStatus != LocationStatus.InboundLocked)
            {
                throw new BusinessException(WmsErrorCodes.LocationNotInboundLockedState)
                .WithData("LocationCode", LocationCode)
                .WithData("currentStatus",LocationStatus);
            }
            this.CarrierId = carrierId;
            LocationStatus = LocationStatus.Occupied;
        }
    }
}