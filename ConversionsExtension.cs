using System.Numerics;
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

    internal static Rectangle ToEngineRect(this Raylib_cs.Rectangle rectangle)
    {
        return new Rectangle {
            Position = new Vector2(rectangle.Position.X, rectangle.Position.Y), 
            Size = new Vector2(rectangle.Size.X, rectangle.Size.Y)
        };
    }

    internal static Raylib_cs.Rectangle ToRaylibRect(this Rectangle rectangle)
    {
        return new Raylib_cs.Rectangle
        {
            Position = new Vector2(rectangle.Position.X, rectangle.Position.Y),
            Size = new Vector2(rectangle.Size.X, rectangle.Size.Y),
        };
    }
}