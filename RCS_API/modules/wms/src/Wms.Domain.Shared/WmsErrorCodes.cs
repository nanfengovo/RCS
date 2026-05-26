namespace Wms;

public static class WmsErrorCodes
{
    //Add your business exception error codes here...
    // 命名规范：模块名:具体错误
    public const string LocationNotAvailableForInbound = "WMS:000101";
    public const string LocationNotInboundLockedState = "WMS:000102";
}
