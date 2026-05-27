namespace Wms.Locations
{
    /// <summary>
    /// 库位逻辑状态
    /// </summary>
    public enum LocationStatus
    {
        /// <summary>
        /// 空闲可用
        /// </summary>
        Empty = 0,
        /// <summary>
        /// 已被占用 (有货)
        /// </summary>
        Occupied = 1,
        /// <summary>
        /// 入库锁定中
        /// </summary>
        InboundLocked = 2,
        /// <summary>
        /// 出库锁定中
        /// </summary>
        OutboundLocked = 3
    }
}