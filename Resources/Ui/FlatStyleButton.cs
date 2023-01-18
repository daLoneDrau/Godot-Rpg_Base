using Godot;

namespace Base.Resources.Ui
{
    /// <summary>
    /// Abstract class for changing styles on a flat button.
    /// </summary>
    public abstract class FlatStyleButton : Button
    {
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
            Connect("pressed", this, "pressed");
            Connect("button_down", this, "button_down");
            Initialize();
        }
        protected abstract void Initialize();
        public void pressed()
        {
            ButtonPressed();
        }
        public void button_down()
        {
            ButtonDown();
        }
        protected abstract void ButtonDown();
        protected abstract void ButtonPressed();
    }
}