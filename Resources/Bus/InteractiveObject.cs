using Base.Bus;
using Base.Exceptions;
using Base.Resources.Data;
using Base.Resources.Inventory;
using Base.Resources.Scripting;
using Base.Resources.Services;
using Bus.Services;
using Godot;
using System;
using System.Collections.Generic;

namespace Base.Resources.Bus
{
    public class InteractiveObject : Resource
    {
        private IoData data;
        /// <summary>
        /// the IO's data.
        /// </summary>
        public IoData Data
        {
            get
            {
                return data;
            }
            set
            {
                if (value == null) {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.data - no data was provided");
                }
                if (typeof(IoItemData).IsInstanceOfType(value)  && !this.IoFlags.Has(Globals.IO_ITEM)) {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.data - cannot set Item data without setting ITEM flag first");
                }
                if (typeof(IoNpcData).IsInstanceOfType(value)  && !this.IoFlags.Has(Globals.IO_NPC)) {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.data - cannot set NPC data without setting NPC flag first");
                }
                if (typeof(IoPcData).IsInstanceOfType(value)  && !this.IoFlags.Has(Globals.IO_PC)) {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.data - cannot set PC data without setting PC flag first");
                }
                data = value;
                if (data.Io == null) {
                    data.Io = this;
                }
            }
        }
        /// <summary>
        /// The total amount of damage the IO has taken. Damage is taken and after a period of time, a scripted message is sent to the IO and the amount is cleared.
        /// </summary>
        /// <value></value>
        public float DamageSum { get; set; }
        /// <summary>
        /// the IO's groups.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <typeparam name="int"></typeparam>
        /// <returns></returns>
        private Dictionary<string, int> groups = new Dictionary<string, int>();
        /// <summary>
        /// the IO's inventory.
        /// </summary>
        private InventoryData inventory;
        /// <summary>
        /// the IO's data.
        /// </summary>
        public InventoryData Inventory
        {
            get
            {
                return inventory;
            }
            set
            {
                inventory = value;
                if (inventory.Io == null)
                {
                    inventory.Io = this;
                }
            }
        }
        /// <summary>
        /// the IO's flags.
        /// </summary>
        public FlagSet IoFlags { get; private set; } = new FlagSet();
        private string localizedName
        {
            get { return localizedName; }
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.localizedName must be a string of 1 or more characters");
                }
            }
        }
        /// <summary>
        /// the IO's localized name.
        /// </summary>
        public string LocalizedName { get; set; }
        private string mainEvent;
        public string MainEvent
        {
            get { return mainEvent; }
            set
            {
                if (value != null &&
                        (value.ToLower() == "main" || value.Length == 0)) {
                    mainEvent = null;
                } else {
                    mainEvent = value;
                }
            }
        }
        /// <summary>
        /// The last time an "Ouch" scripted message was sent to the IO. This could be a turn number, or a time in milliseconds
        /// </summary>
        /// <value></value>
        public long OuchTime { get; set; }
        private IoScript overScript;
        /// <summary>
        /// the IO's extended script. This script executes first, then the base.
        /// </summary>
        /// <value></value>
        public IoScript OverScript
        {
            get { return this.overScript; }
            set
            {
                this.overScript = value;
                if (this.overScript.Io == null)
                {
                    this.overScript.Io = this;
                }
            }
        }
        /// <summary>
        /// the level of posion the IO's poisonous attack inflicts.
        /// </summary>
        /// <value></value>
        public int Poisonous { get; set; }
        /// <summary>
        /// the number of poisonous attacks the IO can inflict.
        /// </summary>
        /// <value></value>
        public int PoisonousCharges { get; set; }
        /// <summary>
        /// the IO's reference id.
        /// </summary>
        /// <value></value>
        public int RefId { get; set; }
        /// <summary>
        /// The IO's base script.
        /// </summary>
        private IoScript script;
        /// <summary>
        /// the IO's base script.
        /// </summary>
        /// <value></value>
        public IoScript Script
        {
            get { return this.script; }
            set
            {
                this.script = value;
                if (this.script.Io == null)
                {
                    this.script.Io = this;
                }
            }
        }
        /// <summary>
        /// the ids of all spells currently on the IO.
        /// </summary>
        private int[] spellsOn = new int[0];
        /// <summary>
        /// Adds the IO to a group.
        /// </summary>
        /// <param name="group">the group</param>
        public void AddToGroup(string group) {
            if (group == null || group.Length == 0)
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.removeFromGroup() must be a string of 1 or more characters");
            }
            this.groups.Add(group.ToLower(), 0);
        }
        /// <summary>
        /// Checks to see if an event is allowed to run on a script.
        /// </summary>
        /// <param name=EventName">the name of the event</param>
        /// <param name=Script">the script being processed</param>
        /// <returns>true if the event is allowed; false otherwise</returns>
        private bool CheckIsEventAllowed(string eventName, IoScript script)
        {
            bool continueProcessing = true;
            try
            {
                if (script.AllowedEvents.Has(GameVariablesDatabase.Instance.EventsWithDisabledFlags[eventName]))
                {
                    continueProcessing = false;
                }
            }
            catch (RPGException) { }
            return continueProcessing;
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            bool b = false;
            b = this.RefId == ((InteractiveObject)obj).RefId;
            if (b)
            {
                b = this.IoFlags.Equals(((InteractiveObject)obj).IoFlags);
            }
            if (b)
            {
                b = this.data.Equals(((InteractiveObject)obj).data);
            }
            return b;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg0"></param>
        private void ExecuteScript(ScriptableEventParameters eventParameters)
        {
            GD.Print("ExecuteScript");
            if (eventParameters == null)
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.ExecuteScriptedEvent()  requires a parameter object");
            }
            bool continueProcessing = false;
            if (eventParameters.TargetIo >= 0)
            {
                if (eventParameters.TargetIo == RefId)
                {
                    // this is a targeted event for this IO
                    continueProcessing = true;
                }
            }
            else
            {
                GD.Print("not targeting IO");
                // event is not targeting a specific IO. is it a Group blast?
                if (eventParameters.Parameters.ContainsKey("group"))
                {
                    if (IsInGroup((string)eventParameters.Parameters["group"]))
                    {
                        continueProcessing = true;
                    }
                }
            }
            if (continueProcessing)
            {
                GD.Print("need to continue processing");
                int ret = -1;
                string message;
                try
                {
                    message = GameVariablesDatabase.Instance.ScriptMessages[eventParameters.EventId];
                }
                catch (RPGException)
                {
                    message = eventParameters.EventName;
                }
                if (message == null || message.Length == 0)
                {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.ExecuteScriptedEvent() requires an event id or name to process");
                }
                eventParameters.EventName = message;

                if (message.Equals("INIT", StringComparison.OrdinalIgnoreCase) || message.Equals("INITEND", StringComparison.OrdinalIgnoreCase))
                {
                    // run INIT scripts in reverse order (the local script first, then the main script)
                    this.ExecuteScriptEventInReverse(eventParameters);
                }
                else
                {
                    if (OverScript == null)
                    {
                        ExecuteScriptEvent(Script, eventParameters);
                        ret = eventParameters.RetValue;
                    }
                    else
                    {
                        // send to Extended(over) script 1st, then to base if not refused
                        ExecuteScriptEvent(OverScript, eventParameters);
                        ret = eventParameters.RetValue;
                        if (ret != Globals.REFUSE) {
                            // make sure the IO is still valid after running the Extended(over) Script
                            if (IoFactory.Instance.IsValidIo(this))
                            {
                                ExecuteScriptEvent(Script, eventParameters);
                                ret = eventParameters.RetValue;
                            }
                        }
                    }
                }
            }
        }
        private void ExecuteScriptEvent(IoScript script, ScriptableEventParameters eventParameters) {
            if (eventParameters == null)
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.ExecuteScript()  requires a parameter object");
            }
            int ret = -9999;
            if (script == null)
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.ExecuteScript() requires a valid script");
            }
            if (eventParameters.EventId == -1 && (eventParameters.EventName == null || eventParameters.EventName.Length == 0))
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.ExecuteScript() requires an event id or name to process");
            }
            string message;
            try
            {
                message = GameVariablesDatabase.Instance.ScriptMessages[eventParameters.EventId];
            }
            catch (RPGException)
            {
                message = eventParameters.EventName;
            }
            if (message == null || message.Length == 0)
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.ExecuteScript() requires an event id or name to process");
            }
            if (IoFactory.Instance.IsValidIo(this))
            {
                if (IoFlags.Has(Globals.IO_NPC))
                {
                    // if a dead NPC gets a message other than "DEAD" or "DIE", just "ACCEPT"]
                    // it and move on
                    if (((IoNpcData)Data).IsDead()
                        && !message.Equals("DEAD", System.StringComparison.OrdinalIgnoreCase)
                        && !message.Equals("DIE", System.StringComparison.OrdinalIgnoreCase))
                    {
                        ret = Globals.ACCEPT;
                    }
                }
            }
            if (ret == -9999) {
                // if there is a master script, use that one for checking local variables
                IoScript localVarScript = Script.Master;
                if (localVarScript == null) {
                    localVarScript = Script;
                }
                if (!CheckIsEventAllowed(message, localVarScript))
                {
                    ret = Globals.REFUSE;
                }
                if (ret != Globals.REFUSE) {
                    localVarScript.Execute(eventParameters);
                    ret = eventParameters.RetValue;
                }
            }
        }
        /// <summary>
        /// Executes a scripted event in reverse, running it in the 
        /// </summary>
        /// <param name="eventParameters"></param>
        private void ExecuteScriptEventInReverse(ScriptableEventParameters eventParameters)
        {
            int ret = Globals.REFUSE;
            // if this IO only has a Local script, send event to it
            if (OverScript != null)
            {
                this.ExecuteScriptEvent(OverScript, eventParameters);
                ret = eventParameters.RetValue;
            } else {
                this.ExecuteScriptEvent(Script, eventParameters);
                ret = eventParameters.RetValue;
                if (eventParameters.RetValue != Globals.REFUSE) {
                    // make sure the IO is still valid after running the Script
                    if (IoFactory.Instance.IsValidIo(this) && OverScript != null)
                    {
                        this.ExecuteScriptEvent(OverScript, eventParameters);
                        ret = eventParameters.RetValue;
                    }
                }
            }
            eventParameters.RetValue = ret;
        }
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            // throw new System.NotImplementedException();
            return base.GetHashCode();
        }
        /// <summary>
        /// Determines if the IO belongs to a specific group.
        /// </summary>
        /// <param name="group">the group's name</param>
        /// <returns>true if the IO belongs to the group; false otherwise</returns>
        public bool IsInGroup(string group) {
            if (group == null || group.Length == 0)
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.isInGroup() must be a string of 1 or more characters");
            }
            return this.groups.ContainsKey(group.ToLower());
        }
        /// <summary>
        /// Removes an IO from all groups.
        /// </summary>
        public void RemoveFromAllGroups() {
            this.groups.Clear();
        }
        /// <summary>
        /// Removes all spells affecting the IO.
        /// </summary>
        public void RemoveAllSpellsOn() {
            for (int i = this.spellsOn.Length - 1; i >= 0; i--)
            {
                this.spellsOn = ArrayUtilities.Instance.RemoveIndex(i, this.spellsOn);
            }
        }
        /// <summary>
        /// Removes an IO from a specific group.
        /// </summary>
        /// <param name="group">the group's name</param>
        public void RemoveFromGroup(string group) {
            if (group == null || group.Length == 0)
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.RemoveFromGroup() must be a string of 1 or more characters");
            }
            this.groups.Remove(group);
        }
        public void Reset(bool needsInitialization = false)
        {
            if (!IoFactory.Instance.IsValidIo(this))
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "InteractiveObject.Reset() - IO is not valid");
            }
            if (Script != null)
            {
                Script.AllowedEvents.Clear();
                if (needsInitialization)
                {
                    this.ExecuteScript(new ScriptableEventParameters()
                    {
                        EventId = Globals.SM_INIT,
                        TargetIo = this.RefId
                    });
                }
                if (IoFactory.Instance.IsValidIo(this))
                {
                    this.MainEvent = "MAIN";
                }
            }

            // Do the same for Local Script
            if (OverScript != null)
            {
                OverScript.AllowedEvents.Clear();

                if (needsInitialization)
                {
                    this.ExecuteScript(new ScriptableEventParameters()
                    {
                        EventId = Globals.SM_INIT,
                        TargetIo = this.RefId
                    });
                }
            }

            // Sends InitEnd Event
            if (needsInitialization)
            {
                if (Script != null)
                {
                    this.ExecuteScript(new ScriptableEventParameters()
                    {
                        EventId = Globals.SM_INITEND,
                        TargetIo = this.RefId
                    });
                }
                if (OverScript != null) {
                    this.ExecuteScript(new ScriptableEventParameters()
                    {
                        EventId = Globals.SM_INITEND,
                        TargetIo = this.RefId
                    });
                }
            }
        }
    }
}
