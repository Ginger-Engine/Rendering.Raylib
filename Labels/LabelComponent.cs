using System.Drawing;
using System.Numerics;
using Engine.Core;
using Engine.Rendering.RaylibBackend.Drawables;

namespace Engine.Rendering.RaylibBackend.Labels;

public struct LabelComponent() : IComponent
{
    public string Text;
    public string Font;
    public int FontSize;
    public Color Color = Color.Black;
    public Vector2 Size;
    public TextHorizontalAlign HorizontalAlign = TextHorizontalAlign.Left;
    public TextVerticalAlign VerticalAlign = TextVerticalAlign.Middle;
}