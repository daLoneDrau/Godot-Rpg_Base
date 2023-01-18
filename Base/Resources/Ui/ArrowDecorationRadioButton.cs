using System.Collections.Generic;
using Godot;

namespace Base.Resources.Ui
{
    /// <summary>
    /// Abstract class for displaying arrow decorations when a button is hovered. When the button is pressed, the decoration will stay until another button in the group is pressed.
    /// </summary>
    public abstract class ArrowDecorationRadioButton : Button
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
        public bool IsSelected { get; set; }  = false;
        protected List<ArrowDecorationRadioButton> buttonGroup = new List<ArrowDecorationRadioButton>();
        public void AddToButtonGroup(ArrowDecorationRadioButton button)
        {
            buttonGroup.Add(button);
        }
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            Connect("mouse_entered", this, "_on_mouse_entered");
            Connect("mouse_exited", this, "_on_mouse_exited");
            Connect("pressed", this, "_on_pressed");
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
        private async void Pressed()
        {
            IsSelected = true;

            // send notice to all other buttons in the group to dispose of their decorations
            for (int i = buttonGroup.Count - 1; i >= 0; i--)
            {
                ArrowDecorationRadioButton btn = buttonGroup[i];
                if (btn.Name.Equals(this.Name))
                {
                    continue;
                }
                btn.IsSelected = false;
                btn._on_mouse_exited();
            }
        }
        public void _on_mouse_entered()
        {
            MouseEntered();
        }
        public void _on_mouse_exited()
        {
            if (!IsSelected)
            {
                ArrowDecoration arrow = GetNode<ArrowDecoration>("ArrowDecoration");
                if (arrow != null)
                {
                    arrow.Free();
                }
            }
        }
        public void _on_pressed()
        {
            Pressed();
        }
    }
}