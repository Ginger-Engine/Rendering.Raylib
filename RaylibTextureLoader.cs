using Engine.Core.Serialization;
using Engine.Rendering.Textures;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend;

public class RaylibTextureLoader : IResourceLoader<ITexture>, ITypeResolver<ITexture>
{
    public ITexture Load(string path)
    {
        if (!File.Exists(path))
            throw new Exception($"Texture file not found: {path}");
        if (!Raylib.IsWindowReady())
            throw new InvalidOperationException("Raylib not initialized — call InitWindow before loading textures.");
        var texture = Raylib.LoadTexture(path);
        return new RaylibTexture(texture);
    }

    public ITexture? Resolve(object raw)
    {
        if (raw is not string path)
            return null;

        return Load(path);
    }

    object? ITypeResolver.Resolve(Type type, object raw)
    {
        return Resolve(raw);
    }
}