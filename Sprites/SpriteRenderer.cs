using System.Numerics;
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
                drawable.Color = e.newValue.Color;
                drawable.Box.Size = e.newValue.Size;
                drawable.TextureBox.Size = e.newValue.Size;
                renderableComponent.Renderable.Drawable = drawable;
            });
        });
        entity.SubscribeComponentChange<TransformComponent>(e =>
        {
            entity.Modify((ref RenderableComponent renderableComponent) =>
            {
                var drawable = (TextureDrawable)renderableComponent.Renderable.Drawable;
                drawable.Box.Position = e.newValue.WorldTransform.Position;
                drawable.TextureBox.Position = e.newValue.WorldTransform.Position;
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
                Box = new Rectangle { Position = transformComponent.WorldTransform.Position, Size = spriteComponent.Size},
                TextureBox = new Rectangle { Position = Vector2.Zero, Size = spriteComponent.Size},
                Rotation = transformComponent.WorldTransform.Rotation,
                Scale = transformComponent.WorldTransform.Scale,
                Color = spriteComponent.Color,
            },
        };
        entity.ApplyComponent(renderableComponent);
    }
}