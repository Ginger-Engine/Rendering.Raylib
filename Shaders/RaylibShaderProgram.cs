using Engine.Rendering.Shaders;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend.Shaders;

public class RaylibShaderProgram : IShaderProgram
{
    private Shader _shader;

    public RaylibShaderProgram(Shader shader)
    {
        _shader = shader;
    }

    public void Use()
    {
        Raylib.BeginShaderMode(_shader);
    }

    public void SetUniform(string name, object value)
    {
        int location = Raylib.GetShaderLocation(_shader, name);

        switch (value)
        {
            case float f:
                Raylib.SetShaderValue(_shader, location, new[] { f }, ShaderUniformDataType.Float);
                break;
            case int i:
                Raylib.SetShaderValue(_shader, location, new[] { i }, ShaderUniformDataType.Int);
                break;
            case System.Numerics.Vector2 v2:
                Raylib.SetShaderValue(_shader, location, new[] { v2.X, v2.Y }, ShaderUniformDataType.Vec2);
                break;
            // расширяем по мере необходимости
            default:
                throw new NotSupportedException($"Unsupported uniform type: {value.GetType()}");
        }
    }
}
