using System;
using Shouldly;
using Volo.Abp;
using Xunit;

namespace Wms.Locations
{
    public class Location_Tests
    {
        [Fact]
        public void ChangeLocationAble_Should_Success_When_Empty()
        {
            // 1. Arrange (准备)：创建一个空库位
            var location = new Location(
                Guid.NewGuid(), 
                "A-01-01", 
                LocationType.Erack, 
                Guid.NewGuid(), 
                new LocationCoordinate(1, 1, 1), 
                LocationStatus.Empty);


            // 2. Act (执行)：尝试禁用它
            location.ChangeLocationAble(false);

            // 3. Assert (断言)：验证状态确实改变了
            location.IsActive.ShouldBeFalse();
        }

        [Fact]
        public void ChangeLocationAble_Should_Throw_Exception_When_Occupied()
        {
            // 1. Arrange (准备)：创建一个有货的库位
            var location = new Location(
                Guid.NewGuid(), 
                "A-01-02", 
                LocationType.Erack, 
                Guid.NewGuid(), 
                new LocationCoordinate(1, 1, 2), 
                LocationStatus.Occupied); // 👈 关键点：设置状态为 Occupied
                

            // 2. Act & Assert (执行并断言)：
            // 期望在调用 ChangeLocationAble(false) 时，抛出 BusinessException
            var exception = Should.Throw<BusinessException>(() => 
            {
                location.ChangeLocationAble(false);
            });

            // 3. 验证异常的错误码是否精确匹配
            exception.Code.ShouldBe(WmsErrorCodes.CannotDisableOccupiedLocation);
            
            // 验证 WithData 携带的上下文信息是否正确
            exception.Data["LocationCode"].ShouldBe("A-01-02");
        }

        // ==========================================
        // 测试：LockForInbound (锁定库位用于入库)
        // ==========================================

        [Fact]
        public void LockForInbound_Should_Success_When_Empty_And_Active()
        {
            // 1. Arrange: 准备一个【空闲】且【启用】的库位
            var location = new Location(Guid.NewGuid(), "A-01-01", LocationType.Erack, Guid.NewGuid(), new LocationCoordinate(1, 1, 1), LocationStatus.Empty, isActive: true);
            var taskId = Guid.NewGuid();

            // 2. Act: 执行锁定入库
            location.LockForInbound(taskId);

            // 3. Assert: 验证状态和任务ID是否正确更新
            location.LocationStatus.ShouldBe(LocationStatus.InboundLocked);
            location.LockingTaskId.ShouldBe(taskId);
        }
    }
}