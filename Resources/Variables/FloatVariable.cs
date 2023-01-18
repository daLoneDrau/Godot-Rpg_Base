using Godot;
using System;

namespace Base.Resources.Variables
{
    public class FloatVariable : Resource
    {
        /// <summary>
        /// The initial property value.
        /// </summary>
        [Export]
        public float InitialValue { get; set; }
        /// <summary>
        /// the runtime property value.
        /// </summary>
        public float RuntimeValue { get; set; }
        // Make sure that every parameter has a default value.
        // Otherwise, there will be problems with creating and editing
        // your resource via the inspector.
        public FloatVariable(float initialValue = 0f)
        {
            InitialValue = initialValue;
            RuntimeValue = initialValue;
        }
        public FloatVariable() { }
    }
}
