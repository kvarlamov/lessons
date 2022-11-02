using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class NativeDictionary<T>
    {
        private const int step = 3;
        public int size;
        public string [] slots;
        public T [] values;

        public NativeDictionary(int sz)
        {
            size = sz;
            slots = new string[size];
            values = new T[size];
        }

        public int HashFun(string key)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(key);
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            
            var res = sum % size;
            
            return res;
        }

        /// <summary>
        /// true if key exists, else - false
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsKey(string key)
        {
            int index = HashFun(key);

            return Seek(index, key) != -1;
        }

        /// <summary>
        /// Put value by key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Put(string key, T value)
        {
            int index = HashFun(key);
            
            index = IsKey(key) ? Seek(index, key) : Seek(index);
            
            slots[index] = key;
            values[index] = value;
        }

        /// <summary>
        /// Get value by key or null(default) if not found  
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(string key)
        {
            int index = HashFun(key);

            int finalIndex = Seek(index, key);
            
            if (finalIndex == -1)
                return default(T);
            
            return values[finalIndex];
        }

        /// <summary>
        /// return slot index by provided key or -1 if all is not empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int SeekSlot(string value)
        {
            int index = HashFun(value);

            return Seek(index);
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