using System.Numerics;
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
            if (drawable.Font is not RaylibFont raylibFont) 
                throw new Exception("font is not a RaylibFont");
            raylibFontRaw = raylibFont.Font;
        }
        else
        {
            raylibFontRaw = Raylib.GetFontDefault();
        }

        var fontSize = drawable.FontSize > 0 ? drawable.FontSize : raylibFontRaw.BaseSize;
        var textSize = Raylib.MeasureTextEx(raylibFontRaw, drawable.Text, fontSize, 1);

        float dx = drawable.HorizontalAlign switch
        {
            TextHorizontalAlign.Left => 0,
            TextHorizontalAlign.Center => -drawable.Size.X / 4 + textSize.X / 2,
            TextHorizontalAlign.Right => -drawable.Size.X / 2 + textSize.X,
            _ => 0
        };

        float dy = drawable.VerticalAlign switch
        {
            TextVerticalAlign.Top => 0,
            TextVerticalAlign.Middle => -drawable.Size.Y / 4 + textSize.Y / 2,
            TextVerticalAlign.Bottom => -drawable.Size.Y / 2 + textSize.Y,
            _ => 0
        };
        
        var origin = new Vector2(dx, dy);
        Raylib.DrawTextPro(
            raylibFontRaw,
            drawable.Text,
            drawable.Position,
            origin,
            drawable.Rotation,
            fontSize,
            1,
            drawable.Color.ToRaylibColor()
        );
    }

}
