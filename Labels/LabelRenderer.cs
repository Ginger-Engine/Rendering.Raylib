using Engine.Core.Behaviours;
using Engine.Core.Entities;
using Engine.Core.Transform;
using Engine.Rendering.RaylibBackend.Drawables;
using Engine.Rendering.Ui;

namespace Engine.Rendering.RaylibBackend.Labels;

public class LabelRenderer : IEntityBehaviour
{
    private readonly RenderQueue _renderQueue;
    private readonly IFontManager _fontManager;

    public LabelRenderer(RenderQueue renderQueue, IFontManager fontManager)
    {
        _renderQueue = renderQueue;
        _fontManager = fontManager;
    }

    public void OnStart(Entity entity)
    {
        InitRenderable(entity);
        UpdateRenderable(entity);

        entity.SubscribeComponentChange<LabelComponent>((_, _) => UpdateRenderable(entity));
        entity.SubscribeComponentChange<TransformComponent>((_, _) => UpdateRenderable(entity));
    }

    public void OnUpdate(Entity entity, float dt)
    {
        _renderQueue.Add(entity.GetComponent<RenderableComponent>().Renderable);
    }

    private void InitRenderable(Entity entity)
    {
        var textComponent = entity.GetComponent<LabelComponent>();
        var transform = entity.GetComponent<WorldTransformComponent>();
        var renderableComponent = entity.GetComponent<RenderableComponent>();
        
        var drawable = new TextDrawable
        {
            Text = textComponent.Text,
            Font = _fontManager.Get(textComponent.Font),
            FontSize = textComponent.FontSize,
            Color = textComponent.Color,
            Position = transform.Position,
            Rotation = transform.Rotation,
            Scale = transform.Scale
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
        var textComponent = entity.GetComponent<LabelComponent>();
        var transform = entity.GetComponent<WorldTransformComponent>();

        entity.Modify((ref RenderableComponent renderableComponent) =>
        {
            var drawable = (TextDrawable)renderableComponent.Renderable.Drawable;
            drawable.Text = textComponent.Text;
            drawable.Font = _fontManager.Get(textComponent.Font);
            drawable.FontSize = textComponent.FontSize;
            drawable.Color = textComponent.Color;
            drawable.Position = transform.Position;
            drawable.Rotation = transform.Rotation;
            drawable.Scale = transform.Scale;
            renderableComponent.Renderable.Drawable = drawable;
        });
    }
}