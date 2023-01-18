using System;

namespace Base.Pooled
{
    public sealed class PooledException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="PooledException"/>.
        /// </summary>
        public PooledException()
        {
        }
        /// <summary>
        /// Creates a new instance of <see cref="PooledException"/>.
        /// <paramref name="message"/>the detail message. The detail message is saved for later retrieval by the <see cref="Exception.Message"/> method.
        /// </summary>
        public PooledException(string message) : base(message)
        {
        }
        /// <summary>
        /// Creates a new instance of <see cref="PooledException"/>.
        /// <paramref name="message"/>
        /// <paramref name="innerException"/>
        /// </summary>
        public PooledException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}