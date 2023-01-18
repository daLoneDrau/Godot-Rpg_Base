using Godot;
using System;

namespace Base.Resources.Variables
{
    public class StringArrayVariable : Resource
    {
        /// <summary>
        /// The initial property value.
        /// </summary>
        [Export]
        public string[] InitialValue { get; set; }
        /// <summary>
        /// the runtime property value.
        /// </summary>
        public string[] RuntimeValue { get; set; }
        // Make sure that every parameter has a default value.
        // Otherwise, there will be problems with creating and editing
        // your resource via the inspector.
        public StringArrayVariable(string[] initialValue = null)
        {
            InitialValue = initialValue;
            RuntimeValue = initialValue;
        }
        public StringArrayVariable() { }
    }
}
