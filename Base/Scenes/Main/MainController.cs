using Base.Resources.Bus;
using Base.Resources.Data;
using Base.Resources.Events;
using Base.Resources.Services;
using Godot;
using System;

namespace Base.Scenes.Main
{
    public abstract class MainController : Node
    {
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
        }
        public virtual void _on_back_to_main()
        {
            GetTree().ChangeScene("res://scenes/App-Main.tscn");
            // remove any subscribers established in ready
        }
        public override void _UnhandledKeyInput(InputEventKey @event)
        {
        }
    }
}