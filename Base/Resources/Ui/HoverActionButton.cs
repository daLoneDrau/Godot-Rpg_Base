using Godot;

namespace Base.Resources.Ui
{
    /// <summary>
    /// Abstract class for performing an action when a button is hovered.
    /// </summary>
    public abstract class HoverActionButton : Button
    {
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            Connect("mouse_entered", this, "OnMouseEntered");
            Connect("mouse_exited", this, "OnMouseExited");
        }
        public abstract void OnMouseExited();
        public abstract void OnMouseEntered();
    }
}