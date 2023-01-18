using System.Collections.Generic;
using Base.Resources.Bus;
using Godot;

namespace Base.Resources.Bus
{
    public class BiLevelIndexedTableResource<K, R> : Resource where R : Resource
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
        public Dictionary<K, Dictionary<Vector2, R>> Elements { get; set; }
        /// <summary>
        /// Gets a random element from the table.
        /// </summary>
        /// <returns></returns>
        public R GetRandomElement(K k)
        {
            R val = null;
            int roll = DieRoll.Roll();
            foreach (KeyValuePair<Vector2, R> entry in Elements[k])
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
