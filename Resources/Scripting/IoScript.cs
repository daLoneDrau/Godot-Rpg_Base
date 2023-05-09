using Base.Bus;
using Base.Exceptions;
using Base.Resources.Data;
using Base.Resources.Services;
using Bus.Services;
using Godot;
using System;
using System.Collections.Generic;

namespace Base.Resources.Scripting
{
    public abstract class IoScript : IoData
    {
        protected Dictionary<string, Action<ScriptableEventParameters>> scriptActions;
        /// <summary>
        /// the script's allowed events.
        /// </summary>
        /// <returns></returns>
        public FlagSet AllowedEvents { get; } = new FlagSet();
        /// <summary>
        /// the master script field.
        /// </summary>
        private IoScript master;
        /// <summary>
        /// the master script property.
        /// </summary>
        public IoScript Master
        {
            get { return this.master; }
            set
            {
                this.master = value;
                this.master.Io = this.Io;
            }
        }
        /// <summary>
        /// the list of times set on the IoScript. used for tracking timers.
        /// </summary>
        private ScriptTimer[] timers;
        /// <summary>
        /// the IoScript's variables.
        /// </summary>
        private ScriptVariableSet variables = new ScriptVariableSet();
        public IoScript()
        {
            Initialize();
        }
        /// <summary>
        /// Executes a scripted action.
        /// </summary>
        /// <param name="actionName">the action's name</param>
        /// <param name="parameters">any parameters applied</param>
        /// <returns></returns>
        public void Execute(ScriptableEventParameters parameters)
        {
            if (parameters.EventName == null)
            {
                parameters.EventName = GameVariablesDatabase.Instance.ScriptMessages[parameters.EventId];
            }
            scriptActions[parameters.EventName](parameters);
        }
        /// <summary>
        /// Gets a local variable value.
        /// </summary>
        /// <param name="name">the variable's name</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetLocalVariableValue<T>(string name) {
            if (!variables.Has(name)) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Missing variable");
            }
            return variables.Value<T>(name);
        }
        /// <summary>
        /// Specialized initializer for sub-classes. The purpose is to define scripted events this IoScript responds to and to load event and broadcast listeners
        /// </summary>
        protected abstract void Initialize();
        /// <summary>
        /// Clears all local variables.
        /// </summary>
        public void ClearLocalVariables() { this.variables.Clear(); }
        /// <summary>
        /// Determines if a IoScript has a variable.
        /// </summary>
        /// <param name="name">the variable's name</param>
        /// <returns></returns>
        public bool HasLocalVariable(string name) { return this.variables.Has(name); }
        /// <summary>
        /// Sets an IoScript variable.
        /// </summary>
        /// <param name="name">the variable's key</param>
        /// <param name="value">the variable's value</param>
        public void SetLocalVariable(string name, object value) {
            if (name == null || value == null) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT,  "need key-value pair when setting script variables");
            }
            if (name.Length == 0) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "key cannot be an empty string");
            }
            variables.Set(name, value);
        }
    }
}
