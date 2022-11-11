using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class NativeCache<T>
    {
        private const int step = 1;
        private const int maxCircles = 1;
        
        private int size;
        public string[] slots;
        public T [] values;
        public int [] hits;

        public NativeCache(int size)
        {
            this.size = size;
            slots = new string[size];
            values = new T[size];
            hits = new int[size];
        }
        

        public T AddOrGetExisting(string key, T value)
        {
            int index = HashFun(key);
            int seekIndex = Seek(index, key);
            
            //if we've had a key
            if (seekIndex != -1)
            {
                hits[seekIndex]++;
                return values[seekIndex];
            }

            
            int newIndex = Seek(index);
            
            // if there no empty slot - change slot with minimum hits
            if (newIndex == -1)
            {
                var minHits = FindMinHits();
                hits[minHits] = 1;
                slots[minHits] = key;
                values[minHits] = value;
                return value;
            }
            
            //set new key-value to empty slot
            slots[newIndex] = key;
            values[newIndex] = value;
            hits[newIndex]++;
            return value;
        }

        private int FindMinHits()
        {
            int min = 0;
            for (int i = 1; i < hits.Length; i++)
            {
                //the lowest - not need to go througth the all array
                if (hits[min] == 0)
                    return min;
                
                if (hits[i] < hits[min])
                    min = i;
            }

            return min;
        }

        private int Seek(int index, string key = null)
        {
            int initialIndex = index;
            
            for (int j = 0; j <= maxCircles; j++)
            {
                if (slots[index] == key)
                    return index;

                int barrier = size;
                if (j == maxCircles)
                    barrier = initialIndex;
                
                for (int i = index + step; i < barrier; i+=step)
                {
                    if (slots[i] == key)
                        return i;
                }

                index = 0;
            }
            
            return -1;
        }
        private int HashFun(string key)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(key);
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }

            return sum % size;
        }
        
    }
}