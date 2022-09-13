using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static bool TankRush(int H1, int W1, string S1, int H2, int W2, string S2)
        {
            if (H2 > H1 || W2 > W1)
                return false;
            
            string[] split = S1.Split(' ');
            string[] subStr = S2.Split(' ');

            Queue<int> indexes = new Queue<int>();
            
            for (int i = 0; i < H1; i++)
            {
                //pointer to second arr
                int k = 0;
                
                if (split[i].Contains(subStr[k]))
                {
                    indexes = split[i].AllIndexesOf(subStr[k]);
                    k++;
                }

                while (indexes.Count != 0)
                {
                    if (H2 == 1)
                        return true;
                    int index = indexes.Dequeue();
                    for (int j = i + 1; j < H1 && k < H2; j++)
                    {
                        if (!split[j].Contains(subStr[k]) || split[j].Substring(index, subStr[k].Length) != subStr[k])
                            break;
                        if (k == H2 - 1)
                            return true;
                        k++;
                    }
                }
            }

            return false;
        }
        
        public static Queue<int> AllIndexesOf(this string str, string value) {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            Queue<int> indexes = new Queue<int>();
            for (int index = 0;; index += 1) {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Enqueue(index);
            }
        }
    }
}