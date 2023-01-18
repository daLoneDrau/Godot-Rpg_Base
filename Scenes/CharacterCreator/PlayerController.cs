using Base.Pooled;
using Base.Resources.Bus;
using Base.Resources.Data;
using Base.Resources.Events;
using Base.Resources.Services;
using Godot;

namespace Base.Scenes.CharacterCreator
{
    /// <summary>
    /// Controller for a player view.
    /// </summary>
    /// <typeparam name="PC_DATA">the type of pc data</typeparam>
    public abstract class PlayerController<PC_DATA> where PC_DATA : IoPcData
    {
        /// <summary>
        /// The parent controller.
        /// </summary>
        protected CharacterCreatorController<PC_DATA> parent;
        /// <summary>
        /// Creates a new instance of PlayerController.
        /// </summary>
        /// <param name="parent">the parent controller</param>
        public PlayerController(CharacterCreatorController<PC_DATA> parent)
        {
            this.parent = parent;
        }
        /// <summary>
        /// Handles player events.
        /// </summary>
        /// <param name="signal">the signal received</param>
        public abstract void HandlePlayerEvents(PcEventSignal signal);
    }
}