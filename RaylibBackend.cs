using System.Numerics;
using Engine.Rendering.Cameras;
using Engine.Rendering.Textures;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend;

public class RaylibBackend : IRenderBackend
{
    public RaylibBackend()
    {
        Raylib.InitWindow(800, 600, "Raylib Backend");
    }
    public void Init()
    {
    }

    public bool IsRunning() => !Raylib.WindowShouldClose();

    public void Start()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.SkyBlue);
    }

    public void DrawTexture(ITexture texture, Vector2 position)
    {
        if (texture is RaylibTexture texture2D) Raylib.DrawTexture(texture2D.Raw, (int)position.X, (int)position.Y, Color.Beige);
        else throw new Exception("texture is not a RaylibTexture");
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
