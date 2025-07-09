using System.Drawing;
using Engine.Core;

namespace Engine.Rendering.RaylibBackend.Labels;

public struct LabelComponent : IComponent
{
    public string Text;
    public string Font;
    public int FontSize;
    public Color Color;
}