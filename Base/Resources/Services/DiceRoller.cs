using Base.Exceptions;
using Godot;
using System;

namespace Base.Resources.Services
{
    public class DiceRoller : Node
    {
        /// <summary>
        /// Reference to the singleton instance.
        /// </summary>
        /// <value></value>
        public static DiceRoller Instance { get; private set; }
        public Dice ONE_D2 { get; private set; }
        public Dice ONE_D4 { get; private set; }
        public Dice ONE_D6 { get; private set; }
        public Dice TWO_D6 { get; private set; }
        public Dice THREE_D6 { get; private set; }
        public Dice ONE_D8 { get; private set; }
        public Dice ONE_D10 { get; private set; }
        public Dice ONE_D12 { get; private set; }
        public Dice ONE_D20 { get; private set; }
        public Dice ONE_D100 { get; private set; }
        /// <summary>
        /// the seed value.
        /// </summary>
        private long mySeed;
        /// <summary>
        /// the random number generator.
        /// </summary>
        private Random random;
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            Instance = this;
            ONE_D2 = new Dice(1, 2, this);
            ONE_D4 = new Dice(1, 4, this);
            ONE_D6 = new Dice(1, 6, this);
            TWO_D6 = new Dice(2, 6, this);
            THREE_D6 = new Dice(3, 6, this);
            ONE_D8 = new Dice(1, 8, this);
            ONE_D10 = new Dice(1, 10, this);
            ONE_D12 = new Dice(1, 12, this);
            ONE_D100 = new Dice(1, 100, this);
        }
        public object GetRandomMember(Array array)
        {
            if (array == null)
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Array is null");
            }
            if (array.Length == 0)
            {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Array is empty");
            }
            Check();
            return array.GetValue(Math.Abs(Next() % array.Length));
        }
        private void Check()
        {
            long now = DateTime.Now.Ticks;
            if (mySeed != now)
            {
                mySeed = now;
                random = new System.Random(unchecked((int)mySeed));
            }
        }
        public int Next()
        {
            Check();
            return random.Next();
        }
        public double NextDouble()
        {
            Check();
            return random.NextDouble();
        }
        /// <summary>
        /// Rolls an x-sided die.
        /// </summary>
        /// <param name="faces">the number of faces on the die</param>
        /// <returns></returns>
        public int RollDX(int faces)
        {
            return RollXdY(1, faces);
        }
        /// <summary>
        /// Rolls an x-sided die plus a modifier.
        /// </summary>
        /// <param name="faces">the number of faces on the die</param>
        /// <returns></returns>
        public int RollDXPlusY(int faces, int modifier)
        {
            return RollXdY(1, faces) + modifier;
        }
        /// <summary>
        /// Rolls an x-sided die, y number of times
        /// </summary>
        /// <param name="rolls">the number of rolls to make</param>
        /// <param name="faces">the number of faces on the die</param>
        /// <returns></returns>
        public int RollXdY(int rolls, int faces)
        {
            return new Dice(rolls, faces, this).Roll();
        }
        public float RollPercent()
        {
            return (float)NextDouble();
        }
    }
    public class Dice
    {
        /// <summary>
        /// the number of rolls needed.
        /// </summary>
        private int rolls;
        /// <summary>
        /// the number of faces on the die
        /// </summary>
        private int faces;
        /// <summary>
        /// the parent roller instance.
        /// </summary>
        private DiceRoller parent;
        /// <summary>
        /// Creates a new Dice instance.
        /// </summary>
        /// <param name="rolls">the number of rolls</param>
        /// <param name="faces">the number of faces</param>
        /// <param name="parent">the parent roller instance</param>
        public Dice(int rolls, int faces, DiceRoller parent)
        {
            this.rolls = rolls;
            this.faces = faces;
            this.parent = parent;
        }
        /// <summary>
        /// Rolls the dice.
        /// </summary>
        /// <returns></returns>
        public int Roll()
        {
            int result = 0;
            for (int i = rolls - 1; i >= 0; i--)
            {
                result += Math.Abs(parent.Next() % faces) + 1;
            }
            return result;
        }
    }
}
