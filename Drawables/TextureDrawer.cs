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
            var source = texture.TextureBox.ToRaylibRect();
            var pos = texture.Box.Position;
            var size = texture.Box.Size;
            var scale = texture.Scale;

            var scaledSize = new Vector2(size.X * scale.X, size.Y * scale.Y);

            var dest = new Rectangle
            {
                Position = new Vector2(
                    pos.X + size.X / 2f,
                    pos.Y + size.Y / 2f
                ),
                Size = scaledSize
            };

            var origin = new Vector2(MathF.Abs(dest.Size.X) / 2f, MathF.Abs(dest.Size.Y) / 2f);

            Raylib.DrawTexturePro(
                texture2D?.Raw ?? default,
                source,
                dest.ToRaylibRect(),
                origin,
                texture.Rotation,
                texture.Color.ToRaylibColor()
            );
        }
        else throw new Exception("texture is not a RaylibTexture");
    }
}