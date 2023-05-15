using Base.Exceptions;
using Base.Resources.Bus;
using Base.Resources.Data;
using Base.Resources.Inventory;
using Base.Resources.Scripting;
using Bus.Services;
using Godot;
using System;
using System.Collections.Generic;

namespace Base.Resources.Services
{
    public abstract class GameController : Node
    {
        /// <summary>
        /// Reference to the singleton instance.
        /// </summary>
        /// <value></value>
        public static GameController Instance { get; protected set; }
        /// <summary>
        /// The path to the Resources folder.  The default path is "res://resources".
        /// </summary>
        /// <value></value>
        public string ResourcesFolderPath { get; protected set; } = "res://resources";
        /// <summary>
        /// The path to the Resources folder.  The default path is "res://resources".
        /// </summary>
        /// <value></value>
        public string ScenesFolderPath { get; protected set; } = "res://scenes";
    }
}
