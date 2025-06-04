using Engine.Rendering.Cameras;

namespace Engine.Rendering.RaylibBackend;

public class RaylibCameraCreator : ICameraCreator
{
    public ICamera Create()
    {
        return new RaylibCamera();
    }
}
