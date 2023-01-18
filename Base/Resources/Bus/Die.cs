using Base.Resources.Services;
using Godot;

namespace Base.Resources.Bus
{
    public class Die : Resource
    {
        /// <summary>
        /// the number of rolls needed.
        /// </summary>
        [Export]
        public int Rolls { get; set; }
        /// <summary>
        /// the number of faces on the die
        /// </summary>
        [Export]
        public int Faces { get; set; }
        public int Roll()
        {
            return DiceRoller.Instance.RollXdY(Rolls, Faces);
        }
    }
}