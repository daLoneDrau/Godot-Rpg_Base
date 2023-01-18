using System.Collections.Generic;
using Godot;

namespace Base.Resources.Bus
{
    /// <summary>
    /// A Table resource that allows the storage of custom resource elements that can be retrieved through a die roll. For instance, a table that stores birth aspects would contain Birth Aspect Resources index by Vector2 elements defining the die roll range needed to qualify for the Birth Aspect.
    /// </summary>
    /// <typeparam name="R"></typeparam>
    public class IndexedTableResource<R> : Resource where R : Resource
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
        public Dictionary<Vector2, R> Elements { get; set; }
        /// <summary>
        /// Gets a specific element from the table.
        /// </summary>
        /// <returns></returns>
        public R GetLookupElement(int lookup)
        {
            R val = null;
            foreach (KeyValuePair<Vector2, R> entry in Elements)
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
        public R GetRandomElement()
        {
            R val = null;
            int roll = DieRoll.Roll();
            foreach (KeyValuePair<Vector2, R> entry in Elements)
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
