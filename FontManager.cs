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
        var font = new RaylibFont
        {
            BaseSize = fontDescription.BaseSize,
            GlyphPadding = fontDescription.GlyphPadding,
            Name = fontDescription.Name,
            Filename = fontDescription.Filename
        };
        var codepoints = Enumerable.Range(0, 512).ToArray();
        if (!File.Exists(PathHelper.Normalize(font.Filename)))
        {
            throw new GingerException("Font not found: " + font.Filename);
        }
        font.Font = Raylib.LoadFontEx(PathHelper.Normalize(font.Filename), (int)font.BaseSize, codepoints, codepoints.Length);

        if (!Raylib.IsFontValid(font.Font))
        {
            throw new GingerException("Font is not valid: " + font.Filename);
        }
        
        if (font.Font.Texture.Id == 0)
        {
            throw new GingerException("Font texture not created: " + font.Filename);
        }
        fonts.Add(font);
    }

    public IFont Get(string name)
    {
        return fonts.FirstOrDefault<IFont>(f => f.Name == name) ?? null;
    }
}