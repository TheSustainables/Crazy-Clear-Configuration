namespace CrazyClearConfiguration.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static string AsString(this object? obj) => obj?.ToString() ?? string.Empty;
        public static int AsInt(this object? obj) => int.Parse(obj?.ToString() ?? "0");
        public static bool AsBoolean(this object? obj) => bool.Parse(obj?.ToString() ?? "false");
    }
}
