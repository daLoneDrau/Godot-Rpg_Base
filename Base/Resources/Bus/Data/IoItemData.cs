using Base.Bus;
using Base.Resources.Events;
using Base.Resources.Inventory;
using Base.Resources.Scripting;
using Base.Resources.Services;
using Godot;
using System;
using System.Collections.Generic;

namespace Base.Resources.Data
{
    public abstract class IoItemData : IoData
    {
        private float count;
        /// <summary>
        /// the current number.
        /// </summary>
        /// <value></value>
        public float Count
        {
            get { return count; }
            set
            {
                count = value;
                if (count < 0)
                {       
                    bool allowItemCountBelowZero = false;             
                    if (Io.Script != null && Io.Script.HasLocalVariable("allowItemCountBelowZero"))
                    {
                        allowItemCountBelowZero = Io.Script.GetLocalVariableValue<bool>("allowItemCountBelowZero");
                    }
                    if (!allowItemCountBelowZero)
                    {
                        count = 0;
                    }
                }
                GD.Print("updating count");
                // send signal that the count was updated.
                // send a scriptable event to all owners of an Inventory that this IO has updated its item count
                PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
                {
                    EventName = "UPDATE_ITEM_COUNT",
                    EventSender = Io,
                    Parameters = new Dictionary<string, object>()
                    {
                        { "group", "INVENTORY_OWNERS" }
                    },
                    TargetIo = -1 // send signal to all IOs
                });
            }
        }
        /// <summary>
        /// the list of equipment modifiers.
        /// </summary>
        private EquipmentItemModifier[] elements;
        /// <summary>
        /// the max number cumulable
        /// </summary>
        public int MaxCount { get; set; } = 1;
        /// <summary>
        /// the item's pruice.
        /// </summary>
        /// <value></value>
        public float Price { get; set; }
        /// <summary>
        /// the most of the item that can be stacked in one slot
        /// </summary>
        /// <value></value>
        public int StackSize { get; set; }
        /// <summary>
        /// the item's type flags.
        /// </summary>
        public FlagSet TypeFlags { get; private set; } = new FlagSet();
        /// <summary>
        /// Gets the element modifier for a specific element.
        /// </summary>
        /// <param name="element">the element</param>
        /// <returns>the element modifier object</returns>
        public EquipmentItemModifier GetElementModifier(int element) {
            if (element >= this.elements.Length) {
                for (int i = this.elements.Length, li = element; i <= li; i++) {
                    this.elements = ArrayUtilities.Instance.ExtendArray(new EquipmentItemModifier(), this.elements);
                }
            }
            return this.elements[element];
        }
    }
}
