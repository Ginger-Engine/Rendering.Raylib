using System.Numerics;
using Engine.Rendering.Cameras;
using Engine.Rendering.Drawables;
using Engine.Rendering.Textures;
using Engine.Rendering.Ui;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend;

public class RaylibBackend(IEnumerable<IDrawer> drawers) : IRenderBackend
{
    private readonly Dictionary<Type, IDrawer> _drawers = drawers.ToDictionary(drawer => drawer.Type, drawer => drawer);
    public void Init()
    {
    }

    public bool IsRunning() => !Raylib.WindowShouldClose();

    public void Start()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.SkyBlue);
    }

    public void Draw(IDrawable drawable)
    {
        _drawers[drawable.GetType()].Draw(drawable);
    }

    public void DrawLine(Vector2 from, Vector2 to, System.Drawing.Color color, float thickness = 1)
    {
        Raylib.DrawLineEx(
            from,
            to,
            thickness,
            new Color(color.R, color.G, color.B, color.A)
        );
    }
    
    public void SetCamera(ICamera? camera)
    {
        if (camera == null)
        {
            Raylib.EndMode2D();
            return;
        }
        if (camera is not RaylibCamera raylibCamera) throw new Exception("camera is not a RaylibCamera");
        switch (camera.CameraType)
        {
            case ICamera.Type.Orthographic:
                Raylib.BeginMode2D(raylibCamera.Camera);
                break;
            case ICamera.Type.ScreenSpace:
                break;
            default:
                Raylib.EndMode2D();
                break;
        }
    }
    public void Render() {}

    public void End()
    {
        Raylib.EndDrawing();
    }
    public void Shutdown()
    {
        Raylib.CloseWindow();
    }
}
