namespace Base.Scenes.Controllers
{
    /// <summary>
    /// Controller for a scene that contains other scenes as widgets and consumes state changes.
    /// </summary>
    public abstract class NesMainSceneContainer : NesController
    {
        /// <summary>
        /// Hides all widgets.
        /// </summary>
        protected abstract void HideAll();
    }
}