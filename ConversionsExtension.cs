using Raylib_cs;

namespace Engine.Rendering.RaylibBackend;

internal static class ConversionsExtension
{
    internal static Color ToRaylibColor(this System.Drawing.Color color)
    {
        return new Color(color.R, color.G, color.B, color.A);
    }
    internal static System.Drawing.Color ToSystemColor(this Color color)
    {
        return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
    }
}