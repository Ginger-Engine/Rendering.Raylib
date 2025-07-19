using System.Numerics;
using Engine.Core.Behaviours;
using Engine.Core.Entities;
using Engine.Core.Transform;
using Engine.Rendering.RaylibBackend.Drawables;
using Engine.Rendering.Ui;
using Engine.Rendering.Windows;

namespace Engine.Rendering.RaylibBackend.Labels;

public class LabelRenderer : IEntityBehaviour
{
    private readonly RenderQueue _renderQueue;
    private readonly IFontManager _fontManager;
    private readonly IWindow _window;

    public LabelRenderer(RenderQueue renderQueue, IFontManager fontManager, IWindow window)
    {
        _renderQueue = renderQueue;
        _fontManager = fontManager;
        _window = window;
    }

    public void OnStart(Entity entity)
    {
        InitRenderable(entity);
        UpdateRenderable(entity);

        entity.SubscribeComponentChange<LabelComponent>(_ => UpdateRenderable(entity));
        entity.SubscribeComponentChange<TransformComponent>(_ => UpdateRenderable(entity));
    }

    public void OnUpdate(Entity entity, float dt)
    {
        _renderQueue.Add(entity.GetComponent<RenderableComponent>().Renderable);
    }

    private static Vector2? GetParentSize(Entity entity, IWindow window)
    {
        if (entity.Parent == null)
            return window.GetSize();
        if (!entity.HasComponent<RenderableComponent>())
            return null;
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
        else
        {
            variables["parent.width"] = window.GetSize().X;
            variables["parent.height"] = window.GetSize().Y;
        }
        
        return sizeExpression.Evaluate(variables);
    }

    private void InitRenderable(Entity entity)
    {
        var textComponent = entity.GetComponent<LabelComponent>();
        var transform = entity.GetComponent<TransformComponent>();
        var renderableComponent = entity.GetComponent<RenderableComponent>();
        var rectComponent = entity.GetComponent<RectangleComponent>();
        
        var size = EvaluateSize(entity, rectComponent.Size, _window);

        var drawable = new TextDrawable
        {
            Text = textComponent.Text,
            Font = _fontManager.Get(textComponent.Font),
            FontSize = textComponent.FontSize,
            Color = textComponent.Color,
            Position = transform.WorldTransform.Position,
            Rotation = transform.WorldTransform.Rotation,
            Scale = transform.WorldTransform.Scale,
            Size = size,
        };
        renderableComponent.Renderable = new Renderable
        {
            Entity = entity,
            Drawable = drawable,
            Layer = renderableComponent.Layer,
        };
        
        entity.ApplyComponent(renderableComponent);
    }

    private void UpdateRenderable(Entity entity)
    {
        var rectComponent = entity.GetComponent<RectangleComponent>();
        var textComponent = entity.GetComponent<LabelComponent>();
        var transform = entity.GetComponent<TransformComponent>();
        var size = EvaluateSize(entity, rectComponent.Size, _window);
        entity.Modify((ref RenderableComponent renderableComponent) =>
        {
            var drawable = (TextDrawable)renderableComponent.Renderable.Drawable;
            drawable.Text = textComponent.Text;
            drawable.Font = _fontManager.Get(textComponent.Font);
            drawable.FontSize = textComponent.FontSize;
            drawable.Color = textComponent.Color;
            drawable.Position = transform.WorldTransform.Position;
            drawable.Rotation = transform.WorldTransform.Rotation;
            drawable.Scale = transform.WorldTransform.Scale;
            drawable.Size = size;
            drawable.HorizontalAlign = textComponent.HorizontalAlign;
            drawable.VerticalAlign = textComponent.VerticalAlign;
            renderableComponent.Renderable.Drawable = drawable;
        });
    }
}