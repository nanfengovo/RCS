namespace Wms.Locations
{
    /// <summary>
    /// 物理传感器状态
    /// </summary>
    public enum PhysicalSensorStatus
    {
        /// <summary>
        /// 未知 (比如设备断线)
        /// </summary>  
        Unknown = 0,

        /// <summary>
        /// 物理无货
        /// </summary>
        NoItem = 1,

        /// <summary>
        /// 物理有货
        /// </summary>
        HasItem = 2,

        /// <summary>
        /// 传感器异常/报警
        /// </summary>
        Error = 3
    }
}