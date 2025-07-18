using System.Drawing;
using System.Numerics;
using Engine.Core;
using Engine.Rendering.Textures;
using Engine.Rendering;

namespace Engine.Rendering.RaylibBackend.Sprites;

public struct SpriteComponent() : IComponent
{
    public ITexture Texture;
    public Color Color = Color.White;
}