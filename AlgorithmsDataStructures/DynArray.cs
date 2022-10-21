using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class DynArray<T>
    {
        private const int MinimumCapacity = 16;
        public T [] array;
        public int count;
        public int capacity;

        public DynArray()
        {
            count = 0;
            MakeArray(16);
        }

        public void MakeArray(int new_capacity)
        {
            Array.Resize(ref array, new_capacity);
            capacity = new_capacity;
        }

        /// <summary>
        /// Get by index
        /// </summary>
        /// <param name="index">index of elemet</param>
        /// <returns>item</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T GetItem(int index)
        {
            CheckIndex(index);
            
            return array[index];
        }

        /// <summary>
        /// Append to tail
        /// </summary>
        /// <param name="itm">new item</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Append(T itm)
        {
            ResizeIfFull();

            array[count] = itm;
            count++;
        }

        /// <summary>
        /// insert in index
        /// </summary>
        /// <param name="itm">new item</param>
        /// <param name="index">index to insert</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Insert(T itm, int index)
        {
            CheckIndex(index);
            ResizeIfFull();
            
            for (int i = index; i <= count; i++)
            {
                T tmp = array[i];
                array[i] = itm;
                itm = tmp;
            }

            count++;
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Remove(int index)
        {
            CheckIndex(index);

            for (int i = index; i < count - 1; i++)
            {
                array[i] = array[i + 1];
            }

            count--;
            ShrinkArr();
        }

        /// <summary>
        /// resize array if it's full
        /// </summary>
        private void ResizeIfFull()
        {
            if (count == capacity)
            {
                MakeArray(capacity * 2);
            }
        }

        /// <summary>
        /// shrink if count < 50% of capacity
        /// </summary>
        private void ShrinkArr()
        {
            double result = (double)count / capacity;
            if (result >= 0.5)
                return;

            int newCapacity = (int)(capacity / 1.5);
            if (newCapacity <= MinimumCapacity)
                MakeArray(MinimumCapacity);
            else
                MakeArray(newCapacity);
        }

        private void CheckIndex(int index)
        {
            if (index > count || index < 0)
                throw new IndexOutOfRangeException();
        }
    }
}