using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static bool TransformTransform(int[] A, int N)
        {
            int[] transform = Transform(A);

            var result = 0;

            foreach (var i in transform)
            {
                result += i;
            }

            return result % 2 == 0;
        }

        private static int[] Transform(int[] A)
        {
            int numberOfTransformation = 2;
            List<int> B = new List<int>();

            for (int n = 0; n < numberOfTransformation; n++)
            {
                B = Transformate(A);
                A = B.ToArray();
            }

            return B.ToArray();
        }

        private static List<int> Transformate(int[] A)
        {
            int n = A.Length;
            List<int> B = new List<int>();
            
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    int k = i + j;
                    int max = FindMax(A, j, k);
                    B.Add(max);
                }
            }

            return B;
        }

        private static int FindMax(int[] A, int j, int k)
        {
            int max = 0;
            
            for (int i = j; i < k; i++)
            {
                if (A[i] > max)
                    max = A[i];
            }

            return max;
        }
    }
}