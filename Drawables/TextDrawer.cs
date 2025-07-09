using Engine.Rendering.Drawables;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend.Drawables;

public class TextDrawer : AbstractDrawer<TextDrawable>
{
    public override void Draw(TextDrawable drawable)
    {
        Font raylibFontRaw;
        if (drawable.Font is not null)
        {
            if (drawable.Font is not RaylibFont raylibFont) throw new Exception("font is not a RaylibFont");
            raylibFontRaw = raylibFont.Font;
        }
        else
        {
            raylibFontRaw = Raylib.GetFontDefault();
        }

        var fontSize = drawable.FontSize > 0 ? drawable.FontSize : raylibFontRaw.BaseSize;
        var origin = Raylib.MeasureTextEx(raylibFontRaw, drawable.Text, fontSize, 1) / 2;
        Raylib.DrawTextPro(
            raylibFontRaw,
            drawable.Text,
            drawable.Position,
            origin,
            drawable.Rotation,
            fontSize,
            1f,
            drawable.Color.ToRaylibColor()
        );
    }
}