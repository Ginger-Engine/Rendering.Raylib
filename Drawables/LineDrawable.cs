using System.Drawing;
using System.Numerics;
using Engine.Rendering.Drawables;

namespace Engine.Rendering.RaylibBackend.Drawables;

public struct LineDrawable() : IDrawable
{
    public Vector2 From;
    public Vector2 To;
    public Color Color;
    public float Thickness = 1;
}
