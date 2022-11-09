using System.Collections.Generic;
using System;
using System.IO;

namespace AlgorithmsDataStructures
{
    public class BloomFilter
    {
        public int filter_len;
        private byte[] bit_array;

        public BloomFilter(int f_len)
        {
            filter_len = f_len;
            bit_array = new byte[filter_len];
            // создаём битовый массив длиной f_len ...
        }

        // хэш-функции
        public int Hash1(string str1)
        {
            // 17
            int n = 17;
            int res = 0;
            for(int i=0; i<str1.Length; i++)
            {
                int code = (int)str1[i];
                unchecked
                {
                    res = Math.Abs((res * n + code) % filter_len);
                }
            }
            
            return res;
        }
        public int Hash2(string str1)
        {
            // 223
            int n = 223;
            int res = 0;
            for(int i=0; i<str1.Length; i++)
            {
                int code = (int)str1[i];
                unchecked
                {
                    res = Math.Abs((res * n + code) % filter_len);
                }
                
            }
            
            return res;
        }

        public void Add(string str1)
        {
            // добавляем строку str1 в фильтр
            int hash1 = Hash1(str1);
            int hash2 = Hash2(str1);
            
            bit_array[hash1] |= 1;
            bit_array[hash2] |= 1;
        }

        public bool IsValue(string str1)
        {
            // проверка, имеется ли строка str1 в фильтре
            int hash1 = Hash1(str1);
            int hash2 = Hash2(str1);
            
            return (bit_array[hash1] & 1) != 0
                   && (bit_array[hash2] & 1) != 0;
        }
    }
}