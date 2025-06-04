using System.Numerics;
using Engine.Rendering.Cameras;
using Engine.Rendering.Layers;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend;

public class RaylibCamera : ICamera
{
    public Camera2D Camera;
    public Vector2 Position
    {
        get => Camera.Target;
        set => Camera.Target = value;
    }

    public float Rotation
    {
        get => Camera.Rotation;
        set => Camera.Rotation = value;
    }

    public float Zoom
    {
        get => Camera.Zoom;
        set => Camera.Zoom = value;
    }

    public Layer[] Layers { get; set; }
    public ICamera.Type CameraType { get; set; }

    public RaylibCamera()
    {
        Camera = new Camera2D();
    }
}
