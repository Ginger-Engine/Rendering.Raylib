using Engine.Rendering.Ui;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend;

public class FontManager : IFontManager
{
    private List<RaylibFont> fonts = [];
    public void Register(FontDescription fontDescription)
    {
        var font = new RaylibFont();
        font.BaseSize = fontDescription.BaseSize;
        font.GlyphPadding = fontDescription.GlyphPadding;
        font.Name = fontDescription.Name;
        font.Filename = fontDescription.Filename;
        font.Font = Raylib.LoadFont(font.Filename);
        fonts.Add(font);
    }

    public IFont Get(string name)
    {
        return fonts.FirstOrDefault<IFont>(f => f.Name == name) ?? throw new KeyNotFoundException("Font not found");
    }
}