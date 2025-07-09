using System.Numerics;
using Engine.Rendering.Drawables;
using Engine.Rendering.Textures;

namespace Engine.Rendering.RaylibBackend.Drawables;

public struct TextureDrawable(
) : IDrawable
{
    public ITexture Texture;
    public float Rotation = 0;
    public Vector2 Position = default;
    public Vector2 Scale = default;
}