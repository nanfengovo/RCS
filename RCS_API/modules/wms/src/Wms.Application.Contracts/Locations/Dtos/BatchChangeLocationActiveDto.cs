using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wms.Locations.Dtos
{
    public class BatchChangeLocationActiveDto
    {
        /// <summary>
        /// 要操作的库位 ID 列表
        /// </summary>
        [Required]
        public List<Guid> LocationIds { get; set; } = new();

        /// <summary>
        /// 目标状态：true 启用，false 禁用
        /// </summary>
        public bool IsActive { get; set; }
    }
}