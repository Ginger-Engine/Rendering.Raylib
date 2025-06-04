using Engine.Rendering.Cameras;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend;

public class RaylibCamera : ICamera
{
    private readonly Camera2D _camera;

    public RaylibCamera()
    {
        _camera = new Camera2D();
    }
}