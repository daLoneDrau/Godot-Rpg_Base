using Base.Exceptions;
using Base.Resources.Bus;
using Base.Resources.Data;
using Base.Resources.Events;
using Base.Resources.Scripting;
using Base.Resources.Services;
using Bus.Services;
using Godot;
using System;


namespace Base.Resources.Inventory
{
    /// <summary>
    /// The InventoryData abstract class.
    /// </summary>
    public abstract class InventoryData : IoData
    {
        /// <summary>
        /// the inventory slots.
        /// </summary>
        protected InventorySlot[] slots;
        /// <summary>
        /// Protected constructor.
        /// </summary>
        protected InventoryData()
        {
            Init();
        }
        /// <summary>
        /// Tries to put an object in an IO's inventory.
        /// </summary>
        /// <param name="ioid"></param>
        /// <returns></returns>
        public bool CanBePutInInventory(int ioid)
        {
            if (!IoFactory.Instance.IsValidIo(ioid)) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InventoryData.canBePutInInventory() requires a valid IO");
            }
            InteractiveObject itemIo = IoFactory.Instance.GetIo(ioid);
            bool can = false;
            // try to stack
            bool stacked = false;
            for (int i = slots.Length - 1; i >= 0; i--)
            {
                InteractiveObject ioInSlot = IoFactory.Instance.GetIo(slots[i].IoId);
                if (ioInSlot != null
                        && ((IoItemData)ioInSlot.Data).StackSize > 1
                        && IoFactory.Instance.IsSameObject(ioid, ioInSlot.RefId))
                {
                    // an item already in a slot is the same type and has less than the max stack size
                    if (((IoItemData)ioInSlot.Data).Count < ((IoItemData)ioInSlot.Data).StackSize)
                    {
                        can = true;
                        IoItemData slotItemData = (IoItemData)ioInSlot.Data;
                        IoItemData itemData = (IoItemData)itemIo.Data;
                        slotItemData.Count += itemData.Count;
                        if (slotItemData.Count > slotItemData.StackSize)
                        {
                            // too much to be stacked. split the items into two stacks
                            itemData.Count = slotItemData.Count - slotItemData.StackSize;
                            slotItemData.Count = slotItemData.StackSize;
                        }
                        else
                        {
                            // item can be stacked with the existing items.
                            itemData.Count = 0;
                            stacked = true;
                        }
                        if (itemData.Count == 0)
                        {
                            IoFactory.Instance.ReleaseIo(IoFactory.Instance.GetIo(ioid));
                        }
                        // send message to Inventory Holder that they stacked the item
                        PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
                        {
                            EventId = Globals.SM_INVENTORYIN,
                            EventSender = ioInSlot,
                            TargetIo = Io.RefId
                        });
                    }
                }
            }
            if (!stacked) {
                for (int i = 0, li = slots.Length; i < li; i++) {
                    if (slots[i].IoId == -1) {
                        slots[i].IoId = ioid;
                        slots[i].Show = true;
                        DeclareInInventory(ioid);
                        can = true;
                        break;
                    }
                }
            }
            return can;
        }
        /// <summary>
        /// Cleans the inventory.
        /// </summary>
        public void CleanInventory()
        {
            for (int i = this.slots.Length - 1; i >= 0; i--) {
                this.slots[i].IoId = -1;
                this.slots[i].Show = true;
            }
        }
        /// <summary>
        /// Declares that an object is in an IO's inventory, sending scripted events to the possessor and the item.
        /// </summary>
        /// <param name="ioid">the object's reference id</param>
        public void DeclareInInventory(int ioid) {
            if (!IoFactory.Instance.IsValidIo(ioid)) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InventoryData.declareInInventory() requires a valid IO");
            }
            // send message to Inventory Holder that they are recieving the item
            PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
            {
                EventId = Globals.SM_INVENTORYIN,
                EventSender = IoFactory.Instance.GetIo(ioid),
                TargetIo = Io.RefId
            });
            // send message to the item they are now in Inventory Holder's possession
            PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
            {
                EventId = Globals.SM_INVENTORYIN,
                EventSender = Io,
                TargetIo = ioid
            });
        }
        /// <summary>
        /// Gets the item in a specific inventory slot.  If no item is there, return null.
        /// </summary>
        /// <param name="i">the inventory slot</param>
        /// <returns></returns>
        public InteractiveObject GetItemInSlot(int i)
        {
            InteractiveObject item = null;
            if (slots[i].IoId >= 0 && IoFactory.Instance.IsValidIo(slots[i].IoId))
            {
                item = IoFactory.Instance.GetIo(slots[i].IoId);
            }
            return item;
        }
        protected abstract void Init();
        /// <summary>
        /// Determines if an object is in inventory.
        /// </summary>
        /// <param name="ioid">the object's reference id</param>
        /// <returns>true if the item is in the inventory; false otherwise</returns>
        public bool IsInInventory(int ioid) {
            if (!IoFactory.Instance.IsValidIo(ioid)) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InventoryData.isInInventory() requires a valid IO");
            }
            bool isIn = false;
            for (int i = slots.Length - 1; i >= 0; i--) {
                int ioInSlot = slots[i].IoId;
                if (ioid == ioInSlot) {
                    isIn = true;
                    break;
                }
            }
            return isIn;
        }
        /// <summary>
        /// Gets the number of inventory slots available.
        /// </summary>
        /// <value></value>
        public int NumInventorySlots { get { return this.slots.Length; } }
        /// <summary>
        /// Tries to remove an object from an IO's inventory, ignoring any count size.
        /// </summary>
        /// <param name="ioid">the object's reference id</param>
        /// <returns>true if the item was removed; false otherwise</returns>
        public bool RemoveFromInventory(int ioid) {
            if (!IoFactory.Instance.IsValidIo(ioid)) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InventoryData.removeFromInventory() requires a valid IO");
            }
            bool removed = false;
            for (int i = this.slots.Length - 1; i >= 0; i--) {
                if (ioid == this.slots[i].IoId) {
                    this.slots[i].IoId = -1;
                    this.slots[i].Show = true;
                    removed = true;
                    break;
                }
            }
            return removed;
        }
        /// <summary>
        /// Tries to replace an object from an IO's inventory, ignoring any count size.
        /// </summary>
        /// <param name="ioid">the object id</param>
        /// <param name="replacedWith">the object replacing with</param>
        public void ReplaceInInventory(int ioid, int replacedWith) {
            if (!IoFactory.Instance.IsValidIo(ioid)) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InventoryData.ReplaceInInventory() requires a valid IO to be replaced");
            }
            if (!IoFactory.Instance.IsValidIo(replacedWith)) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InventoryData.ReplaceInInventory() requires a valid IO to be replaced with");
            }
            for (int i = this.slots.Length - 1; i >= 0; i--) {
                if (ioid == this.slots[i].IoId) {
                    this.slots[i].IoId = replacedWith;
                    this.slots[i].Show = true;
                    break;
                }
            }
        }
    }
}
