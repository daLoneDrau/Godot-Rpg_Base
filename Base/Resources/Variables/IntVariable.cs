using Godot;
using System;

namespace Base.Resources.Variables
{
    public class IntVariable : Resource
    {
        /// <summary>
        /// The initial property value.
        /// </summary>
        [Export]
        public int InitialValue { get; set; }
        /// <summary>
        /// the runtime property value.
        /// </summary>
        public int RuntimeValue { get; set; }
        // Make sure that every parameter has a default value.
        // Otherwise, there will be problems with creating and editing
        // your resource via the inspector.
        public IntVariable(int initialValue = 0)
        {
            InitialValue = initialValue;
            RuntimeValue = initialValue;
        }
        public IntVariable() {
            RuntimeValue = InitialValue;
        }
    }
}
