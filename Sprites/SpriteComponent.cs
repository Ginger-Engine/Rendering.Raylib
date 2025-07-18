using System.Drawing;
using System.Numerics;
using Engine.Core;
using Engine.Rendering.Textures;

namespace Engine.Rendering.RaylibBackend.Sprites;

public struct SpriteComponent() : IComponent
{
    public ITexture Texture;
    public Vector2 Size;
    public Color Color = Color.White;
}