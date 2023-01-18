using Base.Exceptions;
using Bus.Services;
using Godot;
using System;
using System.Collections.Generic;

namespace Base.Resources.Scripting
{
    sealed class ScriptVariable
    {
        /// <summary>
        /// the <see cref="ScriptVariable"/>'s property.
        /// </summary>
        public int Type { get; private set; }
        /// <summary>
        /// the variable's value.
        /// </summary>
        private object value;
        /// <summary>
        /// Clears up member fields, releasing their memory.
        /// </summary>
        public void Clear()
        {
            this.Type = -1;
            this.value = null;
        }
        /// <summary>
        /// Sets the variable's value.
        /// </summary>
        /// <param name="o">the value</param>
        public void Set(object o)
        {
            if (o == null)
            {
                throw new RPGException(ErrorMessage.BAD_PARAMETERS, "ScriptVariable.Set() - parameter is null.");
            }
            this.value = o;
            if (o is float)
            {
                this.Type = Globals.TYPE_FLOAT;
            }
            else if (o is float[])
            {
                this.Type = Globals.TYPE_FLOAT_ARR;
            }
            else if (o is int)
            {
                this.Type = Globals.TYPE_INT;
            }
            else if (o is int[])
            {
                this.Type = Globals.TYPE_INT_ARR;
            }
            else if (o is long)
            {
                this.Type = Globals.TYPE_LONG;
            }
            else if (o is long[])
            {
                this.Type = Globals.TYPE_LONG_ARR;
            }
            else if (o is string)
            {
                this.Type = Globals.TYPE_TEXT;
            }
            else if (o is string[])
            {
                this.Type = Globals.TYPE_TEXT_ARR;
            }
            else if (o is bool)
            {
                this.Type = Globals.TYPE_BOOL;
            }
            else if (o is bool[])
            {
                this.Type = Globals.TYPE_BOOL_ARR;
            }
            else
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "ScriptVariable.Set() - unrecognized type");
            }
        }
        /// <summary>
        /// Gets the variable's value.
        /// </summary>
        /// <typeparam name="T">the value type</typeparam>
        /// <returns>the value</returns>
        public T Value<T>()
        {
            if (typeof(T) == typeof(float))
            {
                if (this.Type != Globals.TYPE_FLOAT)
                {
                    throw new RPGException(ErrorMessage.INVALID_DATA_TYPE, "ScriptVariable.Value() - is not float");
                }
            }
            else if (typeof(T) == typeof(float[]))
            {
                if (this.Type != Globals.TYPE_FLOAT_ARR)
                {
                    throw new RPGException(ErrorMessage.INVALID_DATA_TYPE, "ScriptVariable.Value() - is not float array");
                }
            }
            else if (typeof(T) == typeof(int))
            {
                if (this.Type != Globals.TYPE_INT)
                {
                    throw new RPGException(ErrorMessage.INVALID_DATA_TYPE, "ScriptVariable.Value() - is not int");
                }
            }
            else if (typeof(T) == typeof(int[]))
            {
                if (this.Type != Globals.TYPE_INT_ARR)
                {
                    throw new RPGException(ErrorMessage.INVALID_DATA_TYPE, "ScriptVariable.Value() - is not int array");
                }
            }
            else if (typeof(T) == typeof(long))
            {
                if (this.Type != Globals.TYPE_LONG)
                {
                    throw new RPGException(ErrorMessage.INVALID_DATA_TYPE, "ScriptVariable.Value() - is not long");
                }
            }
            else if (typeof(T) == typeof(long[]))
            {
                if (this.Type != Globals.TYPE_LONG_ARR)
                {
                    throw new RPGException(ErrorMessage.INVALID_DATA_TYPE, "ScriptVariable.Value() - is not long array");
                }
            }
            else if (typeof(T) == typeof(string))
            {
                if (this.Type != Globals.TYPE_TEXT)
                {
                    throw new RPGException(ErrorMessage.INVALID_DATA_TYPE, "ScriptVariable.Value() - is not string");
                }
            }
            else if (typeof(T) == typeof(string[]))
            {
                if (this.Type != Globals.TYPE_TEXT_ARR)
                {
                    throw new RPGException(ErrorMessage.INVALID_DATA_TYPE, "ScriptVariable.Value() - is not string array");
                }
            }
            else if (typeof(T) == typeof(bool))
            {
                if (this.Type != Globals.TYPE_BOOL)
                {
                    throw new RPGException(ErrorMessage.INVALID_DATA_TYPE, "ScriptVariable.Value() - is not bool");
                }
            }
            else if (typeof(T) == typeof(string[]))
            {
                if (this.Type != Globals.TYPE_BOOL_ARR)
                {
                    throw new RPGException(ErrorMessage.INVALID_DATA_TYPE, "ScriptVariable.Value() - is not bool array");
                }
            }
            else
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "ScriptVariable.Value() - unrecognized type");
            }
            return (T)this.value;
        }
    }
    public sealed class ScriptVariableSet
    {
        /// <summary>
        /// the set of variables.
        /// </summary>
        private Dictionary<string, ScriptVariable> set = new Dictionary<string, ScriptVariable>();
        /// <summary>
        /// Adds a new variable to the set.
        /// </summary>
        /// <param name="name">the variable's name</param>
        /// <param name="value">the variable's value</param>
        /// <param name="overridePrevious">if true, allows for a previously-set variable to be overridden.</param>
        public void Add(string name, object value, bool overridePrevious = false)
        {
            if (this.set.ContainsKey(name))
            {
                if (overridePrevious)
                {
                    set[name].Set(value);
                }
                else
                {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "ScriptVariableSet.Add() - variable already exists and is not overriden. Use Set()");
                }
            }
            else
            {
                ScriptVariable variable = new ScriptVariable();
                variable.Set(value);
                this.set.Add(name, variable);
            }
        }
        public void Clear()
        {
            this.set.Clear();
        }
        /// <summary>
        /// Determines if a variable by a specific name was set.
        /// </summary>
        /// <param name="name">the name</param>
        /// <returns>true if the variable exists; false otherwise</returns>
        public bool Has(string name) { return this.set.ContainsKey(name); }
        public void Remove(string key)
        {
            this.set.Remove(key);
        }
        /// <summary>
        /// Sets a script variable's value.
        /// </summary>
        /// <param name="name">the variable's name</param>
        /// <param name="value">the variable's value</param>
        public void Set(string name, object value)
        {
            if (this.set.ContainsKey(name))
            {
                set[name].Set(value);
            }
            else
            {
                ScriptVariable variable = new ScriptVariable();
                variable.Set(value);
                this.set.Add(name, variable);
            }
        }
        /// <summary>
        /// Gets the variable's value.
        /// </summary>
        /// <typeparam name="T">the value type</typeparam>
        /// <param name="name">the variable's name</param>
        /// <returns>the value</returns>
        public T Value<T>(string name)
        {
            if (!this.set.ContainsKey(name))
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "ScriptVariableSet.Value() - variable doesn't exist");
            }
            else
            {
                return this.set[name].Value<T>();
            }
        }
    }
}
