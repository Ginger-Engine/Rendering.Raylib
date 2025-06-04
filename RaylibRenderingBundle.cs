using Engine.Core;
using Engine.Rendering.Cameras;
using Engine.Rendering.Materials;
using Engine.Rendering.RaylibBackend.Shaders;
using Engine.Rendering.Shaders;
using Engine.Rendering.Textures;
using GignerEngine.DiContainer;

namespace Engine.Rendering.RaylibBackend;

public class RaylibRenderingBundle : IBundle
{
    public void InstallBindings(DiBuilder builder)
    {
        builder.Bind<IShaderLoader>().From<RaylibShaderLoader>();
        builder.Bind<IMaterialCompiler>().From<MaterialCompiler>();
        builder.Bind<IRenderBackend>().From<RaylibBackend>().Eager();
        builder.Bind<IResourceLoader<ITexture>>().From<RaylibTextureLoader>();
        builder.Bind<ICameraCreator>().From<RaylibCameraCreator>();
    }
}
