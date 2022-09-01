using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static string PatternUnlock(int N, int [] hits)
        {
            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            var separator = ci.NumberFormat.CurrencyDecimalSeparator;

            if (N == 0)
                return string.Empty;
            
            if (N == 1)
                return hits[0].ToString();
            
            double sqrTwo = 1.414213562373;
            var diagonals = new Dictionary<int, int[]>();
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
                bool flag = diagonals.TryGetValue(hits[i], out var array);
                if (flag && Array.IndexOf(array, hits[i + 1]) != -1)
                {
                    sum += sqrTwo;
                }
                else
                {
                    sum += 1;
                }
            }

            string sums = sum.ToString();
            string[] split = sums.Split(separator[0]);
            if (split.Length > 1)
            {
                double roundChar = Char.GetNumericValue(split[1][5]);
                if (roundChar > 4)
                {
                    double toChange = Char.GetNumericValue(split[1][4]) + 1;
                    split[1] = split[1].Remove(4, 1);
                    split[1] = split[1].Insert(4, toChange.ToString());
                }

                split[1] = split[1].Substring(0, 5);
            }

            string result = string.Join("", split);
            result = result.Replace(separator, "").Replace("0", "");

            return result;
        }
    }
}