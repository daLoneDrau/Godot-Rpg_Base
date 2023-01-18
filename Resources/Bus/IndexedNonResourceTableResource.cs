using System.Collections.Generic;
using Godot;

namespace Base.Resources.Bus
{
    /// <summary>
    /// A Table resource that allows the storage of stringss that can be retrieved through a die roll.
    /// </summary>
    public class IndexedNonResourceTableResource<T> : Resource
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
        public Dictionary<Vector2, T> Elements { get; set; }
        /// <summary>
        /// Gets a specific element from the table.
        /// </summary>
        /// <returns></returns>
        public T GetLookupElement(int lookup)
        {
            T val = default(T);
            foreach (KeyValuePair<Vector2, T> entry in Elements)
            {
                Vector2 range = entry.Key;
                if (range[0] <= lookup && lookup <= range[1])
                {
                    val = entry.Value;
                    break;
                }
            }
            return val;
        }
        /// <summary>
        /// Gets a random element from the table.
        /// </summary>
        /// <returns></returns>
        public T GetRandomElement()
        {
            T val = default(T);
            int roll = DieRoll.Roll();
            foreach (KeyValuePair<Vector2, T> entry in Elements)
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
