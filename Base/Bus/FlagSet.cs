namespace Base.Bus
{
    /// <summary>
    /// Implements a set of flags that can be assigned or removed.
    /// </summary>
    public sealed class FlagSet {
        private int flags = 0;
        /// <summary>
        /// Adds a flag to the set.
        /// </summary>
        /// <param name="flag">the flag</param>
        public void Add(int flag) {
            this.flags |= flag;
        }
        /// <summary>
        /// Clears all flags.
        /// </summary>
        public void Clear() {
            this.flags = 0;
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            return this.flags == ((FlagSet)obj).flags;
        }        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return base.GetHashCode();
        }
        /// <summary>
        /// Determines if a specific flag was added to the set.
        /// </summary>
        /// <param name="flag">the flag</param>
        /// <returns>true if the flag was set; false otherwise</returns>
        public bool Has(int flag) {
            return (this.flags & flag) == flag;
        }
        /// <summary>
        /// Removes a flag from the set.
        /// </summary>
        /// <param name="flag">the flag</param>
        public void Remove(int flag) {
            this.flags &= ~flag;
        }
    }
}