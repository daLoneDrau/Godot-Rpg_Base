using Godot;
using System;

namespace Bus.Services
{
    public static class Globals
    {
        #region GFLAGS
        public const int GFLAG_DOOR = 1 << 5;
        public const int GFLAG_ELEVATOR = 1 << 10;
        public const int GFLAG_GOREEXPLODE = 1 << 14;
        public const int GFLAG_HIDEWEAPON = 1 << 12;
        public const int GFLAG_INTERACTIVITY = 1;
        public const int GFLAG_INTERACTIVITYHIDE = 1 << 4;
        public const int GFLAG_INVISIBILITY = 1 << 6;
        public const int GFLAG_ISINTREATZONE = 1 << 1;
        public const int GFLAG_MEGAHIDE = 1 << 11;
        public const int GFLAG_NEEDINIT = 1 << 3;
        public const int GFLAG_NO_PHYS_IO_COL = 1 << 7;
        public const int GFLAG_NOCOMPUTATION = 1 << 15;
        public const int GFLAG_NOGORE = 1 << 13;
        public const int GFLAG_PLATFORM = 1 << 9;
        public const int GFLAG_VIEW_BLOCKER = 1 << 8;
        public const int GFLAG_WASINTREATZONE = 1 << 2;
        #endregion

        #region IO FLAGS
        /// <summary>
        /// flag indicating the interactive object is a IOPcData.
        /// </summary>
        public const int IO_PC = 1;
        /// <summary>
        /// flag indicating the interactive object is an item.
        /// </summary>
        public const int IO_ITEM = 2;
        /// <summary>
        /// flag indicating the interactive object is an IONpcData.
        /// </summary>
        public const int IO_NPC = 4;
        /** flag indicating the interactive object is a horse object. */
        public const int IO_HORSE = 8;
        /** flag indicating the interactive object is under water. */
        public const int IO_UNDERWATER = 16;
        /** flag indicating the interactive object is a fixable object. */
        public const int IO_FREEZESCRIPT = 32;
        /** flag indicating the interactive object is a fixable object. */
        public const int IO_NO_COLLISIONS = 64;
        /** flag indicating the interactive object is a fixable object. */
        public const int IO_INVULNERABILITY = 128;
        /** flag indicating the interactive object is a dwelling. */
        public const int IO_DWELLING = 256;
        /** flag indicating the interactive object is gold. */
        public const int IO_GOLD = 512;
        /** flag indicating the interactive object has interactivity. */
        public const int IO_INTERACTIVITY = 1024;
        /** flag indicating the interactive object is a treasure. */
        public const int IO_TREASURE = 2048;
        /** flag indicating the interactive object is unique. */
        public const int IO_UNIQUE = 4096;
        /** flag indicating the interactive object is a party. */
        public const int IO_PARTY = 8192;
        /** flag indicating the interactive object is a winged mount. */
        public const int IO_MOVABLE = 16384;
        /** flag indicating the interactive object is a trigger. */
        public const int IO_TRIGGER = 1 << 16;
        /** flag indicating the interactive object is fixable. */
        public const int IO_FIX = 1 << 17;
        #endregion
        
        #region NPC FLAGS
        /// <summary>
        /// flag indicating that an NPC can be backstabbed
        /// </summary>
        public const int NPCFLAG_BACKSTAB = 1;
        #endregion
        
        #region PLAYER MOVEMENT FLAGS
        public const int PLAYER_CROUCH = 1 << 7;
        public const int PLAYER_LEAN_LEFT = 1 << 8;
        public const int PLAYER_LEAN_RIGHT = 1 << 9;
        public const int PLAYER_MOVE_JUMP = 1 << 4;
        public const int PLAYER_MOVE_STEALTH = 1 << 5;
        public const int PLAYER_MOVE_STRAFE_LEFT = 1 << 2;
        public const int PLAYER_MOVE_STRAFE_RIGHT = 1 << 3;
        public const int PLAYER_MOVE_WALK_BACKWARD = 1 << 1;
        public const int PLAYER_MOVE_WALK_FORWARD = 1;
        public const int PLAYER_ROTATE = 1 << 6;
        #endregion
        
        #region PLAYER FLAGS
        public const int PLAYERFLAGS_INVULNERABILITY = 2;
        public const int PLAYERFLAGS_NO_MANA_DRAIN = 1;
        #endregion
        
        #region SHOW FLAGS
        public const int SHOW_FLAG_DESTROYED = 255;
        public const int SHOW_FLAG_HIDDEN = 5;       // In =
                                                      // Inventory;
        public const int SHOW_FLAG_IN_INVENTORY = 4;     // In =
                                                          // Inventory;
        public const int SHOW_FLAG_IN_SCENE = 1;
        public const int SHOW_FLAG_KILLED = 7;       // Not Used
                                                      // = Yet;
        public const int SHOW_FLAG_LINKED = 2;
        public const int SHOW_FLAG_MEGAHIDE = 8;
        public const int SHOW_FLAG_NOT_DRAWN = 0;
        public const int SHOW_FLAG_ON_PLAYER = 9;
        public const int SHOW_FLAG_TELEPORTING = 6;
        #endregion
        
        #region TARGET SETTINGS
        public const int TARGET_PATH = -3;
        public const int TARGET_NONE = -2;
        public const int TARGET_PLAYER = 0;
        #endregion
        
        #region MOVEMENT MODES
        public const int WALKMODE = 0;
        public const int RUNMODE = 1;
        public const int NOMOVEMODE = 2;
        public const int SNEAKMODE = 3;
        #endregion
        
        #region MISCELLANEOUS
        public const int NO_IDENT = 1;
        public const int NO_MESH = 2;
        public const int NO_ON_LOAD = 4;
        public const int IO_IMMEDIATELOAD = 8;
        #endregion
        
        #region DAMAGE TYPE FLAGS
        public const int DAMAGE_TYPE_ACID = 1 << 9;
        public const int DAMAGE_TYPE_COLD = 1 << 3;
        public const int DAMAGE_TYPE_DRAIN_LIFE = 1 << 12;
        public const int DAMAGE_TYPE_DRAIN_MANA = 1 << 13;
        public const int DAMAGE_TYPE_FAKEFIRE = 1 << 15;
        public const int DAMAGE_TYPE_FIELD = 1 << 16;
        public const int DAMAGE_TYPE_FIRE = 1;
        public const int DAMAGE_TYPE_GAS = 1 << 5;
        public const int DAMAGE_TYPE_GENERIC = 0;
        public const int DAMAGE_TYPE_LIGHTNING = 1 << 2;
        public const int DAMAGE_TYPE_MAGICAL = 1 << 1;
        public const int DAMAGE_TYPE_METAL = 1 << 6;
        public const int DAMAGE_TYPE_NO_FIX = 1 << 17;
        public const int DAMAGE_TYPE_ORGANIC = 1 << 10;
        public const int DAMAGE_TYPE_PER_SECOND = 1 << 11;
        public const int DAMAGE_TYPE_POISON = 1 << 4;
        public const int DAMAGE_TYPE_PUSH = 1 << 14;
        public const int DAMAGE_TYPE_STONE = 1 << 8;
        public const int DAMAGE_TYPE_WOOD = 1 << 7;
        #endregion
        
        #region EQUIPMENT SLOTS
        public const int EQUIP_SLOT_RING_LEFT = 0;
        public const int EQUIP_SLOT_RING_RIGHT = 1;
        public const int EQUIP_SLOT_WEAPON = 2;
        public const int EQUIP_SLOT_SHIELD = 3;
        public const int EQUIP_SLOT_TORCH = 4;
        public const int EQUIP_SLOT_TORSO = 5;
        public const int EQUIP_SLOT_HELMET = 6;
        public const int EQUIP_SLOT_LEGGINGS = 7;
        #endregion
        
        #region OBJECT TYPE FLAGS
        public const int OBJECT_TYPE_WEAPON = 1;
        /// <summary>
        /// Dagger type.
        /// </summary>
        public const int OBJECT_TYPE_DAGGER = 1 << 1;
        public const int OBJECT_TYPE_1H = 1 << 2;
        public const int OBJECT_TYPE_2H = 1 << 3;
        public const int OBJECT_TYPE_BOW = 1 << 4;
        public const int OBJECT_TYPE_SHIELD = 1 << 5;
        public const int OBJECT_TYPE_FOOD = 1 << 6;
        public const int OBJECT_TYPE_GOLD = 1 << 7;
        public const int OBJECT_TYPE_ARMOR = 1 << 8;
        public const int OBJECT_TYPE_HELMET = 1 << 9;
        public const int OBJECT_TYPE_RING = 1 << 10;
        public const int OBJECT_TYPE_LEGGINGS = 1 << 11;
        #endregion
        
        #region WEAPON TYPES
        public const int WEAPON_1H = 2;
        public const int WEAPON_2H = 3;
        public const int WEAPON_BARE = 0;
        public const int WEAPON_BOW = 4;
        public const int WEAPON_DAGGER = 1;
        #endregion
        
        #region MATH GLOBALS
        public const float DEUXTIERS = 0.6666666666666666666667f;
        public const float DIV10 = 0.1f;
        public const float DIV100 = 0.01f;
        public const float DIV1000 = 0.001f;
        public const float DIV10000 = 0.0001f;
        public const float DIV100000 = 0.00001f;
        public const float DIV1024 = 0.0009765625f;
        public const float DIV11 = 0.0909090909090909090909f;
        public const float DIV110 = 0.0090909090909090909090f;
        public const float DIV12 = 0.0833333333333333333333f;
        public const float DIV120 = 0.0083333333333333333333f;
        public const float DIV12000 = 0.0000833333333333333333f;
        public const float DIV128 = 0.0078125f;
        public const float DIV13 = 0.0769230769230769230769f;
        public const float DIV130 = 0.0076923076923076923076f;
        public const float DIV13000 = 0.0000769230769230769231f;
        public const float DIV14 = 0.0714285714285714285714f;
        public const float DIV140 = 0.0071428571428571428571f;
        public const float DIV15 = 0.0666666666666666666667f;
        public const float DIV150 = 0.0066666666666666666666f;
        public const float DIV15000 = 0.0000666666666666666666f;
        public const float DIV16 = 0.0625f;
        public const float DIV160 = 0.00625f;
        public const float DIV16000 = 0.0000625f;
        public const float DIV17 = 0.0588235294117647058823f;
        public const float DIV170 = 0.0058823529411764705882f;
        public const float DIV18 = 0.0555555555555555555555f;
        public const float DIV180 = 0.0055555555555555555555f;
        public const float DIV18000 = 0.0000555555555555555555f;
        public const float DIV19 = 0.0526315789473684210526f;
        public const float DIV190 = 0.0052631578947368421052f;
        public const float DIV2 = 0.5f;
        public const float DIV20 = 0.05f;
        public const float DIV200 = 0.005f;
        public const float DIV2000 = 0.0005f;
        public const float DIV20000 = 0.00005f;
        public const float DIV2048 = 0.00048828125f;
        public const float DIV21 = 0.0476190476190476190476f;
        public const float DIV22 = 0.0454545454545454545454f;
        public const float DIV23 = 0.0434782608695652173913f;
        public const float DIV24 = 0.0416666666666666666667f;
        public const float DIV25 = 0.04f;
        public const float DIV255 = 0.00392156862745098f;
        public const float DIV256 = 0.00390625f;
        public const float DIV26 = 0.0384615384615384615384f;
        public const float DIV27 = 0.0370370370370370370370f;
        public const float DIV28 = 0.0357142857142857142857f;
        public const float DIV29 = 0.0344827586206896551724f;
        public const float DIV3 = 0.3333333333333333333333f;
        public const float DIV30 = 0.0333333333333333333333f;
        public const float DIV300 = 0.0033333333333333333333f;
        public const float DIV3000 = 0.00033333333f;
        public const float DIV32 = 0.03125f;
        public const float DIV32768 = 0.000030517578125f;
        public const float DIV384 = 0.0026041666666666666666f;
        public const float DIV4 = 0.25f;
        public const float DIV40 = 0.025f;
        public const float DIV400 = 0.0025f;
        public const float DIV4000 = 0.00025f;
        public const float DIV4096 = 0.000244140625f;
        public const float DIV480 = 0.0020833333333333333333f;
        public const float DIV5 = 0.2f;
        public const float DIV50 = 0.02f;
        public const float DIV500 = 0.002f;
        public const float DIV5000 = 0.0002f;
        public const float DIV512 = 0.001953125f;
        public const float DIV6 = 0.1666666666666666666666f;
        public const float DIV60 = 0.0166666666666666666666f;
        public const float DIV600 = 0.0016666666666666666666f;
        public const float DIV64 = 0.015625f;
        public const float DIV640 = 0.0015625f;
        public const float DIV7 = 0.1428571428571428571428f;
        public const float DIV70 = 0.0142857142857142857142f;
        public const float DIV700 = 0.0014285714285714285714f;
        public const float DIV8 = 0.125f;
        public const float DIV80 = 0.0125f;
        public const float DIV800 = 0.00125f;
        public const float DIV9 = 0.1111111111111111111111f;
        public const float DIV90 = 0.0111111111111111111111f;
        public const float DIV900 = 0.0011111111111111111111f;
        public const float DIVPI = 0.318309886183790671537767526745029f;
        #endregion
        
        #region PATHFINDING
        public const int PATHFIND_ALWAYS = 1;
        public const int PATHFIND_ONCE = 2;
        public const int PATHFIND_NO_UPDATE = 4;
        #endregion
        
        #region DISABLE FLAGS
        public const int DISABLE_AGGRESSION = 32;
        public const int DISABLE_CHAT = 2;
        public const int DISABLE_COLLIDE_NPC = 128;
        public const int DISABLE_CURSORMODE = 256;
        public const int DISABLE_DETECT = 16;
        public const int DISABLE_EXPLORATIONMODE = 512;
        public const int DISABLE_HEAR = 8;
        public const int DISABLE_HIT = 1;
        public const int DISABLE_INVENTORY2_OPEN = 4;
        public const int DISABLE_MAIN = 64;
        #endregion
        
        #region SCRIPT MESSAGES
        public const int REFUSE = -1;
        public const int SM_NULL = 0;
        public const int SM_INIT = 1;
        public const int SM_INVENTORYIN = 2;
        public const int SM_INVENTORYOUT = 3;
        public const int SM_INVENTORYUSE = 4;
        public const int SM_SCENEUSE = 5;
        public const int SM_EQUIPIN = 6;
        public const int SM_EQUIPOUT = 7;
        public const int SM_MAIN = 8;
        public const int SM_RESET = 9;
        public const int SM_CHAT = 10;
        public const int SM_ACTION = 11;
        public const int SM_DEAD = 12;
        public const int SM_REACHEDTARGET = 13;
        public const int SM_FIGHT = 14;
        public const int SM_FLEE = 15;
        /** script message - the BaseInteractiveObject has been hit. */
        public const int SM_HIT = 16;
        public const int SM_DIE = 17;
        public const int SM_LOSTTARGET = 18;
        public const int SM_TREATIN = 19;
        public const int SM_TREATOUT = 20;
        /** script message to move to a travel location. */
        public const int SM_MOVE = 21;
        public const int SM_DETECTPLAYER = 22;
        public const int SM_UNDETECTPLAYER = 23;
        public const int SM_COMBINE = 24;
        public const int SM_NPC_FOLLOW = 25;
        public const int SM_EXECUTELINE = 255;
        public const int SM_DUMMY = 256;
        public const int SM_NPC_FIGHT = 26;
        public const int SM_NPC_STAY = 27;
        public const int SM_INVENTORY2_OPEN = 28;
        public const int SM_INVENTORY2_CLOSE = 29;
        public const int SM_CUSTOM = 30;
        public const int SM_ENTER_ZONE = 31;
        public const int SM_LEAVE_ZONE = 32;
        public const int SM_INITEND = 33;
        public const int SM_CLICKED = 34;
        public const int SM_INSIDEZONE = 35;
        public const int SM_CONTROLLEDZONE_INSIDE = 36;
        public const int SM_LEAVEZONE = 37;
        public const int SM_CONTROLLEDZONE_LEAVE = 38;
        public const int SM_ENTERZONE = 39;
        public const int SM_CONTROLLEDZONE_ENTER = 40;
        public const int SM_LOAD = 41;
        public const int SM_SPELLCAST = 42;
        public const int SM_RELOAD = 43;
        public const int SM_COLLIDE_DOOR = 44;
        public const int SM_OUCH = 45;
        public const int SM_HEAR = 46;
        public const int SM_SUMMONED = 47;
        public const int SM_SPELLEND = 48;
        public const int SM_SPELLDECISION = 49;
        public const int SM_STRIKE = 50;
        public const int SM_COLLISION_ERROR = 51;
        public const int SM_WAYPOINT = 52;
        public const int SM_PATHEND = 53;
        public const int SM_CRITICAL = 54;
        public const int SM_COLLIDE_NPC = 55;
        public const int SM_BACKSTAB = 56;
        public const int SM_AGGRESSION = 57;
        public const int SM_COLLISION_ERROR_DETAIL = 58;
        public const int SM_GAME_READY = 59;
        public const int SM_CINE_END = 60;
        public const int SM_KEY_PRESSED = 61;
        public const int SM_CONTROLS_ON = 62;
        public const int SM_CONTROLS_OFF = 63;
        public const int SM_PATHFINDER_FAILURE = 64;
        public const int SM_PATHFINDER_SUCCESS = 65;
        public const int SM_TRAP_DISARMED = 66;
        public const int SM_BOOK_OPEN = 67;
        public const int SM_BOOK_CLOSE = 68;
        public const int SM_IDENTIFY = 69;
        public const int SM_BREAK = 70;
        public const int SM_STEAL = 71;
        public const int SM_COLLIDE_FIELD = 72;
        public const int SM_CURSORMODE = 73;
        public const int SM_EXPLORATIONMODE = 74;
        public const int SM_MAXCMD = 75;
        public const int ACCEPT = 1;
        #endregion
        
        #region SCRIPT VARIABLE TYPES
        /** flag indicating the script variable is a global string. */
        public const int TYPE_G_00_TEXT = 0;
        /** flag indicating the script variable is a global string. */
        public const int TYPE_G_01_TEXT_ARR = 1;
        /** flag indicating the script variable is a global floating-point type. */
        public const int TYPE_G_02_FLOAT = 2;
        /** flag indicating the script variable is a global floating-point array. */
        public const int TYPE_G_03_FLOAT_ARR = 3;
        /** flag indicating the script variable is a global integer. */
        public const int TYPE_G_04_INT = 4;
        /** flag indicating the script variable is a global integer array. */
        public const int TYPE_G_05_INT_ARR = 5;
        /** flag indicating the script variable is a global integer. */
        public const int TYPE_G_06_LONG = 6;
        /** flag indicating the script variable is a global long array. */
        public const int TYPE_G_07_LONG_ARR = 7;
        /** flag indicating the script variable is a local string. */
        public const int TYPE_L_08_TEXT = 8;
        public const int TYPE_TEXT = 8;
        /** flag indicating the script variable is a local string array. */
        public const int TYPE_L_09_TEXT_ARR = 9;
        public const int TYPE_TEXT_ARR = 9;
        /** flag indicating the script variable is a local floating-point type. */
        public const int TYPE_L_10_FLOAT = 10;
        public const int TYPE_FLOAT = 10;
        /** flag indicating the script variable is a local floating-point array. */
        public const int TYPE_L_11_FLOAT_ARR = 11;
        public const int TYPE_FLOAT_ARR = 11;
        /** flag indicating the script variable is a local integer. */
        public const int TYPE_L_12_INT = 12;
        public const int TYPE_INT = 12;
        /** flag indicating the script variable is a local integer array. */
        public const int TYPE_L_13_INT_ARR = 13;
        public const int TYPE_INT_ARR = 13;
        /** flag indicating the script variable is a local integer. */
        public const int TYPE_L_14_LONG = 14;
        public const int TYPE_LONG = 14;
        /** flag indicating the script variable is a local long array. */
        public const int TYPE_L_15_LONG_ARR = 15;
        public const int TYPE_LONG_ARR = 15;
        public const int TYPE_BOOL = 16;
        public const int TYPE_BOOL_ARR = 17;
        #endregion

        #region BEHAVIOUR FLAGS
        public const int BEHAVIOUR_NONE = 1;		// no pathfind
        public const int BEHAVIOUR_FRIENDLY = (1<<1);		// no pathfind
        public const int BEHAVIOUR_MOVE_TO = (1<<2);	
        public const int BEHAVIOUR_WANDER_AROUND = (1<<3);	//behavior_param = distance
        public const int BEHAVIOUR_FLEE = (1<<4);	//behavior_param = distance
        public const int BEHAVIOUR_HIDE = (1<<5);	//behavior_param = distance
        public const int BEHAVIOUR_LOOK_FOR = (1<<6);	//behavior_param = distance
        public const int BEHAVIOUR_SNEAK = (1<<7);
        public const int BEHAVIOUR_FIGHT = (1<<8);
        public const int BEHAVIOUR_DISTANT = (1<<9);
        public const int BEHAVIOUR_MAGIC = (1<<10);
        public const int BEHAVIOUR_GUARD = (1<<11);
        public const int BEHAVIOUR_GO_HOME = (1<<12);
        public const int BEHAVIOUR_LOOK_AROUND = (1<<13);
        public const int BEHAVIOUR_STARE_AT = (1<<14);
        #endregion
        
        #region PC EVENT SIGNALS
        public const int PLAYER_EVENT_UPDATE_ATTRIBUTES = 0;
        #endregion
        public const int MAX_SPELLS = 0;
        public const int MAX_EVENT_STACK = 800;
        public const int MAX_SCRIPTTIMERS = 5;
    }
}
