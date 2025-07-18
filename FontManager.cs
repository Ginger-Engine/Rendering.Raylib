using Engine.Core;
using Engine.Core.Helper;
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
        int[] codepoints = Enumerable.Range(32, 512).ToArray();
        if (!File.Exists(PathHelper.Normalize(font.Filename)))
        {
            throw new GingerException("Font not found: " + font.Filename);
        }
        font.Font = Raylib.LoadFontEx(PathHelper.Normalize(font.Filename), (int)font.BaseSize, codepoints, codepoints.Length);
        fonts.Add(font);
    }

    public IFont Get(string name)
    {
        return fonts.FirstOrDefault<IFont>(f => f.Name == name) ?? null; //throw new KeyNotFoundException("Font not found");
    }
}