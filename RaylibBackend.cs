using System.Numerics;
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
        else throw new System.Exception("texture is not a RaylibTexture");
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
