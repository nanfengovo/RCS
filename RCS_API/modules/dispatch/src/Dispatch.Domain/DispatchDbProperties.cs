namespace Dispatch;

public static class DispatchDbProperties
{
    public static string DbTablePrefix { get; set; } = "Dispatch";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Dispatch";
}
