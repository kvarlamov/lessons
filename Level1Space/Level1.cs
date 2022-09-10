using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static int [] UFO(int N, int [] data, bool octal)
        {
            int a = octal ? 8 : 16;
            int[] result = new int[N];
            for (var i = 0; i < N; i++)
            {
                int deg = 0;
                double localResult = 0;
                while (data[i] > 0)
                {
                    var t = data[i] % 10;
                    data[i] = data[i] / 10;
                    localResult += Math.Pow(a, deg++) * t;
                }

                result[i] = (int)localResult;
            }

            return result;
        }
    }
}