using Godot;

namespace Base.Resources.Ui
{
    /// <summary>
    /// Abstract class for displaying tooltips.
    /// </summary>
    public abstract class Tooltip : Popup
    {
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            
            if (Validate())
            {
                SetContent();
            }
        }
        /// <summary>
        /// Validates the tooltip.
        /// </summary>
        /// <returns></returns>
        public abstract bool Validate();
        /// <summary>
        /// Validates the tooltip.
        /// </summary>
        /// <returns></returns>
        protected abstract void SetContent();
    }
}