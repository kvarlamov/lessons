using System.Collections.Generic;
using System;
using System.IO;

namespace AlgorithmsDataStructures
{
    public class BloomFilter
    {
        public int filter_len;
        private int bittest;

        public BloomFilter(int f_len)
        {
            filter_len = f_len;
            bittest = 0;
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

            var bitmask1 = 1 << hash1;
            var bitmask2 = 1 << hash2;
            bittest |= bitmask1;
            bittest |= bitmask2;
        }

        public bool IsValue(string str1)
        {
            // проверка, имеется ли строка str1 в фильтре
            int hash1 = Hash1(str1);
            int hash2 = Hash2(str1);

            return (bittest & (1 << hash1)) != 0 && (bittest & (1 << hash2)) != 0;
        }
    }
}