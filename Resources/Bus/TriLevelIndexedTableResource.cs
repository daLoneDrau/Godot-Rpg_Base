using System.Collections.Generic;
using Base.Resources.Bus;
using Godot;

namespace Base.Resources.Bus
{
    public class TriLevelIndexedTableResource<K0, K1, R> : Resource where R : Resource
    {
        /// <summary>
        /// The die roll used with this Indexed Table.
        /// </summary>
        /// <value></value>
        [Export]
        public Die DieRoll { get; set; }
        /// <summary>
        /// The Indexed Table's elements.
        /// </summary>
        /// <value></value>
        [Export]
        public Dictionary<K0, Dictionary<K1, Dictionary<Vector2, R>>> Elements { get; set; }
        /// <summary>
        /// Gets a random element from the table.
        /// </summary>
        /// <returns></returns>
        public R GetRandomElement(K0 k0, K1 k1, int rollModifer = 0)
        {
            R val = null;
            int roll = DieRoll.Roll() + rollModifer;
            if (roll < 1)
            {
                roll = 1;
            }
            foreach (KeyValuePair<Vector2, R> entry in Elements[k0][k1])
            {
                Vector2 range = entry.Key;
                if (range[0] <= roll && roll <= range[1])
                {
                    val = entry.Value;
                    break;
                }
            }
            return val;
        }
    }
}
