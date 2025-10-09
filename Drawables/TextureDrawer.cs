using System.Numerics;
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
            var dest = new Rectangle
            {
                Position = new(texture.Box.Position.X + texture.Box.Size.X / 2f,
                    texture.Box.Position.Y + texture.Box.Size.Y / 2f),
                Size = texture.Box.Size,
            };
            var origin = new Vector2(dest.Size.X / 2f, dest.Size.Y / 2f);
            Raylib.DrawTexturePro(
                texture2D?.Raw ?? default,
                texture.TextureBox.ToRaylibRect(),
                dest.ToRaylibRect(),
                origin,
                texture.Rotation,
                texture.Color.ToRaylibColor()
            );
        }
        else throw new Exception("texture is not a RaylibTexture");
    }
}