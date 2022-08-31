using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static string PatternUnlock(int N, int [] hits)
        {

            Dictionary<int, int[]> diagonals = new Dictionary<int, int[]>
            {
                {6, new int[] {2}},
                {1, new int[] {5,8}},
                {9, new int[] {2}},
                {5, new int[] {1,3}},
                {2, new int[] {6,9,4,7}},
                {8, new int[] {1,3}},
                {4, new int[] {2}},
                {3, new int[] {5,8}},
                {7, new int[] {2}}
            };

            double sum = 0;

            for(int i=0; i < N - 1; i++)
            {
                //long diagonal
                if (((hits[i] == 6 || hits[i] == 7) && (hits[i + 1] == 6 || hits[i + 1] == 7)) || 
                ((hits[i] == 9 || hits[i] == 4) && (hits[i + 1] == 9 || hits[i + 1] == 4)))
                    sum += Math.Sqrt(2) * 2;
                else if (Array.IndexOf(diagonals[hits[i]], hits[i+1]) != -1)
                    sum += Math.Sqrt(2);
                else
                    sum += 1;
            }
            
            sum = Math.Round(sum, 5);

            return sum.ToString().Replace(".","").Replace("0","");
        }
    }
}