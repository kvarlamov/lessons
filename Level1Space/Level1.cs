using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static int SumOfThe(int N, int [] data)
        {
            int totalSum = 0;

            foreach (var sum in data)
            {
                totalSum += sum;
            }

            for (int i = 0; i < N; i++)
            {
                if (totalSum - data[i] == data[i])
                    return data[i];
            }

            return 0;
        }
    }
}