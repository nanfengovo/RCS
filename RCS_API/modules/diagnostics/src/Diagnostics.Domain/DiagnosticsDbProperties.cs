namespace Diagnostics;

public static class DiagnosticsDbProperties
{
    public static string DbTablePrefix { get; set; } = "Diagnostics";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Diagnostics";
}
