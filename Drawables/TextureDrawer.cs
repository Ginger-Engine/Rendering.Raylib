using Engine.Rendering.Drawables;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend.Drawables;

public class TextureDrawer : AbstractDrawer<TextureDrawable>
{
    public override void Draw(TextureDrawable texture)
    {
        if (texture.Texture is RaylibTexture texture2D) Raylib.DrawTextureEx(texture2D.Raw, texture.Position, texture.Rotation, texture.Scale.X, Color.Beige);
        else throw new Exception("texture is not a RaylibTexture");
    }
}