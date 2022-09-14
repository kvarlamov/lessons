using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static int MaximumDiscount(int N, int [] price)
        {
            if (N < 3 || price.Length < 3)
                return 0;
            
            List<int> priceFree = new List<int>();
            const int number = 3;
            Array.Sort(price);
            Array.Reverse(price);

            for (int i = 2; i < N; i+=3)
            {
                priceFree.Add(price[i]);
            }

            int result = 0;
            foreach (var p in priceFree)
            {
                result += p;
            }
            
            return result;
        }
    }
}