using Engine.Rendering.Textures;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend;

public class RaylibTexture : ITexture
{
    public string Id { get; } = string.Empty;
    public Texture2D Raw { get; }
    public int Width => Raw.Width;
    public int Height => Raw.Height;

    public RaylibTexture(Texture2D texture)
    {
        Raw = texture;
    }
}