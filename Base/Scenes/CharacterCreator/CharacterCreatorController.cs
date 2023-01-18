using Base.Resources.Bus;
using Base.Resources.Data;
using Base.Resources.Events;
using Base.Resources.Services;
using Godot;
using System;

namespace Base.Scenes.CharacterCreator
{
    public abstract class CharacterCreatorController<PC_DATA> : Node
    where PC_DATA : IoPcData
    {
        /// <summary>
        /// The IO being created.
        /// </summary>
        /// <value></value>
        public InteractiveObject Io { get; private set; }
        protected PlayerController<PC_DATA> playerController;
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            InitializePlayerController();
            PublicBroadcastService.Instance.PcEventChannel.AddSubscriber(playerController.HandlePlayerEvents);
            Io = IoFactory.Instance.AddPc();
            NewPlayer();
        }
        /// <summary>
        /// Initializes the player controller instance.
        /// </summary>
        protected abstract void InitializePlayerController();
        /// <summary>
        /// Initializes the player controller instance.
        /// </summary>
        protected abstract void NewPlayer();
        private void ReceiveMenuSignal(int code)
        {
            switch (code)
            {
                case 3:
                    NewPlayer();
                    break;
            }
        }
        public void _on_reroll_character(int code)
        {
            NewPlayer();
        }
        public void _on_back_to_main()
        {
            GetTree().ChangeScene("res://scenes/App-Main.tscn");
            PublicBroadcastService.Instance.PcEventChannel.RemoveSubscriber(playerController.HandlePlayerEvents);
        }
        public override void _UnhandledKeyInput(InputEventKey @event)
        {
        }
    }
}