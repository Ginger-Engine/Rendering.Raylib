using System.Numerics;
using Engine.Core.Behaviours;
using Engine.Core.Entities;
using Engine.Core.Transform;
using Engine.Rendering.RaylibBackend.Drawables;
using Engine.Rendering.Windows;

namespace Engine.Rendering.RaylibBackend.Sprites;

public class SpriteRenderer : IEntityBehaviour
{
    private readonly RenderQueue _renderQueue;
    private readonly IWindow _window;

    public SpriteRenderer(RenderQueue renderQueue, IWindow window)
    {
        _renderQueue = renderQueue;
        _window = window;
    }

    public void OnStart(Entity entity)
    {
        InitRenderable(entity, _window);
        entity.SubscribeComponentChange<SpriteComponent>(e =>
        {
            entity.Modify((ref RenderableComponent renderableComponent) =>
            {
                var drawable = (TextureDrawable)renderableComponent.Renderable.Drawable;
                drawable.Texture = e.newValue.Texture;
                drawable.Color = e.newValue.Color;
                renderableComponent.Renderable.Drawable = drawable;
            });
        });
        entity.SubscribeComponentChange<RectangleComponent>(e =>
        {
            var size = EvaluateSize(entity, e.newValue.Size, _window);
            entity.Modify((ref RenderableComponent renderableComponent) =>
            {
                var drawable = (TextureDrawable)renderableComponent.Renderable.Drawable;
                drawable.Box.Size = size;
                drawable.TextureBox.Size = size;
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

    private static Vector2? GetParentSize(Entity entity, IWindow window)
    {
        if (entity.Parent == null)
            return window.GetSize();
        var rectComponent = entity.GetComponent<RectangleComponent>();
        return EvaluateSize(entity.Parent, rectComponent.Size, window);
    }

    private static Vector2 EvaluateSize(Entity entity, SizeExpression sizeExpression, IWindow window)
    {
        var variables = new Dictionary<string, float>();
        var parentSize = GetParentSize(entity, window);
        if (parentSize != null)
        {
            variables["parent.width"] = parentSize.Value.X;
            variables["parent.height"] = parentSize.Value.Y;
        }
        // TODO: screen.width, screen.height
        return sizeExpression.Evaluate(variables);
    }

    private static void InitRenderable(Entity entity, IWindow window)
    {
        var renderableComponent = entity.GetComponent<RenderableComponent>();
        var transformComponent = entity.GetComponent<TransformComponent>();
        var spriteComponent = entity.GetComponent<SpriteComponent>();
        var rectComponent = entity.GetComponent<RectangleComponent>();
        var size = EvaluateSize(entity, rectComponent.Size, window);
        renderableComponent.Renderable = new Renderable
        {
            Layer = renderableComponent.Layer,
            Entity = entity,
            Drawable = new TextureDrawable
            {
                Texture = spriteComponent.Texture,
                Box = new Rectangle { Position = transformComponent.WorldTransform.Position, Size = size},
                TextureBox = new Rectangle { Position = Vector2.Zero, Size = size},
                Rotation = transformComponent.WorldTransform.Rotation,
                Scale = transformComponent.WorldTransform.Scale,
                Color = spriteComponent.Color,
            },
        };
        entity.ApplyComponent(renderableComponent);
    }
}