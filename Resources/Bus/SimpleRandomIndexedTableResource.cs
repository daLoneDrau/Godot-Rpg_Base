using System;
using System.Collections.Generic;
using Godot;

namespace Base.Resources.Bus
{
    /// <summary>
    /// A Table resource that allows the storage of custom resource elements that can be retrieved through a die roll. For instance, a table that stores birth aspects would contain Birth Aspect Resources index by Vector2 elements defining the die roll range needed to qualify for the Birth Aspect.
    /// </summary>
    /// <typeparam name="R"></typeparam>
    public class SimpleRandomIndexedTableResource<R> : Resource
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
        public Dictionary<string, R> Elements { get; set; }
        /// <summary>
        /// Gets a random element from the table.
        /// </summary>
        /// <returns></returns>
        public R GetRandomElement()
        {
            R val = default;
            int roll = DieRoll.Roll();
            foreach (KeyValuePair<string, R> entry in Elements)
            {
                int key = Int32.Parse(entry.Key);
                if (key == roll)
                {
                    val = entry.Value;
                    break;
                }
            }
            return val;
        }
    }
}
