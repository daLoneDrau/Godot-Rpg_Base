using Base.Bus;
using Base.Exceptions;
using Base.Pooled;
using Base.Resources.Scripting;
using Base.Resources.Variables;
using Bus.Services;
using Godot;
using System;
using System.Collections.Generic;

namespace Base.Resources.Services
{
    public class GameVariablesDatabase : Node
    {
        /// <summary>
        /// Reference to the singleton instance.
        /// </summary>
        /// <value></value>
        public static GameVariablesDatabase Instance { get; private set; }
        /// <summary>
        /// The dictionary of global bool constants
        /// </summary>
        /// <typeparam name="string">the name of the bool variable</typeparam>
        /// <typeparam name="StringVariable">the bool variable instance</typeparam>
        /// <returns></returns>
        private Dictionary<string, BoolVariable> globalBoolConstants = new Dictionary<string, BoolVariable>();
        /// <summary>
        /// The dictionary of screen states
        /// </summary>
        /// <typeparam name="string">the name of the screen state</typeparam>
        /// <typeparam name="GameEvent">the screen state instance</typeparam>
        /// <returns></returns>
        private Dictionary<string, IntVariable> globalIntConstants = new Dictionary<string, IntVariable>();
        /// <summary>
        /// The dictionary of global float constants
        /// </summary>
        /// <typeparam name="string">the name of the float variable</typeparam>
        /// <typeparam name="FloatVariable">the float variable instance</typeparam>
        /// <returns></returns>
        private Dictionary<string, FloatVariable> globalFloatConstants = new Dictionary<string, FloatVariable>();
        /// <summary>
        /// The dictionary of global string constants
        /// </summary>
        /// <typeparam name="string">the name of the string variable</typeparam>
        /// <typeparam name="StringVariable">the string variable instance</typeparam>
        /// <returns></returns>
        private Dictionary<string, StringVariable> globalStringConstants = new Dictionary<string, StringVariable>();
        /// <summary>
        /// The dictionary of global string array constants and variables.
        /// </summary>
        /// <typeparam name="string">the name of the string array variable</typeparam>
        /// <typeparam name="StringVariable">the string array variable instance</typeparam>
        /// <returns></returns>
        private Dictionary<string, StringArrayVariable> globalStringArrayConstants = new Dictionary<string, StringArrayVariable>();
        /// <summary>
        /// the game's global variable set.
        /// </summary>
        public ScriptVariableSet GlobalVariables { get; private set; } = new ScriptVariableSet();
        public EventsWithDisabledFlagsIndexer EventsWithDisabledFlags { get; private set; }
        public ScriptMessagesIndexer ScriptMessages { get; private set; }
        #region VARIABLE INDEXERS
        public VariableIndexer<string, BoolVariable> BoolVariable { get; private set; }
        public VariableIndexer<string, IntVariable> IntVariable { get; private set; }
        public VariableIndexer<string, FloatVariable> FloatVariable { get; private set; }
        public VariableIndexer<string, StringVariable> StringVariable { get; private set; }
        public VariableIndexer<string, StringArrayVariable> StringArrayVariable { get; private set; }
        #endregion
        /// <summary>
        /// Recursively loads a directory, storing all identifiable resources in variable indexers.
        /// </summary>
        /// <param name="path"></param>
        private void LoadDirectory(params string[] path)
        {
            PooledStringBuilder sb = StringBuilderPool.Instance.GetStringBuilder();
            sb.Append(GameController.Instance.VariablesFolderPath);
            for (int i = 0, li = path.Length; i < li; i++) {
                sb.Append("/");
                sb.Append(path[i]);
            }
            Directory dir = new Directory();
            if (dir.Open(sb.ToString()) == Godot.Error.Ok)
            {
                dir.ListDirBegin(skipNavigational: true);
                string fileName = dir.GetNext();
                while (!fileName.Equals("", StringComparison.OrdinalIgnoreCase))
                {
                    string[] arr = ArrayUtilities.Instance.CopyArray(path);
                    arr = ArrayUtilities.Instance.ExtendArray(fileName, arr);
                    if (dir.CurrentIsDir())
                    {
                        GD.Print("Found directory:", fileName);
                        LoadDirectory(arr);
                    }
                    else
                    {
                        GD.Print("Found file:", fileName);

                        string key = fileName.Substr(0, fileName.Length - 5);
                        
                        sb.Length = 0;
                        sb.Append(GameController.Instance.VariablesFolderPath);
                        for (int i = 0, li = arr.Length; i < li; i++) {
                            sb.Append("/");
                            sb.Append(arr[i]);
                        }
                        Resource so = GD.Load(sb.ToString());
                        if (so is BoolVariable)
                        {
                            if (globalBoolConstants.ContainsKey(key))
                            {
                                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Constant defined: " + key);
                            }
                            globalBoolConstants.Add(key, (BoolVariable)so);
                            globalBoolConstants[key].RuntimeValue = globalBoolConstants[key].InitialValue;
                        }
                        else if (so is IntVariable)
                        {
                            if (globalIntConstants.ContainsKey(key))
                            {
                                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Constant defined: " + key);
                            }
                            globalIntConstants.Add(key, (IntVariable)so);
                            globalIntConstants[key].RuntimeValue = globalIntConstants[key].InitialValue;
                        }
                        else if (so is FloatVariable)
                        {
                            if (globalFloatConstants.ContainsKey(key))
                            {
                                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Constant defined: " + key);
                            }
                            globalFloatConstants.Add(key, (FloatVariable)so);
                            globalFloatConstants[key].RuntimeValue = globalFloatConstants[key].InitialValue;
                        }
                        else if (so is StringVariable)
                        {
                            if (globalStringConstants.ContainsKey(key))
                            {
                                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Constant defined: " + key);
                            }
                            globalStringConstants.Add(key, (StringVariable)so);
                            globalStringConstants[key].RuntimeValue = globalStringConstants[key].InitialValue;
                        }
                        else if (so is StringArrayVariable)
                        {
                            if (globalStringArrayConstants.ContainsKey(key))
                            {
                                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Constant defined: " + key);
                            }
                            globalStringArrayConstants.Add(key, (StringArrayVariable)so);
                            globalStringArrayConstants[key].RuntimeValue = globalStringArrayConstants[key].InitialValue;
                        }
                        else
                        {
                            throw new RPGException(ErrorMessage.INTERNAL_ERROR, "une " + so);
                        }
                    }
                    fileName = dir.GetNext();
                }
            }
            sb.ReturnToPool();

            BoolVariable = new VariableIndexer<string, BoolVariable>(globalBoolConstants);
            IntVariable = new VariableIndexer<string, IntVariable>(globalIntConstants);
            FloatVariable = new VariableIndexer<string, FloatVariable>(globalFloatConstants);
            StringVariable = new VariableIndexer<string, StringVariable>(globalStringConstants);
            StringArrayVariable = new VariableIndexer<string, StringArrayVariable>(globalStringArrayConstants);
            
            EventsWithDisabledFlags = new EventsWithDisabledFlagsIndexer();
            ScriptMessages = new ScriptMessagesIndexer();
        }
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            Instance = this;
            LoadDirectory("variables");
        }
        public void AddResource(string key, Resource res, bool ignoreDuplicates = false)
        {
            if (res is BoolVariable)
            {
                try
                {
                    var s = BoolVariable[key];
                }
                catch (RPGException)
                {
                    // catching an error means the variable isn't there. it can be added safely
                    BoolVariable.Add(key, (BoolVariable)res);
                    BoolVariable[key].RuntimeValue = BoolVariable[key].InitialValue;
                }
                finally
                {
                    if (!ignoreDuplicates)
                    {
                        throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Variable defined: " + key);
                    }
                }
            }
            else if (res is IntVariable)
            {
                try
                {
                    var s = IntVariable[key];
                }
                catch (RPGException)
                {
                    // catching an error means the variable isn't there. it can be added safely
                    IntVariable.Add(key, (IntVariable)res);
                    IntVariable[key].RuntimeValue = IntVariable[key].InitialValue;
                }
                finally
                {
                    if (!ignoreDuplicates)
                    {
                        throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Variable defined: " + key);
                    }
                }
            }
            else if (res is FloatVariable)
            {
                try
                {
                    var s = FloatVariable[key];
                }
                catch (RPGException)
                {
                    // catching an error means the variable isn't there. it can be added safely
                    FloatVariable.Add(key, (FloatVariable)res);
                    FloatVariable[key].RuntimeValue = FloatVariable[key].InitialValue;
                }
                finally
                {
                    if (!ignoreDuplicates)
                    {
                        throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Variable defined: " + key);
                    }
                }
            }
            else if (res is StringVariable)
            {
                try
                {
                    var s = StringVariable[key];
                }
                catch (RPGException)
                {
                    // catching an error means the variable isn't there. it can be added safely
                    StringVariable.Add(key, (StringVariable)res);
                    StringVariable[key].RuntimeValue = StringVariable[key].InitialValue;
                }
                finally
                {
                    if (!ignoreDuplicates)
                    {
                        throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Variable defined: " + key);
                    }
                }
            }
            else if (res is StringArrayVariable)
            {
                try
                {
                    var s = StringArrayVariable[key];
                }
                catch (RPGException)
                {
                    // catching an error means the variable isn't there. it can be added safely
                    StringArrayVariable.Add(key, (StringArrayVariable)res);
                    StringArrayVariable[key].RuntimeValue = StringArrayVariable[key].InitialValue;
                }
                finally
                {
                    if (!ignoreDuplicates)
                    {
                        throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Variable defined: " + key);
                    }
                }
            }
            else
            {
                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "unexpected type " + res);
            }
        }
    }
    public class EventsWithDisabledFlagsIndexer
    {
        private Dictionary<string, int> dictionary;
        public EventsWithDisabledFlagsIndexer()
        {
            dictionary = new Dictionary<string, int>()
            {
                { "COLLIDE_NPC", Globals.DISABLE_COLLIDE_NPC },
                { "CHAT", Globals.DISABLE_CHAT },
                { "HIT", Globals.DISABLE_HIT },
                { "INVENTORY2_OPEN", Globals.DISABLE_INVENTORY2_OPEN },
                { "HEAR", Globals.DISABLE_HEAR },
                { "UNDETECT_PLAYER", Globals.DISABLE_DETECT },
                { "DETECT_PLAYER", Globals.DISABLE_DETECT },
                { "AGGRESSION", Globals.DISABLE_AGGRESSION },
                { "MAIN", Globals.DISABLE_MAIN },
                { "CURSORMODE", Globals.DISABLE_CURSORMODE },
                { "EXPLORATIONMODE", Globals.DISABLE_EXPLORATIONMODE }
            };
        }
        public int this[string eventName]
        {
            get
            {
                if (!dictionary.ContainsKey(eventName))
                {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Invalid Constant " + eventName);
                }
                return dictionary[eventName];
            }
        }
    }
    public class ScriptMessagesIndexer
    {
        private Dictionary<int, string> dictionary;
        public ScriptMessagesIndexer()
        {
            dictionary = new Dictionary<int, string>()
            {
                { Globals.SM_INIT, "INIT" },
                { Globals.SM_INVENTORYIN, "INVENTORY_IN" },
                { Globals.SM_INVENTORYOUT, "INVENTORY_OUT" },
                { Globals.SM_INVENTORYUSE, "INVENTORY_USE" },
                { Globals.SM_EQUIPIN, "EQUIP_IN" },
                { Globals.SM_EQUIPOUT, "EQUIP_OUT" },
                { Globals.SM_MAIN, "MAIN" },
                { Globals.SM_RESET, "RESET" },
                { Globals.SM_CHAT, "CHAT" },
                { Globals.SM_ACTION, "ACTION" },
                { Globals.SM_DEAD, "DEAD" },
                { Globals.SM_HIT, "HIT" },
                { Globals.SM_DIE, "DIE" },
                { Globals.SM_DETECTPLAYER, "DETECT_PLAYER" },
                { Globals.SM_UNDETECTPLAYER, "UNDETECT_PLAYER" },
                { Globals.SM_INITEND, "INIT_END" },
                { Globals.SM_HEAR, "HEAR" },
                { Globals.SM_COLLIDE_NPC, "COLLIDE_NPC" },
                { Globals.SM_AGGRESSION, "AGGRESSION" }
            };
        }
        public string this[int eventId]
        {
            get
            {
                if (!dictionary.ContainsKey(eventId))
                {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Invalid Constant " + eventId);
                }
                return dictionary[eventId];
            }
        }
    }
}
