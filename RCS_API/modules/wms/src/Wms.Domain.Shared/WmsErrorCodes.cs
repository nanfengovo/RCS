namespace Wms;

public static class WmsErrorCodes
{
    //Add your business exception error codes here...
    // 命名规范：模块名:具体错误

    /// <summary>
    /// 库位不满足入库条件（不空闲或未启用）
    /// </summary>
    public const string LocationNotAvailableForInbound = "WMS:000101";

    /// <summary>
    /// 库位不满足入库锁定条件（不空闲或未启用）
    /// </summary>
    public const string LocationNotInboundLockedState = "WMS:000102";

    /// <summary>
    /// 无法禁用库位（库位被占用时不允许禁用）
    /// </summary>
    public const string CannotDisableOccupiedLocation = "WMS:000103";

    /// <summary>
    /// 库位编码已存在
    /// </summary>

    public const string LocationCodeAlreadyExists = "WMS:000104";

}
