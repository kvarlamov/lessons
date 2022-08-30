using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static int [] MadMax(int N, int [] Tele)
        {
            int l = Tele.Length;
            int[] result = new int[l];
            Array.Sort(Tele);
            int maxIndex = N / 2;
    
            result[maxIndex] = Tele[l-1];
    
            for(int i = 0; i < maxIndex; i++)
            {
                result[i] = Tele[i];
                result[i + maxIndex + 1] = Tele[l-2-i];
            }
    
            return result;
        }
    }
}