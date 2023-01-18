using System.Collections.Generic;
using Base.Resources.Bus;
using Godot;

namespace Base.Resources.Bus
{
    public class TriLevelIndexedDictionaryTableResource<K0, K1> : Resource
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
        public Dictionary<K0, Dictionary<K1, Dictionary<string, Dictionary<string, object>>>> Elements { get; set; }
        /// <summary>
        /// Gets a random element from the table.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetRandomElement(K0 k0, K1 k1)
        {
            Dictionary<string, object> val = new Dictionary<string, object>();
            int roll = DieRoll.Roll();
            foreach (KeyValuePair<string, Dictionary<string, object>> entry in Elements[k0][k1])
            {
                Vector2 range = (Vector2)entry.Value["range"];
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
