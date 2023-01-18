using Base.Resources.Bus;
using Godot;
using System;
using System.Collections.Generic;

namespace Base.Resources.Bus
{
    public abstract class SelectedResourceAttribute<E> : AttributeDescriptor
    {
        /// <summary>
        /// The die roll needed to randomly be assigned this Birth Aspect.
        /// </summary>
        /// <value></value>
        [Export]
        public Die DieRoll { get; set; }
        public E Value { get; protected set; }
        public abstract void AssignRandomValue(Dictionary<string, object> parameterObject = null);    
    }
}
