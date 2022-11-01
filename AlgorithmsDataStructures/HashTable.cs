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

        public int SeekSlot(string value)
        {
            int index = HashFun(value);

            return Seek(index);
        }

        public int Put(string value)
        {
            int index = SeekSlot(value);
            
            if (index == -1)
                return -1;

            slots[index] = value;
            return index;
        }

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

        private int Seek(int index, string value = null)
        {
            int circle = 0;
            int maxCircles = size;
            int currStep = step;
            while (circle <= maxCircles)
            {
                if (slots[index] == value)
                    return index;

                int i = index + step;
                
                for (; i < size; i += currStep)
                {
                    if (slots[i] == value)
                        return i;
                }

                circle++;
                index = index + 1 > size - 1 ? 0 : index + 1;
                currStep = currStep * step >= size ? step : currStep * step ;
            }

            return -1;
        }
    }
}