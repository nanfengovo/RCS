using System;
using System.Threading.Tasks;

namespace Wms.Locations.Events
{
    // 这是一个事件载体：库位被锁定事件
    public class LocationLockedEto
    {
        public Guid LocationId { get; set; }
        public Guid TaskId { get; set; }
        public DateTime OccurredTime { get; set; }
    }
}