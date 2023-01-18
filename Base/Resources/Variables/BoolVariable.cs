using Godot;
using System;

namespace Base.Resources.Variables
{
    public class BoolVariable : Resource
    {
        /// <summary>
        /// The initial property value.
        /// </summary>
        [Export]
        public bool InitialValue { get; set; }
        /// <summary>
        /// the runtime property value.
        /// </summary>
        public bool RuntimeValue { get; set; }
        // Make sure that every parameter has a default value.
        // Otherwise, there will be problems with creating and editing
        // your resource via the inspector.
        public BoolVariable(bool initialValue = false)
        {
            InitialValue = initialValue;
            RuntimeValue = initialValue;
        }
        public BoolVariable() { }
    }
}
