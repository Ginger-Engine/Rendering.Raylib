using Engine.Core.Behaviours;
using Engine.Core.Entities;
using Engine.Core.Transform;
using Engine.Rendering.RaylibBackend.Drawables;

namespace Engine.Rendering.RaylibBackend.Sprites;

public class SpriteRenderer : IEntityBehaviour
{
    private readonly RenderQueue _renderQueue;

    public SpriteRenderer(RenderQueue renderQueue)
    {
        _renderQueue = renderQueue;
    }

    public void OnStart(Entity entity)
    {
        InitRenderable(entity);
        UpdateRenderable(entity);
        entity.SubscribeComponentChange<SpriteComponent>((value, oldValue) =>
        {
            UpdateRenderable(entity);
        });
        entity.SubscribeComponentChange<WorldTransformComponent>((value, oldValue) =>
        {
            UpdateRenderable(entity);
        });
        
        _renderQueue.Add(entity.GetComponent<RenderableComponent>().Renderable);
    }

    public void OnUpdate(Entity entity, float dt)
    {
    }

    public void InitRenderable(Entity entity)
    {
        var renderableComponent = entity.GetComponent<RenderableComponent>();
        renderableComponent.Renderable = new Renderable
        {
            Layer = renderableComponent.Layer,
            Entity = entity,
            Drawable = new TextureDrawable(),
        };
        entity.ApplyComponent(renderableComponent);
    }

    public void UpdateRenderable(Entity entity)
    {
        var spriteComponent = entity.GetComponent<SpriteComponent>();
        var transformComponent = entity.GetComponent<WorldTransformComponent>();

        entity.Modify((ref RenderableComponent renderableComponent) =>
        {
            var drawable = (TextureDrawable)renderableComponent.Renderable.Drawable;
            drawable.Texture = spriteComponent.Texture;
            drawable.Position = transformComponent.Position;
            drawable.Rotation = transformComponent.Rotation;
            drawable.Scale = transformComponent.Scale;
            renderableComponent.Renderable.Drawable = drawable;
        });
    }
}