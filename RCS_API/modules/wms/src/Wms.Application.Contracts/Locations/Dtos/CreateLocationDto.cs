using System;
using System.ComponentModel.DataAnnotations;

namespace Wms.Locations.Dtos
{
    public class CreateLocationDto
    {
        [Required]
        [MaxLength(64)]
        public string LocationCode { get; set;} = default!;

        public LocationType LocationType { get; set; }

        public Guid ZoneId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public int Layer { get; set; }
    }
}