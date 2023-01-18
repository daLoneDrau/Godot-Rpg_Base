using Base.Exceptions;
using Godot;
using System;

namespace Base.Resources.Inventory
{
    public class EquipmentItemModifier : Resource
    {
    /// <summary>
        /// the flag indicating whether the EquipmentItemModifier is a percentage modifier.
        /// </summary>
        /// <value></value>
        [Export]
        public bool Percent { get; set; }
        /// <summary>
        /// the flag indicating whether the EquipmentItemModifier represents a specal effect (PARALYSIS, DRAIN LIFE, etc...).
        /// </summary>
        /// <value></value>
        [Export]
        public int Special { get; set; }
        /// <summary>
        /// the EquipmentItemModifier value.
        /// </summary>
        /// <value></value>
        [Export]
        public int Value { get; set; }
        /// <summary>
        /// Creates a new EquipmentItemModifier instance
        /// </summary>
        public EquipmentItemModifier() { }
        /// <summary>
        /// Creates a new EquipmentItemModifier instance
        /// </summary>
        /// <param name="copy">the EquipmentItemModifier to copy</param>
        public EquipmentItemModifier(EquipmentItemModifier copy)
        {
            if (copy == null) {
                throw new RPGException(ErrorMessage.BAD_PARAMETERS, "EquipmentItemModifier() - copy parameter is null");
            }
            Set(copy);
        }
        /// <summary>
        /// Sets the modifier values.
        /// </summary>
        /// <param name="clone">the EquipmentItemModifier to clone</param>
        public void Set(EquipmentItemModifier clone) {
            if (clone == null) {
                throw new RPGException(ErrorMessage.BAD_PARAMETERS, "EquipmentItemModifier.set() requires an EquipmentItemModifier to clone");
            }
            this.Percent = clone.Percent;
            this.Special = clone.Special;
            this.Value = clone.Value;
        }
    }
}
