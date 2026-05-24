namespace Wms;

public static class WmsDbProperties
{
    public static string DbTablePrefix { get; set; } = "Wms";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Wms";
}
