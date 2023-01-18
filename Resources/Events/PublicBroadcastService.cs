using Base.Resources.Scripting;
using Base.Resources.Variables;
using Godot;
using System;

namespace Base.Resources.Events
{
    public class PublicBroadcastService : Node
    {
        /// <summary>
        /// Reference to the singleton instance.
        /// </summary>
        /// <value></value>
        public static PublicBroadcastService Instance { get; private set; }
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            Instance = this;
        }
        public Channel<PcEventSignal> PcEventChannel = new Channel<PcEventSignal>();
        public Channel<ScriptableEventParameters> ScriptableEventChannel = new Channel<ScriptableEventParameters>();
    }
}
