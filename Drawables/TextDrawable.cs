using System.Drawing;
using System.Numerics;
using Engine.Rendering.Drawables;
using Engine.Rendering.Ui;

namespace Engine.Rendering.RaylibBackend.Drawables;
public enum TextVerticalAlign
{
    Top,
    Middle,
    Bottom
}

public enum TextHorizontalAlign
{
    Left,
    Center,
    Right,
}
public struct TextDrawable() : IDrawable
{
    public string Text = string.Empty;
    public Vector2 Position;
    public float Rotation;
    public Vector2 Scale;
    public float FontSize;
    public Color Color;
    public IFont? Font = null;
    public Vector2 Size;
    public TextVerticalAlign VerticalAlign = TextVerticalAlign.Middle;
    public TextHorizontalAlign HorizontalAlign = TextHorizontalAlign.Left;
}