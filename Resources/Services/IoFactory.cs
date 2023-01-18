using Base.Exceptions;
using Base.Resources.Bus;
using Base.Resources.Data;
using Base.Resources.Inventory;
using Base.Resources.Scripting;
using Bus.Services;
using Godot;
using System;
using System.Collections.Generic;

namespace Base.Resources.Services
{
    public abstract class IoFactory : Node
    {
        /// <summary>
        /// the singleton instance.
        /// </summary>
        public static IoFactory Instance { get; protected set; }
        /// <summary>
        /// the list of interactive objects the factory has created.
        /// </summary>
        private InteractiveObject[] ios = new InteractiveObject[0];
        public abstract InteractiveObject AddItem();
        public abstract InteractiveObject AddNpc();
        public abstract InteractiveObject AddPc();
        public abstract int GenerateRefId();
        public abstract int GetLastId();
        /// <summary>
        /// Adds an IO.
        /// </summary>
        /// <param name="io">the io being added</param>
        protected void AddIo(InteractiveObject io)
        {
            if (io == null) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Interactive.addIo() requires an instance of InteractiveObject");
            }
            // look for an empty spot
            int index = -1;
            for (int i = this.ios.Length - 1; i >= 0; i--) {
                if (this.ios[i] == null)
                {
                    index = i;
                    break;
                }
            }
            if (index >= 0) {
                this.ios[index] = io;
            } else {
                this.ios = ArrayUtilities.Instance.ExtendArray(io, this.ios);
            }
        }
        public void ResetAllNpcBehaviours()
        {
            for (int i = this.ios.Length - 1; i >= 0; i--)
            {
                InteractiveObject io = this.ios[i];
                if (io != null && io.IoFlags.Has(Globals.IO_NPC)) {
                    ((IoNpcData)io.Data).ResetBehaviour();
                }
            }
        }
        /// <summary>
        /// Sends an initialization event to an InteractiveObject. The initialization event runs the local script first, followed by the over script.
        /// </summary>
        /// <param name="io"></param>
        /// <returns></returns>
        public int SendInitScriptEvent(InteractiveObject io, Dictionary<string, object> parameters = null)
        {
            if (!IsValidIo(io))
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "IoFactory.sendInitScriptEvent() needs an IO to initialize");
            }

            if (io.Script != null)
            {
                io.Script.Execute(new ScriptableEventParameters()
                {
                    EventName = "INIT",
                    TargetIo = io.RefId,
                    Parameters = parameters
                });
            }
            if (io.OverScript != null)
            {
                io.OverScript.Execute(new ScriptableEventParameters()
                {
                    EventName = "INIT",
                    TargetIo = io.RefId,
                    Parameters = parameters
                });
            }
            if (io.Script != null)
            {
                io.Script.Execute(new ScriptableEventParameters()
                {
                    EventName = "INITEND",
                    TargetIo = io.RefId,
                    Parameters = parameters
                });
            }
            if (io.OverScript != null)
            {
                io.OverScript.Execute(new ScriptableEventParameters()
                {
                    EventName = "INITEND",
                    TargetIo = io.RefId,
                    Parameters = parameters
                });
            }
            return Globals.ACCEPT;
        }
        #region "INVENTORY"
        /// <summary>
        /// Seeks an IO in all Inventories to remove it.
        /// </summary>
        /// <param name="ioid">the IO's reference ID</param>
        public void RemoveFromAllInventories(int ioid)
        {
            if (!this.IsValidIo(ioid))
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Interactive.removeFromAllInventories() needs an IO to remove");
            }
            for (int i = this.ios.Length - 1; i >= 0; i--) {
                InventoryData inventory = this.ios[i].Inventory;
                if (inventory != null) {
                    inventory.RemoveFromInventory(ioid);
                }
            }
        }
        /// <summary>
        /// Seeks an IO in all Inventories to replace it.
        /// </summary>
        /// <param name="ioid">the IO's reference ID</param>
        /// <param name="ioid">the replacement IO's reference ID</param>
        public void ReplaceInAllInventories(int ioid, int replacedWith)
        {
            if (!this.IsValidIo(ioid))
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Interactive.replaceInAllInventories() needs an IO to replace");
            }
            if (!this.IsValidIo(replacedWith))
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Interactive.replaceInAllInventories() needs an IO to replace with");
            }
            for (int i = this.ios.Length - 1; i >= 0; i--)
            {
                InventoryData inventory = this.ios[i].Inventory;
                if (inventory != null) {
                    inventory.ReplaceInInventory(ioid, replacedWith);
                }
            }
        }
        #endregion
        #region "VALIDATION"
        /// <summary>
        /// Gets an IO's reference id.
        /// </summary>
        /// <param name="io">the IO</param>
        /// <returns>the IO's reference id</returns>
        public int GetInteractiveRefId(InteractiveObject io) {
            if (io == null) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Interactive.getInteractiveRefId() needs a valid IO");
            }
            int refId = -1;
            for (int i = this.ios.Length - 1; i >= 0; i--) {
                if (io.Equals(this.ios[i])) {
                    refId = this.ios[i].RefId;
                    break;
                }
            }
            return refId;
        }
        /// <summary>
        /// Gets an IO instance by its id.
        /// </summary>
        /// <param name="ioid">the IO's reference id</param>
        /// <returns>the IO or null if it is not a valid reference id</returns>
        public InteractiveObject GetIo(int id)
        {
            InteractiveObject io = null;
            for (int i = this.ios.Length - 1; i >= 0; i--)
            {
                if (ios[i] != null && ios[i].RefId == id)
                {
                    io = this.ios[i];
                    break;
                }
            }
            return io;
        }
        /// <summary>
        /// Determines if two IOs are the same item. Used when stacking objects in Inventory.
        /// </summary>
        /// <param name="ioid0">the first IO</param>
        /// <param name="ioid1">io1 the second IO</param>
        /// <returns>true if the IOs are the same; false otherwise</returns>
        public bool IsSameObject(int ioid0, int ioid1)
        {
            bool isSame = false;
            InteractiveObject io0 = this.GetIo(ioid0), io1 = this.GetIo(ioid1);
            if (io0 != null && io1 != null)
            {
                if (!io0.IoFlags.Has(Globals.IO_UNIQUE) && !io1.IoFlags.Has(Globals.IO_UNIQUE))
                {
                    // neither item is unique
                    if (io0.IoFlags.Has(Globals.IO_ITEM)
                            && io1.IoFlags.Has(Globals.IO_ITEM)
                            && ((io0.OverScript == null && io1.OverScript == null) || (io0.OverScript != null && io1.OverScript != null && io0.OverScript.GetType().Equals(io1.OverScript.GetType())))) {
                        // both are items with either no local script or same type of local script
                        // TODO - have to think about comparing items with same local script source BUT NOT same local variables. What if two swords are found and ready to be stacked, but one sword has the local variable 'cursed'=true?
                        if (io0.LocalizedName.Equals(io1.LocalizedName, StringComparison.OrdinalIgnoreCase))
                        {
                            isSame = true;
                        }
                    }
                }
            }
            return isSame;
        }
        /// <summary>
        /// Validates that an IO exists in the system.
        /// </summary>
        /// <param name="ioid">the IO's reference id</param>
        /// <returns>true if the IO was found in the system; false otherwise</returns>
        public bool IsValidIo(int ioid) { return this.GetIo(ioid) != null; }
        /// <summary>
        /// Validates that an IO exists in the system.
        /// </summary>
        /// <param name="ioid">the IO/param>
        /// <returns>true if the IO was found in the system; false otherwise</returns>
        public bool IsValidIo(InteractiveObject io) { return this.GetInteractiveRefId(io) >= 0; }
        #endregion
        #region "DESTRUCTION"
        public void DestroyDynamicInfo(InteractiveObject io) {
            if (!this.IsValidIo(io)) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Interactive.destroyDynamicInfo() needs an IO to release");
            }
            if (io.IoFlags.Has(Globals.IO_ITEM)) {
                // unequip from any players
                for (int i = this.ios.Length - 1; i >= 0; i--) {
                    InteractiveObject playerIo = this.ios[i];
                    if (playerIo.IoFlags.Has(Globals.IO_PC) && ((IoPcData)playerIo.Data).HasEquipped(io)) {
                        ((IoPcData)playerIo.Data).Unequip(io);
                    }
                }
            }
            // TODO - clear all scripted events that fire later for involving this IO
            // ScriptController.Instance.EventStackClearForIo(io);
            if (this.IsValidIo(io)) {
                // TODO - clear spells on IO
            }
        }
        public void ReleaseIo(InteractiveObject io) {
            if (!this.IsValidIo(io)) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Interactive.releaseIo() needs an IO to release");
            }
            this.DestroyDynamicInfo(io);
            // TODO - clear all timers involving this IO
            // ScriptController.Instance.TimerClearForIO(io);

            io.RemoveAllSpellsOn();
            this.RemoveFromAllInventories(io.RefId);

            if (io.Script != null) {
                io.Script.ClearLocalVariables();
            }
            if (io.OverScript != null) {
                io.OverScript.ClearLocalVariables();
            }

            // clear animations

            // clear damage data

            io.RemoveFromAllGroups();

            for (int i = this.ios.Length - 1; i >= 0; i--) {
                if (this.ios[i].Equals(io)) {
                    this.ios[i] = null;
                    break;
                }
            }
            io = null;
        }
        #endregion
    }
}
