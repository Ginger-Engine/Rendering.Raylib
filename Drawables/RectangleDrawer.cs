using Engine.Rendering.Drawables;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend.Drawables;

public class RectangleDrawer : AbstractDrawer<RectangleDrawable>
{
    public override void Draw(RectangleDrawable drawable)
    {
        Raylib.DrawRectangle(
            (int)drawable.Position.X,
            (int)drawable.Position.Y,
            (int)drawable.Size.X,
            (int)drawable.Size.Y,
            drawable.Color.ToRaylibColor()
        );
    }
}