namespace Device;

public static class DeviceDbProperties
{
    public static string DbTablePrefix { get; set; } = "Device";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Device";
}
