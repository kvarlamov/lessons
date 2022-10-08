using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static bool Football(int[] F, int N)
        {
            if (IsOrdered(F))
                return false;
            
            var copy = (int[])F.Clone();
            
            return Rule1(F) || Rule2(copy);
        }

        private static bool Rule1(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] < arr[i + 1])
                {
                    continue;
                }

                if (i > 1 && arr[i-1] > arr[i + 1])
                {
                    return false;
                }
                
                int from = i > 0 ? arr[i - 1] : 0;
                int to = arr.Length == 3 ? arr[i] : arr[i + 1];
                int swapIndex = FindSwapIndex(arr, from, to, i + 1);

                if (swapIndex == -1)
                    return false;
                
                (arr[i], arr[swapIndex]) = (arr[swapIndex], arr[i]);

                break;
            }

            return IsOrdered(arr);
        }
        
        private static bool Rule2(int[] arr)
        {
            if (arr.Length == 3)
            {
                Array.Reverse(arr);
                return IsOrdered(arr);
            }
            
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] < arr[i + 1])
                    continue;

                int from = i;

                int to = FindToIndex(arr, i);

                ReverseArrayPart(arr, from, to);
                
                break;
            }

            return IsOrdered(arr);
        }

        private static void ReverseArrayPart(int[] arr, int from, int to)
        {
            for (int j = from, i = to; j <= i; j++, i--)
            {
                (arr[j], arr[i]) = (arr[i], arr[j]);
            }
        }

        private static int FindToIndex(int[] arr, int i)
        {
            int left = i > 0 ? arr[i - 1] : arr[i];
            
            
            for (int j = i + 1; j < arr.Length - 1; j++)
            {
                if ((arr[j] > left || i == 0) && arr[j] < arr[j + 1])
                    return j;
            }

            return arr.Length - 1;
        }

        private static int FindSwapIndex(int[] arr, int from, int to, int i)
        {
            for (; i < arr.Length; i++)
            {
                if (arr[i] > from && arr[i] < to)
                    return i;
            }

            return -1;
        }

        private static bool IsOrdered(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                    return false;
            }

            return true;
        }
    }
}