using System;
using System.Text;

namespace Base.Pooled
{
    public sealed class PooledStringBuilder : PoolableObject
    {
        /// <summary>
        ///  the initial capacity.
        /// </summary>
        private const int capacity = 1000;
        /// <summary>
        ///  the pooled object's index.
        /// </summary>
        private readonly int poolIndex;
        /// <summary>
        ///  the internal <see cref="StringBuilder"/> instance.
        /// </summary>
        private readonly StringBuilder stringBuilder = new StringBuilder(capacity);
        /// <summary>
        ///  Creates a new instance of <see cref="PooledStringBuilder"/>.
        /// <param name="index">the object's index</param>       
        /// </summary>
        public PooledStringBuilder(int index)
        {
            poolIndex = index;
        }
        /// <summary>
        /// Appends the string representation of the <c>char</c> argument to this
        /// sequence.
        /// <p>
        /// The argument is appended to the contents of this sequence.The length of
        /// this sequence increases by <c>1</c>.
        /// <p></p>
        /// The overall effect is exactly as if the argument were converted to a
        /// string by the method <see cref="Object.ToString"/>, and the character in
        /// that string were then appended <see cref="StringBuilder.Append(string)"/> to this character
        /// sequence.
        /// <paramref name="c"/>a <c>char</c>
        /// <exception cref="">if the item was not locked</exception>
        /// </summary>
        public void Append(char c)
        {
            if (StringBuilderPool.Instance.IsItemLocked(this))
            {
                stringBuilder.Append(c);
            }
            else
            {
                throw new PooledException("Item not locked for use!");
            }
        }
        /// <summary>
        /// Appends the string representation of the <c>char</c> array argument to
        /// this sequence.
        /// < p >
        /// The characters of the array argument are appended, in order, to the
        /// contents of this sequence.The length of this sequence increases by the
        /// length of the argument.
        /// < p >
        /// The overall effect is exactly as if the argument were converted to a
        /// string by the method <see cref="Object.ToString"/>, and the characters
        /// of that string were then <see cref="StringBuilder.Append(string)"/> appended to this
        /// character sequence.
        /// <paramref name="str"/> the characters to be appended.
        /// </summary>
        public void Append(char[] str)
        {
            if (StringBuilderPool.Instance.IsItemLocked(this))
            {
                stringBuilder.Append(new String(str));
            }
            else
            {
                throw new PooledException("Item not locked for use!");
            }
        }
        /// <summary>
        /// Appends the string representation of the <c>float</c> argument to
        /// this sequence.
        /// < p >
        /// The characters of the array argument are appended, in order, to the
        /// contents of this sequence.The length of this sequence increases by the
        /// length of the argument.
        /// < p >
        /// The overall effect is exactly as if the argument were converted to a
        /// string by the method <see cref="Object.ToString"/>, and the characters
        /// of that string were then <see cref="StringBuilder.Append(string)"/> appended to this
        /// character sequence.
        /// <paramref name="f"/> the floating-point value to be appended.
        /// </summary>
        public void Append(float f)
        {
            if (StringBuilderPool.Instance.IsItemLocked(this))
            {
                stringBuilder.Append(f);
            }
            else
            {
                throw new PooledException("Item not locked for use!");
            }
        }
        /// <summary>
        /// Appends the string representation of the <c>int</c> argument to
        /// this sequence.
        /// < p >
        /// The characters of the array argument are appended, in order, to the
        /// contents of this sequence.The length of this sequence increases by the
        /// length of the argument.
        /// < p >
        /// The overall effect is exactly as if the argument were converted to a
        /// string by the method <see cref="Object.ToString"/>, and the characters
        /// of that string were then <see cref="StringBuilder.Append(string)"/> appended to this
        /// character sequence.
        /// <paramref name="i"/> the integer value to be appended.
        /// </summary>
        public void Append(int i)
        {
            if (StringBuilderPool.Instance.IsItemLocked(this))
            {
                stringBuilder.Append(i);
            }
            else
            {
                throw new PooledException("Item not locked for use!");
            }
        }
        /// <summary>
        /// Appends the string representation of the <c>object</c> argument to
        /// this sequence.
        /// < p >
        /// The characters of the array argument are appended, in order, to the
        /// contents of this sequence.The length of this sequence increases by the
        /// length of the argument.
        /// < p >
        /// The overall effect is exactly as if the argument were converted to a
        /// string by the method <see cref="Object.ToString"/>, and the characters
        /// of that string were then <see cref="StringBuilder.Append(string)"/> appended to this
        /// character sequence.
        /// <paramref name="o"/> the object value to be appended.
        /// </summary>
        public void Append(object o)
        {
            if (StringBuilderPool.Instance.IsItemLocked(this))
            {
                stringBuilder.Append(o);
            }
            else
            {
                throw new PooledException("Item not locked for use!");
            }
        }
        /// <summary>
        /// Appends the string representation of the <c>string</c> argument to
        /// this sequence.
        /// < p >
        /// The characters of the array argument are appended, in order, to the
        /// contents of this sequence.The length of this sequence increases by the
        /// length of the argument.
        /// < p >
        /// The overall effect is exactly as if the argument were converted to a
        /// string by the method <see cref="Object.ToString"/>, and the characters
        /// of that string were then <see cref="StringBuilder.Append(string)"/> appended to this
        /// character sequence.
        /// <paramref name="str"/> the string value to be appended.
        /// </summary>
        public void Append(string str)
        {
            if (StringBuilderPool.Instance.IsItemLocked(this))
            {
                stringBuilder.Append(str);
            }
            else
            {
                throw new PooledException("Item not locked for use!");
            }
        }
        /// <summary>
        /// 
        /// Gets the value for the object's index.
        /// @return {@link int}

        /// </summary>
        public int GetPoolIndex()
        {
            return poolIndex;
        }
        public void Init()
        {
            // TODO Auto-generated method stub
        }
        public int Length
        {
            get { return stringBuilder.Length; }
            set { stringBuilder.Length = value; }
        }
        public void ReturnToPool()
        {
            stringBuilder.Length = 0;
            if (StringBuilderPool.Instance.IsItemLocked(this))
            {
                StringBuilderPool.Instance.UnlockItem(this);
            }
        }
        /// <summary>
        /// 
        /// Returns a string representing the data in this sequence. A new
        /// {@code String} object is allocated and initialized to contain the
        /// character sequence currently represented by this object. This
        /// {@code String} is then returned. Subsequent changes to this sequence do
        /// not affect the contents of the {@code String}.
        /// @return a string representation of this sequence of characters.
        /// </summary>
        public override String ToString()
        {
            return stringBuilder.ToString();
        }
    }
}