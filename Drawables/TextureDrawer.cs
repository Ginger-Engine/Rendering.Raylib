using Engine.Rendering.Drawables;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend.Drawables;

public class TextureDrawer : AbstractDrawer<TextureDrawable>
{
    public override void Draw(TextureDrawable texture)
    {
        var texture2D = texture.Texture as RaylibTexture;
        if (texture.Texture is RaylibTexture or null)
        {
            Raylib.DrawTexturePro(
                texture2D?.Raw ?? default,
                texture.TextureBox.ToRaylibRect(),
                texture.Box.ToRaylibRect(),
                texture.Box.Center,
                texture.Rotation,
                texture.Color.ToRaylibColor()
            );
        }
        else throw new Exception("texture is not a RaylibTexture");
    }
}