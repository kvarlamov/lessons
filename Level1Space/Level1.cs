using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static string PatternUnlock(int N, int [] hits)
        {
            double sqrTwo = Math.Sqrt(2);
            Dictionary<int, int[]> diagonals = new Dictionary<int, int[]>();
            diagonals.Add(1, new int [] {5,8});
            diagonals.Add(2, new int [] {6,9,4,7});
            diagonals.Add(3, new int [] {5,8});
            diagonals.Add(4, new int [] {2});
            diagonals.Add(5, new int [] {1,3});
            diagonals.Add(6, new int [] {2});
            diagonals.Add(7, new int [] {2});
            diagonals.Add(8, new int [] {1,3});
            diagonals.Add(9, new int [] {2});

            double sum = 0;

            for(int i = 0; i < N - 1; i++)
            {
                if (Array.IndexOf(diagonals[hits[i]], hits[i + 1]) != -1)
                {
                    sum += sqrTwo;
                }
                else
                {
                    sum += 1;
                }
            }
            
            sum = Math.Round(sum, 5);
            string sumS = sum.ToString().Replace(".","").Replace("0","");

            return sumS;
        }
    }
}