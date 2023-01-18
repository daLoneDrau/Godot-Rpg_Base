using Godot;

namespace Base.Resources.Ui
{
    /// <summary>
    /// Abstract class for displaying an animated decorations when a button is hovered.
    /// </summary>
    public class AnimatedDecorationButton : Button
    {
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            Connect("mouse_entered", this, "_on_mouse_entered");
            Connect("mouse_exited", this, "_on_mouse_exited");
        }
        public void _on_mouse_entered()
        {
            if (!Disabled)
            {
                GetNode<AnimatedSprite>("./decoration").Visible = true;
            }
        }
        public void _on_mouse_exited()
        {
            GetNode<AnimatedSprite>("./decoration").Visible = false;
        }
    }
}