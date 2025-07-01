using System.Numerics;
using Engine.Rendering.Windows;
using Raylib_cs;

namespace Engine.Rendering.RaylibBackend;

public class RaylibWindow : IWindow
{
    private readonly object _window;

    public RaylibWindow()
    {
        Raylib.InitWindow(800, 600, "Raylib Backend");
    }

    public void SetSize(SizeExpression size)
    {
        var absoluteSize = size.Evaluate(new()
        {
            ["width"] = Raylib.GetScreenWidth(),
            ["height"] = Raylib.GetScreenHeight(),
        });

        Raylib.SetWindowSize((int)MathF.Round(absoluteSize.X), (int)MathF.Round(absoluteSize.Y));
    }

    public Vector2 GetSize()
    {
        return new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
    }

    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            Raylib.SetWindowTitle(_title);
        }
    }
}