using System.Collections.Generic;
using Godot;

namespace Base.Resources.Bus
{
    /// <summary>
    /// A Table resource that allows the storage of custom resource elements that can be retrieved through a die roll. For instance, a table that stores birth aspects would contain Birth Aspect Resources index by Vector2 elements defining the die roll range needed to qualify for the Birth Aspect.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="R"></typeparam>
    public class ResourcedLookupTableResource<K, R> : Resource 
    where K : Resource 
    where R : Resource
    {
        /// <summary>
        /// The Indexed Table's elements.
        /// </summary>
        /// <value></value>
        [Export]
        public Dictionary<K, R> Elements { get; set; }
        /// <summary>
        /// Gets a specific element from the table.
        /// </summary>
        /// <returns></returns>
        public R GetLookupElement(K lookup) { return Elements[lookup]; }
    }
}
