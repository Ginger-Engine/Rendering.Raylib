using System.Numerics;
using Engine.Rendering.Cameras;
using Engine.Rendering.Textures;
using Engine.Rendering.Ui;
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

    public void DrawTexture(ITexture texture, Vector2 position, float rotation, Vector2 scale)
    {
        if (texture is RaylibTexture texture2D) Raylib.DrawTextureEx(texture2D.Raw, position, rotation, scale.X, Color.Beige);
        else throw new Exception("texture is not a RaylibTexture");
        Console.WriteLine($"{position}, {rotation}, {scale}");
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
    

    public void DrawText(string text, Vector2 position, float rotation, Vector2 scale, float fontSize, System.Drawing.Color color, IFont? font = null)
    {
        Font raylibFontRaw;
        if (font is not null)
        {
            if (font is not RaylibFont raylibFont) throw new Exception("font is not a RaylibFont");
            raylibFontRaw = raylibFont.Font;
        }
        else
        {
            raylibFontRaw = Raylib.GetFontDefault();
        }

        fontSize = fontSize > 0 ? fontSize : raylibFontRaw.BaseSize;
        Vector2 origin = Raylib.MeasureTextEx(raylibFontRaw, text, fontSize, 1) / 2;
        Raylib.DrawTextPro(
            raylibFontRaw,
            text,
            position,
            origin,
            rotation,
            fontSize,
            1f,
            new Color(color.R / 255f, color.G / 255f, color.B / 255f, 1f) 
        );
    }
}
