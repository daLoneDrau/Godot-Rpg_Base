using Base.Resources.Data;
using Base.Resources.Events;
using Bus.Services;
using Godot;
using System;

namespace Base.Resources.Bus
{
    public class PlayerAttribute
    {
        /// <summary>
        /// Wrapper to get the attrribute's abbreviation.
        /// </summary>
        /// <value></value>
        public string Abbr { get { return AttributeDescriptor.Abbreviation; } }
        /// <summary>
        /// Gets the attribute's descriptor instance.
        /// </summary>
        /// <value></value>
        public AttributeDescriptor AttributeDescriptor { get; }
        /// <summary>
        /// the attribute's base value.
        /// </summary>
        private float baseValue;
        /// <summary>
        /// the attribute's base value property.
        /// </summary>
        /// <value></value>
        public float Base
        {
            get { return baseValue; }
            set
            {
                baseValue = value;
                // broadcast signal that attributes have changed
                PublicBroadcastService.Instance.PcEventChannel.Broadcast(new PcEventSignal(Parent.Io.RefId, Globals.PLAYER_EVENT_UPDATE_ATTRIBUTES));
            }
        }
        /// <summary>
        /// Wrapper to get the attrribute's element modifier.
        /// </summary>
        /// <value></value>
        public int ElementModifier { get { return AttributeDescriptor.EquipmentElement.RuntimeValue; } }
        /// <summary>
        /// Property for retrieving the player's full attribute score.
        /// </summary>
        /// <value></value>
        public float Full { get { return Base + mod; } }
        /// <summary>
        /// Wrapper to get the attribute's Id.
        /// </summary>
        /// <value></value>
        public int Id { get { return AttributeDescriptor.Id; } }
        /// <summary>
        /// the value of any modifiers applied to the attribute.
        /// </summary>
        private float mod;
        /// <summary>
        /// The parent data instance.
        /// </summary>
        /// <value></value>
        public IoPcData Parent { get; }
        /// <summary>
        /// Creates a new PlayerAttribute instance.
        /// </summary>
        /// <param name="attributeDescriptor"></param>
        public PlayerAttribute(AttributeDescriptor attributeDescriptor, IoPcData parent)
        {
            AttributeDescriptor = attributeDescriptor;
            Parent = parent;
        }
        /// <summary>
        /// Adjusts the attribute modifier by a certain amount.
        /// </summary>
        /// <param name="value">the amount the attribute should be modified by</param>
        public void AdjustModifier(float value) {
            mod += value;
            // broadcast signal that attributes have changed
            PublicBroadcastService.Instance.PcEventChannel.Broadcast(new PcEventSignal(Parent.Io.RefId, Globals.PLAYER_EVENT_UPDATE_ATTRIBUTES));
        }
        public void ClearModifiers() {  this.mod = 0; }
    }
}
