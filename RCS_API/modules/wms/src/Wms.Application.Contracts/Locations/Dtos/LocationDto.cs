using System;
using Volo.Abp.Application.Dtos;

namespace Wms.Locations.Dtos
{
    public class LocationDto : EntityDto<Guid>
    {
        public string LocationCode { get; set; } = default!;
        public LocationType LocationType { get; set; }
        public Guid ZoneId { get; set; }
        
        public int Row { get; set; }
        public int Column { get; set; }
        public int Layer { get; set; }
        
        public LocationStatus LocationStatus { get; set; }
        public bool IsActive { get; set; }
    }
}