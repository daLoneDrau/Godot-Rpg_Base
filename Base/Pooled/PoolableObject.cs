namespace Base.Pooled
{
    interface PoolableObject
    {
        /// <summary>
        ///  Initializes the object.
        /// </summary>
        void Init();
        /// <summary>
        ///  Returns the object to the pool.
        /// </summary>
        void ReturnToPool();
    }
}