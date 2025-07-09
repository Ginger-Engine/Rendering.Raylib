using System.Drawing;
using System.Numerics;
using Engine.Rendering.Drawables;

namespace Engine.Rendering.RaylibBackend.Drawables;

public struct RectangleDrawable() : IDrawable
{
    public Vector2 Position = Vector2.Zero;
    public Vector2 Size = Vector2.Zero;
    public Color Color = Color.White;
}