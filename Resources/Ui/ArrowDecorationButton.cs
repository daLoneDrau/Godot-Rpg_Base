using Godot;

namespace Base.Resources.Ui
{
    /// <summary>
    /// Abstract class for displaying arrow decorations when a button is hovered.
    /// </summary>
    public abstract class ArrowDecorationButton : Button
    {
        /// <summary>
        /// the decoration's alignment. default is to the left of the Button node.
        /// </summary>
        /// <value></value>
        protected Vector2 Alignment { get; set; } = Vector2.Left;
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
        protected PackedScene ArrowDecoration { get; set; }
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
        private async void MouseEntered()
        {
            ArrowDecoration instance = (ArrowDecoration)ArrowDecoration.Instance();

            float x = 0, y = 0;
            if (Alignment == Vector2.Right)
            {
                // place the arrow directly to the right of the hovered item.
                // hovered item's x origin + hovered item's width (viewport width * item's right anchor - item's left margin)
                y = GetGlobalTransformWithCanvas().origin.y + YOffset;
                x = TopLevelCanvasItem.GetGlobalTransformWithCanvas().origin.x + (TopLevelCanvasItem.GetViewport().Size.x * TopLevelContainer.AnchorRight - TopLevelContainer.MarginLeft);
                // GD.Print(TopLevelCanvasItem.GetGlobalTransformWithCanvas().origin.x);
                // GD.Print(TopLevelCanvasItem.GetViewport().Size.x);
                // GD.Print(TopLevelContainer.AnchorRight);
                // GD.Print(TopLevelContainer.MarginLeft);
                // GD.Print(TopLevelCanvasItem.GetViewport().Size.x * TopLevelContainer.AnchorRight - TopLevelContainer.MarginLeft);
            }
            else if (Alignment == Vector2.Left)
            {
                y = GetGlobalTransformWithCanvas().origin.y; // + (GetViewport().Size.y * AnchorBottom - MarginBottom);
                x = TopLevelCanvasItem.GetGlobalTransformWithCanvas().origin.x - instance.RectMinSize.x;
            }
            else if (Alignment == Vector2.Down)
            {
                x = GetGlobalTransformWithCanvas().origin.x + XOffset;
                y = TopLevelCanvasItem.GetGlobalTransformWithCanvas().origin.y + (TopLevelCanvasItem.GetViewport().Size.y * TopLevelContainer.AnchorBottom - TopLevelContainer.MarginBottom);
            }
            instance.RectPosition = new Vector2(x, y);
            GD.Print("instance.RectPosition ", instance.RectPosition);

            AddChild(instance);
            await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
            if (HasNode("ArrowDecoration"))
            {
                GetNode<ArrowDecoration>("ArrowDecoration").Show();
                GD.Print("show");
            }
        }
        public void _on_mouse_entered()
        {
            MouseEntered();
        }
        public void _on_mouse_exited()
        {
            GetNode<ArrowDecoration>("ArrowDecoration").Free();
        }
    }
}