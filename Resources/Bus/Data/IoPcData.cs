using Base.Bus;
using Base.Exceptions;
using Base.Resources.Bus;
using Base.Resources.Events;
using Base.Resources.Inventory;
using Base.Resources.Scripting;
using Base.Resources.Services;
using Bus.Services;
using Godot;
using System;
using System.Collections.Generic;

namespace Base.Resources.Data
{
    public abstract class IoPcData : IoData
    {
        /// <summary>
        /// The collection of player attributes.
        /// </summary>
        /// <returns></returns>
        public AttributeCollection Attributes { get; private set; } = new AttributeCollection();
        /// <summary>
        /// the list of items the player has equipped.
        /// </summary>
        protected int[] equipped;
        /// <summary>
        /// the player's gold field.
        /// </summary>
        /// <value></value>
        private int gold = 0;
        /// <summary>
        /// the player's Gold property.
        /// </summary>
        /// <value></value>
        public int Gold
        {
            get { return this.gold; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                this.gold = value;
                // broadcast signal that gold have changed
                PublicBroadcastService.Instance.PcEventChannel.Broadcast(new PcEventSignal(Io.RefId, Globals.PLAYER_EVENT_UPDATE_ATTRIBUTES));
            }
        }
        /// <summary>
        /// the player's level field.
        /// </summary>
        /// <value></value>
        private int level = 0;
        /// <summary>
        /// the player's level property.
        /// </summary>
        /// <value></value>
        public int Level
        {
            get { return this.level; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                level = value;
                // broadcast signal that attributes have changed
                PublicBroadcastService.Instance.PcEventChannel.Broadcast(new PcEventSignal(Io.RefId, Globals.PLAYER_EVENT_UPDATE_ATTRIBUTES));
            }
        }
        /// <summary>
        /// the Player's flags.
        /// </summary>
        public FlagSet PlayerFlags { get; private set; } = new FlagSet();
        /// <summary>
        /// The amount of poison the PC has taken.
        /// </summary>
        /// <value></value>
        public float Poison { get; set; }
        /// <summary>
        /// The PC's level of posion resistance. In ARX, it was 0-255.
        /// </summary>
        /// <value></value>
        public int ResistPoison { get; set; }
        /// <summary>
        /// the player's xp field.
        /// </summary>
        /// <value></value>
        private int xp = 0;
        /// <summary>
        /// the player's XP property.
        /// </summary>
        /// <value></value>
        public int Xp
        {
            get { return this.xp; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                this.xp = value;
                // broadcast signal that xp has changed
                PublicBroadcastService.Instance.PcEventChannel.Broadcast(new PcEventSignal(Io.RefId, Globals.PLAYER_EVENT_UPDATE_ATTRIBUTES));
            }
        }
        public abstract void SetPlayerAttributes();
        /*
        PlayerAttribute pa = new PlayerAttribute(
            (AttributeDescriptor)AssetDatabase.LoadAssetAtPath(cleanedPath, typeof(AttributeDescriptor)),
            PcEventListener,
            PcEventsDatabase["PlayerAttributeUpdate"]);
        Attributes.Add(pa);
        */
        /// <summary>
        /// Applies modifiers to the character's attributes and skills based on the game rules.
        /// </summary>
        protected abstract void ApplyRulesModifiers();
        /// <summary>
        /// Applies percentage modifiers to the character's attributes and skills based on the game rules.
        /// </summary>
        protected abstract void ApplyRulesPercentModifiers();
        /// <summary>
        /// Handles steps taken when the player dies, such as spells being turned off, or the camer adjusted.
        /// </summary>
        public abstract void BecomesDead();
        /// <summary>
        /// Clears all element modifiers.
        /// </summary>
        public void ClearElementModifiers() {
            foreach(PlayerAttribute attribute in this.Attributes.Attributes)
            {
                attribute.ClearModifiers();
            }
        }
        public void ComputeFullStats() {
            this.ComputeStats();
            this.ClearElementModifiers();
            // TODO - identify any equipment if needed

            // apply modifiers
            foreach(PlayerAttribute attribute in Attributes.Attributes)
            {
                attribute.AdjustModifier(GetEquipmentModifiers(attribute.ElementModifier));
            }
            this.ApplyRulesModifiers();
            // apply percentage modifiers
            foreach(PlayerAttribute attribute in this.Attributes.Attributes)
            {
                attribute.AdjustModifier(GetEquipmentPercentageModifiers(attribute.ElementModifier, attribute.Base));
            }
            this.ApplyRulesPercentModifiers();

            // TODO - check for spell modifiers

            // TODO - set life, maxlife, mana, maxmana

            // broadcast signal that attributes have been updated
            PublicBroadcastService.Instance.PcEventChannel.Broadcast(new PcEventSignal(Io.RefId, Globals.PLAYER_EVENT_UPDATE_ATTRIBUTES));
        }
        public abstract void ComputeStats();
        public abstract float DamagePlayer(float damage, FlagSet type, int source);
        /// <summary>
        /// Equips an item.
        /// </summary>
        /// <param name="itemIo">the item being equipped</param>
        public void Equip(InteractiveObject itemIo)
        {
            if (!IoFactory.Instance.IsValidIo(itemIo)) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "IoPcData.equip() requires a valid IO");
            }
            IoFactory.Instance.RemoveFromAllInventories(itemIo.RefId);
            int slot = Globals.EQUIP_SLOT_WEAPON;
            if (((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_DAGGER)
                    || ((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_1H)
                    || ((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_2H)
                    || ((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_BOW))
            {
                if (IoFactory.Instance.IsValidIo(this.equipped[slot]))
                {
                    // Unequip the old weapon
                    Unequip(IoFactory.Instance.GetIo(this.equipped[slot]));
                }
                // equip the new weapon
                equipped[slot] = itemIo.RefId;
                GD.Print("\tEquipping ", itemIo.LocalizedName, " in slot ", slot);
                
                // send message to player that they are equipping an item
                PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
                {
                    EventId = Globals.SM_EQUIPIN,
                    EventSender = itemIo,
                    TargetIo = Io.RefId
                });

                // send message to IO that it is being unequipped
                PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
                {
                    EventId = Globals.SM_EQUIPIN,
                    EventSender = Io,
                    TargetIo = itemIo.RefId
                });
                // check to see if equipping a 2-handed weapon while holding a shield
                if (((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_2H) || ((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_BOW))
                {
                    slot = Globals.EQUIP_SLOT_SHIELD;
                    if (IoFactory.Instance.IsValidIo(this.equipped[slot]))
                    {
                        // Unequip shield
                        Unequip(IoFactory.Instance.GetIo(this.equipped[slot]));
                    }
                }
            }
            else if (((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_SHIELD))
            {
                slot = Globals.EQUIP_SLOT_SHIELD;
                if (IoFactory.Instance.IsValidIo(equipped[slot]))
                {
                    // Unequip the old shield
                    Unequip(IoFactory.Instance.GetIo(equipped[slot]));
                }
                // equip the new shield
                equipped[slot] = itemIo.RefId;
                GD.Print("\tEquipping ", itemIo.LocalizedName, " in slot ", slot);
                
                // send message to player that they are equipping an item
                PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
                {
                    EventId = Globals.SM_EQUIPIN,
                    EventSender = itemIo,
                    TargetIo = Io.RefId
                });

                // send message to IO that it is being unequipped
                PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
                {
                    EventId = Globals.SM_EQUIPIN,
                    EventSender = Io,
                    TargetIo = itemIo.RefId
                });
                // check to see if equipping a shield while holding a 2-handed weapon
                slot = Globals.EQUIP_SLOT_WEAPON;
                if (IoFactory.Instance.IsValidIo(equipped[slot]))
                {
                    InteractiveObject wpnIo = IoFactory.Instance.GetIo(this.equipped[slot]);
                    if (((IoItemData)wpnIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_2H) || ((IoItemData)wpnIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_BOW))
                    {
                        // Unequip weapon
                        Unequip(wpnIo);
                    }
                }
            }
            else if (((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_RING))
            {
                // TODO - check to see if player already has that type of ring equipped

                // check to see whic finger is available. left ring is selected first
                slot = Globals.EQUIP_SLOT_RING_LEFT;
                if (IoFactory.Instance.IsValidIo(equipped[slot])) {
                    slot = Globals.EQUIP_SLOT_RING_RIGHT;
                }
                if (IoFactory.Instance.IsValidIo(equipped[slot])) {
                    slot = Globals.EQUIP_SLOT_RING_LEFT;
                }
                if (IoFactory.Instance.IsValidIo(equipped[slot])) {
                    // Unequip the old ring
                    Unequip(IoFactory.Instance.GetIo(equipped[slot]));
                }
                // equip the new ring
                equipped[slot] = itemIo.RefId;
                GD.Print("\tEquipping ", itemIo.LocalizedName, " in slot ", slot);
                
                // send message to player that they are equipping an item
                PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
                {
                    EventId = Globals.SM_EQUIPIN,
                    EventSender = itemIo,
                    TargetIo = Io.RefId
                });

                // send message to IO that it is being unequipped
                PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
                {
                    EventId = Globals.SM_EQUIPIN,
                    EventSender = Io,
                    TargetIo = itemIo.RefId
                });
            } else if (((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_ARMOR)
                    || ((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_LEGGINGS)
                    || ((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_HELMET))
            {
                if (((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_ARMOR))
                {
                    slot = Globals.EQUIP_SLOT_TORSO;
                }
                if (((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_LEGGINGS))
                {
                    slot = Globals.EQUIP_SLOT_LEGGINGS;
                }
                if (((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_HELMET))
                {
                    slot = Globals.EQUIP_SLOT_HELMET;
                }
                if (IoFactory.Instance.IsValidIo(equipped[slot])) {
                    // Unequip the old armour
                    Unequip(IoFactory.Instance.GetIo(equipped[slot]));
                }
                // equip the new armour
                equipped[slot] = itemIo.RefId;
                GD.Print("\tEquipping ", itemIo.LocalizedName, " in slot ", slot);
                
                // send message to player that they are equipping an item
                PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
                {
                    EventId = Globals.SM_EQUIPIN,
                    EventSender = itemIo,
                    TargetIo = Io.RefId
                });

                // send message to IO that it is being unequipped
                PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
                {
                    EventId = Globals.SM_EQUIPIN,
                    EventSender = Io,
                    TargetIo = itemIo.RefId
                });
                RecreateMesh();
            } else {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "IoPcData.equip() cannot equip unrecognized object type");
            }
            ComputeFullStats();
        }
        /// <summary>
        /// Gets the total modifier for a specific element type from the equipment the player is wielding.
        /// </summary>
        /// <param name="element">the element modifier id. these represent element's of the player being modified, such as Strength score, or Saving Throw vs Illusion</param>
        /// <returns>the element modifier total</returns>
        public int GetEquipmentModifiers(int element)
        {
            int toadd = 0;
            for (int i = equipped.Length - 1; i >= 0; i--) {
                if (IoFactory.Instance.IsValidIo(equipped[i]))
                {
                    InteractiveObject itemIo = IoFactory.Instance.GetIo(equipped[i]);
                    if (itemIo.IoFlags.Has(Globals.IO_ITEM))
                    {
                        EquipmentItemModifier modifier = ((IoItemData)itemIo.Data).GetElementModifier(element);
                        if (!modifier.Percent)
                        {
                            toadd += modifier.Value;
                        }
                    }
                }
            }
            return toadd;
        }
        /// <summary>
        /// Gets the total percentage modifier for a specific element type from the equipment the player is wielding.
        /// </summary>
        /// <param name="element">the element modifier id</param>
        /// <returns>the element percentage modifier total</returns>
        public float GetEquipmentPercentageModifiers(int element, float trueValue)
        {
            int toadd = 0;
            for (int i = this.equipped.Length - 1; i >= 0; i--) {
                if (IoFactory.Instance.IsValidIo(this.equipped[i]))
                {
                    InteractiveObject itemIo = IoFactory.Instance.GetIo(this.equipped[i]);
                    if (itemIo.IoFlags.Has(Globals.IO_ITEM)) {
                        EquipmentItemModifier modifier = ((IoItemData)itemIo.Data).GetElementModifier(element);
                        if (modifier.Percent) {
                            toadd += modifier.Value;
                        }
                    }
                }
            }
            return toadd * trueValue * Globals.DIV100;
        }
        public int GetWeaponType()
        {
            int type = Globals.WEAPON_BARE;
            if (IoFactory.Instance.IsValidIo(this.equipped[Globals.EQUIP_SLOT_WEAPON])) {
                InteractiveObject wpnIo = IoFactory.Instance.GetIo(this.equipped[Globals.EQUIP_SLOT_WEAPON]);
                if (((IoItemData)wpnIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_DAGGER)) {
                    type = Globals.WEAPON_DAGGER;
                }
                if (((IoItemData)wpnIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_1H)) {
                    type = Globals.WEAPON_1H;
                }
                if (((IoItemData)wpnIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_2H)) {
                    type = Globals.WEAPON_2H;
                }
                if (((IoItemData)wpnIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_BOW)) {
                    type = Globals.WEAPON_BOW;
                }
            }
            return type;
        }
        /// <summary>
        /// Gets the item the pc has equipped in a specific slot.
        /// </summary>
        /// <param name="slot">the slot</param>
        /// <returns></returns>
        public InteractiveObject GetEquippedItem(int slot)
        {
            InteractiveObject io = null;
            if (IoFactory.Instance.IsValidIo(equipped[slot])) {
                io = IoFactory.Instance.GetIo(equipped[slot]);
            }
            return io;
        }
        public bool HasEquipped(InteractiveObject itemIo)
        {
            if (!IoFactory.Instance.IsValidIo(itemIo)) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "IoPcData.hasEquipped() requires a valid IO");
            }
            bool equipped = false;
            for (int i = this.equipped.Length - 1; i >= 0; i--) {
                if (this.equipped[i] >= 0
                        && IoFactory.Instance.IsValidIo(this.equipped[i])
                        && IoFactory.Instance.GetIo(this.equipped[i]).Equals(itemIo)) {
                    equipped = true;
                }
            }
            return equipped;
        }
        public abstract bool IsDead();
        public void LevelUp()
        {
            this.level++;
            // TODO - set stats
            this.ComputeStats();
            // TODO - set life an mana to full

            // send a scriptable event to the parent IO that it has levelled up
            PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
            {
                EventName = "LEVEL_UP",
                TargetIo = Io.RefId
            });
        }
        public abstract void PutInFrontOfPlayer(InteractiveObject itemIo, int flag);
        public abstract void RecreateMesh();
        public void ReleaseEquippedIo(int ioid)
        {
            for (int i = this.equipped.Length - 1; i >= 0; i--) {
                if (this.equipped[i] == ioid) {
                    this.equipped[i] = -1;
                }
            }
        }
        /// <summary>
        /// Unequips an item.
        /// </summary>
        /// <param name="itemIo">the item being unequipped</param>
        /// <param name="itemDestroyed">if false, the item is moved to inventory or put in front of the player; otherwise it is destroyed.</param>
        public void Unequip(InteractiveObject itemIo, bool itemDestroyed = false)
        {
            if (!IoFactory.Instance.IsValidIo(itemIo))
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "IoPcData.unequip() requires a valid IO");
            }
            if (!HasEquipped(itemIo))
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "IoPcData.unequip() item was not equipped");
            }
            for (var i = equipped.Length - 1; i >= 0; i--)
            {
                if (equipped[i] == itemIo.RefId)
                {
                    equipped[i] = -1;
                    GD.Print("\tUN-Equipping ", itemIo.LocalizedName, " in slot ", i);
                    if (!itemDestroyed)
                    {
                        if (!Io.Inventory.CanBePutInInventory(itemIo.RefId))
                        {
                            PutInFrontOfPlayer(itemIo, 1);
                        }
                    }
                    // send message to player that they are unequipping an item
                    PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
                    {
                        EventId = Globals.SM_EQUIPOUT,
                        EventSender = itemIo,
                        TargetIo = Io.RefId
                    });

                    // send message to IO that it is being unequipped
                    PublicBroadcastService.Instance.ScriptableEventChannel.Broadcast(new ScriptableEventParameters()
                    {
                        EventId = Globals.SM_EQUIPOUT,
                        EventSender = Io,
                        TargetIo = itemIo.RefId
                    });
                    break;
                }
            }
            if (((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_HELMET)
                    || ((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_ARMOR)
                    || ((IoItemData)itemIo.Data).TypeFlags.Has(Globals.OBJECT_TYPE_LEGGINGS))
            {
                RecreateMesh();
            }
            ComputeFullStats();
        }
        /// <summary>
        /// Unequips all items.
        /// </summary>
        public void UnequipAll()
        {
            for (int i = this.equipped.Length - 1; i >= 0; i--) {
                if (IoFactory.Instance.IsValidIo(this.equipped[i])) {
                    this.Unequip(IoFactory.Instance.GetIo(this.equipped[i]));
                }
            }
            this.ComputeFullStats();
        }
    }
    /// <summary>
    /// A collection of player attributes.
    /// </summary>
    public class AttributeCollection
    {
        /// <summary>
        /// the internal collection
        /// </summary>
        /// <typeparam name="PlayerAttribute"></typeparam>
        /// <returns></returns>
        private List<PlayerAttribute> list =  new List<PlayerAttribute>();
        /// <summary>
        /// Gets the attribute by its id. Not for use with all systems.
        /// </summary>
        /// <value></value>
        public PlayerAttribute this[int id]
        {
            get
            {
                int found = -1;
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].Id == id)
                    {
                        found = i;
                        break;
                    }
                }
                if (found == -1)
                {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Invalid Attribute Id " + id);
                }
                return list[found];
            }
        }
        /// <summary>
        /// Gets the attribute by its abbreviation. Not for use with all systems.
        /// </summary>
        /// <value></value>
        public PlayerAttribute this[string id]
        {
            get
            {
                int found = -1;
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].AttributeDescriptor.Abbreviation.Equals(id, StringComparison.OrdinalIgnoreCase))
                    {
                        found = i;
                        break;
                    }
                    else if (list[i].AttributeDescriptor.Title != null && list[i].AttributeDescriptor.Title.Equals(id, StringComparison.OrdinalIgnoreCase))
                    {
                        found = i;
                        break;
                    }
                }
                if (found == -1)
                {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Invalid Attribute Abbreviation " + id);
                }
                return list[found];
            }
        }
        /// <summary>
        /// Adds an attribute to the collection.
        /// </summary>
        /// <param name="value"></param>
        public void Add(PlayerAttribute value)
        {
            list.Add(value);
        }
        /// <summary>
        /// Property field to get the list of all attribute fields.
        /// </summary>
        /// <value></value>
        public List<string> AttributeNames
        {
            get
            {
                this.list.Sort((a, b) =>
                {
                    int c = 0;
                    if (a.AttributeDescriptor.SortOrder < b.AttributeDescriptor.SortOrder)
                    {
                        c = -1;
                    }
                    else if (a.AttributeDescriptor.SortOrder > b.AttributeDescriptor.SortOrder)
                    {
                        c = 1;
                    }
                    return c;
                });
                List<string> keys = new List<string>();
                for (int i = this.list.Count - 1; i >= 0; i--)
                {
                    keys.Add(this.list[i].Abbr);
                }
                return keys;
            }
        }
        /// <summary>
        /// Property field to get the list of all attribute fields.
        /// </summary>
        /// <value></value>
        public List<PlayerAttribute> Attributes
        {
            get
            {
                this.list.Sort((a, b) =>
                {
                    int c = 0;
                    if (a.AttributeDescriptor.SortOrder < b.AttributeDescriptor.SortOrder)
                    {
                        c = -1;
                    }
                    else if (a.AttributeDescriptor.SortOrder > b.AttributeDescriptor.SortOrder)
                    {
                        c = 1;
                    }
                    return c;
                });
                List<PlayerAttribute> keys = new List<PlayerAttribute>();
                for (int i = this.list.Count - 1; i >= 0; i--)
                {
                    keys.Add(this.list[i]);
                }
                return keys;
            }
        }
    }
}
