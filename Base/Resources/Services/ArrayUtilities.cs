using Godot;
using System;

namespace Base.Resources.Services
{
    public class ArrayUtilities : Node
    {
        /// <summary>
        /// Reference to the singleton instance.
        /// </summary>
        /// <value></value>
        public static ArrayUtilities Instance { get; private set; }
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            Instance = this;
        }
        public bool ArrayContainsOneElement(int[] arr0, int[] arr1)
        {
            bool contains = false;
            for (int i = arr0.Length - 1; i >= 0; i--)
            {
                for (int j = arr1.Length - 1; j >= 0; j--)
                {
                    if (arr0[i] == arr1[j])
                    {
                        contains = true;
                        break;
                    }
                }
            }
            return contains;
        }
        public T[] CopyArray<T>(T[] src)
        {
            T[] dest = new T[src.Length];

            for (int i = src.Length - 1; i >= 0; i--)
            {
                dest[i] = src[i];
            }

            return dest;
        }
        /// <summary>
        /// Extends an array, adding a new element to the last index.
        /// </summary>
        /// <typeparam name="T">the parameterized type</typeparam>
        /// <param name="element">the new element</param>
        /// <param name="src">the source array</param>
        /// <returns></returns>
        public T[] ExtendArray<T>(T element, T[] src)
        {
            T[] dest = new T[src.Length + 1];
            Array.Copy(src, dest, src.Length);
            dest[src.Length] = element;
            return dest;
        }
        /// <summary>
        /// Inserts a new element into the source array at a specific position.
        /// </summary>
        /// <typeparam name="T">the parameterized type</typeparam>
        /// <param name="element">the new element</param>
        /// <param name="index">the element's index</param>
        /// <param name="src">the source array</param>
        /// <returns><see cref="T"/>[]</returns>
        public T[] InsertElement<T>(T element, int index, T[] src)
        {
            T[] dest = new T[src.Length + 1];
            // insert the elements from the  
            // old array into the new array 
            // insert all elements till pos 
            // then insert x at pos 
            // then insert rest of the elements 
            for (int i = 0; i < src.Length + 1; i++)
            {
                if (i < index)
                {
                    dest[i] = src[i];
                }
                else if (i == index)
                {
                    dest[i] = element;
                }
                else
                {
                    dest[i] = src[i - 1];
                }
            }
            return dest;
        }
        /// <summary>
        /// Extends an array, adding a new element to the first index.
        /// </summary>
        /// <typeparam name="T">the parameterized type</typeparam>
        /// <param name="element">the new element</param>
        /// <param name="src">the source array</param>
        /// <returns></returns>
        public T[] PrependArray<T>(T element, T[] src)
        {
            T[] dest = new T[src.Length + 1];
            Array.Copy(src, 0, dest, 1, src.Length);
            dest[0] = element;
            return dest;
        }
        /// <summary>
        /// Removes an element from an array.
        /// </summary>
        /// <typeparam name="T">the parameterized type</typeparam>
        /// <param name="index">the element's index</param>
        /// <param name="src">the source array</param>
        /// <returns><see cref="T"/>[]</returns>
        public T[] RemoveIndex<T>(int index, T[] src)
        {
            T[] dest = new T[src.Length - 1];
            if (index > 0)
            {
                Array.Copy(src, 0, dest, 0, index);
            }
            if (index < src.Length - 1)
            {
                Array.Copy(src, index + 1, dest, index, src.Length - index - 1);
            }
            return dest;
        }
    }
}
