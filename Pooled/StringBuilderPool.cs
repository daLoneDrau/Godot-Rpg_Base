using System;
using System.Collections.Generic;

namespace Base.Pooled
{
    /// <summary>
    /// Pooled string builder assets list.
    /// </summary>
    public sealed class StringBuilderPool
    {
        /// <summary>
        /// the singleton instance.
        /// </summary>
        private static StringBuilderPool instance;
        public static StringBuilderPool Instance
        {
            get
            {
                const int initialLength = 5;
                if (instance == null)
                {
                    instance = new StringBuilderPool(initialLength);
                }
                return instance;
            }
        }
        /// <summary>
        /// the flags for each pool item indicating whether it is locked or not.
        /// </summary>
        private readonly List<bool> locked;
        /// <summary>
        /// the pool of {@link PooledStringBuilder}s.
        /// </summary>
        private readonly List<PooledStringBuilder> pool;
        /// <summary>
        /// Creates the pool of {@link PooledStringBuilder}s.
        /// <paramref name="initialCapacity"/> the initial pool capacity
        /// </summary>
        private StringBuilderPool(int initialCapacity)
        {
            // create an initial list
            pool = new List<PooledStringBuilder>(initialCapacity);
            locked = new List<bool>(initialCapacity);
            // populate the list and set all items to unlocked
            for (int i = 0; i < initialCapacity; i++)
            {
                pool.Add(new PooledStringBuilder(i));
                locked.Add(false);
            }
        }
        /// <summary>
        /// Retrieves a <see cref="PooledStringBuilder"/> from the pool and locks it for use.
        /// </summary>
        public PooledStringBuilder GetStringBuilder()
        {
            int freeIndex = 0, c = locked.Count;
            for (; freeIndex < c; freeIndex++)
            {
                // current index is free
                if (!locked[freeIndex])
                {
                    // lock this item
                    locked[freeIndex] = true;
                    break;
                }
            }
            if (freeIndex >= c)
            {
                // we got here because all items are in use
                // create a new item and add it to the list
                pool.Add(new PooledStringBuilder(freeIndex));
                // lock the item
                locked.Add(true);
            }
            // return the item at the free index
            return pool[freeIndex];
        }
        /// <summary>
        /// Determines if an item is locked.
        /// <paramref name="item"/> the <see cref="PooledStringBuilder"/> instance
        /// <returns><c>true</c> if the asset is locked, false if it is free and ready for use</returns>
        /// </summary>
        public bool IsItemLocked(PooledStringBuilder item)
        {
            return locked[item.GetPoolIndex()];
        }
        /// <summary>
        /// Returns an item to the pool.
        /// <paramref name="item"/> the <see cref="PooledStringBuilder"/> being returned
        /// </summary>
        public void ReturnObject(PooledStringBuilder item)
        {
            // remove the lock
            locked[item.GetPoolIndex()] = false;
            // tell the item it's been returned to the pool
            item.ReturnToPool();
        }
        /// <summary>
        /// Unlocks the assets, readying it for use again.
        /// <paramref name="item"/> the <see cref="PooledStringBuilder"/> asset
        /// </summary>
        public void UnlockItem(PooledStringBuilder item)
        {
            locked[item.GetPoolIndex()] = false;
        }
    }
}