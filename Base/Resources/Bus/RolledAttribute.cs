using Godot;

namespace Base.Resources.Bus
{
    public class RolledAttribute : AttributeDescriptor
    {
        /// <summary>
        /// The die roll needed to randomly be assigned this Birth Aspect.
        /// </summary>
        /// <value></value>
        [Export]
        public Die DieRoll { get; set; }
        /// <summary>
        /// Rolls for a default Base attribute value.
        /// </summary>
        /// <returns>int</returns>
        public int RollBaseValue() { return DieRoll.Roll(); }
    }
}