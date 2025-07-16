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
        entity.SubscribeComponentChange<SpriteComponent>(e =>
        {
            entity.Modify((ref RenderableComponent renderableComponent) =>
            {
                var drawable = (TextureDrawable)renderableComponent.Renderable.Drawable;
                drawable.Texture = e.newValue.Texture;
                renderableComponent.Renderable.Drawable = drawable;
            });
        });
        entity.SubscribeComponentChange<TransformComponent>(e =>
        {
            entity.Modify((ref RenderableComponent renderableComponent) =>
            {
                var drawable = (TextureDrawable)renderableComponent.Renderable.Drawable;
                drawable.Position = e.newValue.WorldTransform.Position;
                drawable.Rotation = e.newValue.WorldTransform.Rotation;
                drawable.Scale = e.newValue.WorldTransform.Scale;
                renderableComponent.Renderable.Drawable = drawable;
            });
        });
        
        _renderQueue.Add(entity.GetComponent<RenderableComponent>().Renderable);
    }

    public void OnUpdate(Entity entity, float dt)
    {
    }

    private static void InitRenderable(Entity entity)
    {
        var renderableComponent = entity.GetComponent<RenderableComponent>();
        var transformComponent = entity.GetComponent<TransformComponent>();
        var spriteComponent = entity.GetComponent<SpriteComponent>();
        renderableComponent.Renderable = new Renderable
        {
            Layer = renderableComponent.Layer,
            Entity = entity,
            Drawable = new TextureDrawable
            {
                Texture = spriteComponent.Texture,
                Position = transformComponent.WorldTransform.Position,
                Rotation = transformComponent.WorldTransform.Rotation,
                Scale = transformComponent.WorldTransform.Scale,
            },
        };
        entity.ApplyComponent(renderableComponent);
    }
}