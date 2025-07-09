using Engine.Rendering.Drawables;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend.Drawables;

public class LineDrawer : AbstractDrawer<LineDrawable>
{
    public override void Draw(LineDrawable drawable)
    {
        Raylib.DrawLineEx(
            drawable.From,
            drawable.To,
            drawable.Thickness,
            drawable.Color.ToRaylibColor()
        );
    }
}