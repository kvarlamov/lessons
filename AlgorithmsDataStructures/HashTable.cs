using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class HashTable
    {
        public int size;
        public int step;
        public string [] slots; 

        public HashTable(int sz, int stp)
        {
            size = sz;
            step = stp;
            slots = new string[size];
            for(int i=0; i<size; i++) 
                slots[i] = null;
        }

        /// <summary>
        /// return slot index
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int HashFun(string value)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            
            var res = sum % size;
            
            return res;
        }

        /// <summary>
        /// find empty slot index or -1 if all not empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SeekSlot(string value)
        {
            int index = HashFun(value);

            return Seek(index);
        }

        /// <summary>
        /// put value and return index, or return -1 if index not found
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Put(string value)
        {
            int index = SeekSlot(value);
            
            if (index == -1)
                return -1;

            slots[index] = value;
            return index;
        }

        /// <summary>
        /// return index of slot with provided value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Find(string value)
        {
            int index = HashFun(value);

            return Seek(index, value);
        }

        public List<string> GetAll()
        {
            var res = new List<string>();
            for (int i = 0; i < size; i++)
            {
                res.Add(slots[i]);
            }

            return res;
        }

        /// <summary>
        /// return index of value (or empty slot index if value == null) or -1 if not found empty
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private int Seek(int index, string value = null)
        {
            int maxCircles = size;
            int currStep = step;
            for (int circle = 0; circle <= maxCircles; circle++)
            {
                if (slots[index] == value)
                    return index;

                int i = index + step;
                
                for (; i < size; i += currStep)
                {
                    if (slots[i] == value)
                        return i;
                }

                index = index + 1 > size - 1 ? 0 : index + 1;
                currStep = currStep * step >= size ? step : currStep * step ;
            }

            return -1;
        }
    }
}