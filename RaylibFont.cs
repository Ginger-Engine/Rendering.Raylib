using Engine.Rendering.Ui;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend;

public class RaylibFont : IFont
{
    public Font Font;
    public string Name { get; set; }
    public string Filename { get; set; }
    public float BaseSize { get; set; }
    public float GlyphPadding { get; set; }
}