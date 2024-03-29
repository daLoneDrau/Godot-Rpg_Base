using Godot;

namespace Base.Resources.Ui
{
    public abstract class TooltipParent : Label
    {
        /// <summary>
        /// the tooltip's alignment. default is to the right of the Control node.
        /// </summary>
        /// <value></value>
        protected Vector2 Alignment { get; set; } = Vector2.Right;
        /// <summary>
        /// the top-level parent.
        /// </summary>
        /// <value></value>
        protected Control TopLevelContainer { get; set; }
        /// <summary>
        /// the top-level parent.
        /// </summary>
        /// <value></value>
        protected CanvasItem TopLevelCanvasItem { get; set; }
        /// <summary>
        /// the tooltip's alignment. default is to the right of the Control node.
        /// </summary>
        /// <value></value>
        protected float YOffset { get; set; } = -100;
        /// <summary>
        /// the tooltip's alignment. default is to the right of the Control node.
        /// </summary>
        /// <value></value>
        protected float XOffset { get; set; } = -0;
        /// <summary>
        /// the tooltip scene
        /// </summary>
        protected PackedScene Tooltip { get; set; }
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            Connect("mouse_entered", this, "_on_mouse_entered");
            Connect("mouse_exited", this, "_on_mouse_exited");
            Initialize();
        }
        protected abstract void Initialize();
        /// <summary>
        /// Sets the tooltip instance display.
        /// </summary>
        /// <param name="tooltip"></param>
        protected abstract void SetTooltipDisplay(Tooltip tooltip);
        private async void MouseEntered()
        {
            Tooltip instance = (Tooltip)Tooltip.Instance();
            SetTooltipDisplay(instance);

            GD.Print("TopLevelCanvasItem ", TopLevelCanvasItem.Name);
            GD.Print("TopLevelContainer ", TopLevelContainer.Name);

            float x = 0, y = 0;
            if (Alignment == Vector2.Right)
            {
                y = GetGlobalTransformWithCanvas().origin.y + YOffset;
                float rightAnchor = TopLevelContainer.AnchorRight;
                float leftMargin = TopLevelContainer.MarginLeft;
                if (rightAnchor == 0f)
                {
                    float rightMargin = TopLevelContainer.MarginRight;
                    x = TopLevelCanvasItem.GetGlobalTransformWithCanvas().origin.x + (rightMargin - leftMargin) + XOffset;
                }
                else
                {
                    x = TopLevelCanvasItem.GetGlobalTransformWithCanvas().origin.x + (TopLevelCanvasItem.GetViewport().Size.x * TopLevelContainer.AnchorRight - TopLevelContainer.MarginLeft) + XOffset;
                }
                // place the tooltip directly to the right of the hovered item.
                // hovered item's x origin + hovered item's width (viewport width * item's right anchor - item's left margin)
                GD.Print("origin x ", TopLevelCanvasItem.GetGlobalTransformWithCanvas().origin.x);
                GD.Print("viewport width ", TopLevelCanvasItem.GetViewport().Size.x);
                GD.Print("AnchorRight ", TopLevelContainer.AnchorRight);
                GD.Print("MarginLeft ", TopLevelContainer.MarginLeft);
                GD.Print("MarginRight ", TopLevelContainer.MarginRight);
                GD.Print("x ", x);
                GD.Print(TopLevelCanvasItem.GetViewport().Size.x * TopLevelContainer.AnchorRight - TopLevelContainer.MarginLeft);
            }
            else if (Alignment == Vector2.Left)
            {
                y = GetGlobalTransformWithCanvas().origin.y + YOffset;
                x = TopLevelCanvasItem.GetGlobalTransformWithCanvas().origin.x - instance.RectMinSize.x;
            }
            else if (Alignment == Vector2.Down)
            {
                x = GetGlobalTransformWithCanvas().origin.x + XOffset;
                float bottomAnchor = TopLevelContainer.AnchorBottom;
                float bottomMargin = TopLevelContainer.MarginBottom;
                y = TopLevelCanvasItem.GetGlobalTransformWithCanvas().origin.y + (TopLevelCanvasItem.GetViewport().Size.y * TopLevelContainer.AnchorBottom - TopLevelContainer.MarginBottom);
                GD.Print("origin y ", TopLevelCanvasItem.GetGlobalTransformWithCanvas().origin.y);
                GD.Print("viewport height ", TopLevelCanvasItem.GetViewport().Size.y);
                GD.Print(TopLevelContainer.AnchorBottom);
                GD.Print(TopLevelContainer.MarginBottom);
                if (bottomAnchor == 0f)
                {
                    float topMargin = TopLevelContainer.MarginTop;
                    y = TopLevelCanvasItem.GetGlobalTransformWithCanvas().origin.y + (bottomMargin - topMargin) + YOffset;
                }
                else
                {
                    y = TopLevelCanvasItem.GetGlobalTransformWithCanvas().origin.y + (TopLevelCanvasItem.GetViewport().Size.y * TopLevelContainer.AnchorBottom - TopLevelContainer.MarginBottom);
                }
            }
            instance.RectPosition = new Vector2(x, y);

            AddChild(instance);
            await ToSignal(GetTree().CreateTimer(0.35f), "timeout");
            if (HasNode("Tooltip") && GetNode<Tooltip>("Tooltip").Validate())
            {
                GetNode<Tooltip>("Tooltip").Show();
            }
        }
        public void _on_mouse_entered()
        {
            MouseEntered();
        }
        public void _on_mouse_exited()
        {
            GetNode<Tooltip>("Tooltip").Free();
        }
    }
}