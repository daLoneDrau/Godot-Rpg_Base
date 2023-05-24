namespace Base.Scenes.Controllers
{
    /// <summary>
    /// Controller for a scene that contains other scenes as widgets and consumes state changes.
    /// </summary>
    public abstract class Commodore64MainSceneContainer : Commodore64Controller
    {
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            ConsumeStateEvent();
        }
        /// <summary>
        /// Consumes any signals of a state change event.
        /// </summary>
        public abstract void ConsumeStateEvent();
        /// <summary>
        /// Hides all widgets.
        /// </summary>
        protected abstract void HideAll();
    }
}