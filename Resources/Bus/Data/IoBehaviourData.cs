using Base.Bus;
using Base.Exceptions;
using Bus.Services;

namespace Base.Resources.Data
{
    public class IoBehaviourData
    {
        /// <summary>
        /// the parameter applied to a behavior.
        /// </summary>
        /// <value></value>
        public int BehaviourParam { get; set; }
        /// <summary>
        /// the parameter applied to a behavior.
        /// </summary>
        /// <returns></returns>
        public FlagSet Behaviour { get; private set; } = new FlagSet();
        /// <summary>
        /// flag indicating whether the behavior exists.
        /// </summary>
        public bool Exists { get; set; }
        private int movementMode = Globals.WALKMODE;
        /// <summary>
        /// the parameter applied to a behavior.
        /// </summary>
        /// <value></value>
        public int MovementMode
        {
            get
            {
                return this.movementMode;
            }
            set
            {
                if (value < Globals.WALKMODE || value > Globals.SNEAKMODE) {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "IoBehaviourData.MovementMode - '" + value + "' is not recognized");
                }
                this.movementMode = value;
            }
        }
        /// <summary>
        /// tactics for the behavior; e.g., 0=none, 1=side, 2=side + back, etc...
        /// </summary>
        /// <value></value>
        public int Tactics { get; set; }
        /// <summary>
        /// the behavior's target.
        /// </summary>
        private int target = -1;
        /// <summary>
        /// the parameter applied to a behavior.
        /// </summary>
        /// <value></value>
        public int Target
        {
            get
            {
                return this.target;
            }
            set
            {
                if (value < 0) {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "IoBehaviourData.Target - '" + value + "' is not valid");
                }
                this.target = value;
            }
        }
        /// <summary>
        /// Determines whether the behaviour matches a specific behaviour flag.
        /// </summary>
        /// <param name="behaviour">the behaviour flag</param>
        /// <returns>true if the behaviour matches the flag; false otherwise</returns>
        public bool Is(int behaviour) { return this.Behaviour.Has(behaviour); }
    }
}
