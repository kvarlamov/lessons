using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public static class sBalancedBST
    {
        public static int[] GenerateBBSTArray(int[] a)
        {
            if (a== null || a.Length == 0)
                return null;
            
            //sort input array
            Array.Sort(a);

            var aBst = new int[a.Length];
            
            G(a, aBst, 0, 0, a.Length - 1);

            return aBst;
        }

        //i - current index in new array
        private static void G(int[] input, int[] aBST, int i,  int left, int right)
        {
            if (left > right || i > aBST.Length - 1)
                return;
            
            int rootIndex = GetCentralIndex(left, right);
            aBST[i] = input[rootIndex];
            
            // set left
            G(input, aBST, 2 * i + 1, left, rootIndex - 1);

            // set right
            G(input, aBST, 2 * i + 2, rootIndex + 1, right);
        }
        
        private static int GetCentralIndex(int left, int right) => (left + right) / 2;
    }
}