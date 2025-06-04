using Engine.Rendering.Textures;

namespace Engine.Rendering.RaylibBackend;

using Raylib_cs;

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