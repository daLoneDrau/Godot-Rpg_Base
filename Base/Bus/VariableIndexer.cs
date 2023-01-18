using System.Collections.Generic;
using Base.Exceptions;
using Base.Resources.Services;

/// <summary>
/// 
/// </summary>
namespace Base.Bus
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class VariableIndexer<K, V>
    {
        /// <summary>
        /// the backing dictionary.
        /// </summary>
        private Dictionary<K, V> dictionary;
        /// <summary>
        /// Creates a new VariableIndexer instance from the supplied dictionary.
        /// </summary>
        /// <param name="baseMap">the base dictionary</param>
        public VariableIndexer(Dictionary<K, V> baseMap)
        {
            dictionary = baseMap;
        }
        /// <summary>
        /// Gets the value from the dictionary based on the supplied key.
        /// </summary>
        /// <value></value>
        public V this[K id]
        {
            get
            {
                if (!dictionary.ContainsKey(id))
                {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "Invalid Constant " + id);
                }
                return dictionary[id];
            }
        }
        /// <summary>
        /// Gets all entries in the indexer.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <returns></returns>
        public List<K> Keys { get { return new List<K>(dictionary.Keys); } }
        /// <summary>
        /// Gets a random entry.
        /// </summary>
        /// <returns></returns>
        public V GetRandomEntry()
        {
            List<K> keys = this.Keys;
            int index = DiceRoller.Instance.RollDXPlusY(keys.Count, -1);
            return dictionary[keys[index]];
        }
    }
}