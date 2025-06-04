using Engine.Rendering.Shaders;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend.Shaders;

public class RaylibShaderLoader : IShaderLoader
{
    public IShaderProgram Load(string shaderName)
    {
        var shader = Raylib.LoadShader(null, shaderName);
        return new RaylibShaderProgram(shader);
    }
}
