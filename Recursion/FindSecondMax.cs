using System;
using System.Collections.Generic;

namespace Recursion
{
    public class FindSecondMax
    {
        public static int Run(List<int> list)
        {
            if (list == null || list.Count == 0)
                throw new ArgumentException("Wrong argument");

            return GetSecondMax(list, new List<int>(), 0);
        }
        
        private static int GetSecondMax(List<int> list, List<int> maxIndices, int curIndex, int max = 0)
        {
            //if list finished and found 2 max
            if (curIndex > list.Count - 1 && maxIndices.Count > 1)
                return maxIndices[1];
            
            // if list finished and not found 2 max
            if (curIndex > list.Count - 1 && maxIndices.Count <= 1)
                return -1;

            if (list[curIndex] > max)
            {
                maxIndices.Clear();
                max = list[curIndex];
            }

            if (list[curIndex] >= max)
            {
                maxIndices.Add(curIndex);
            }

            return GetSecondMax(list, maxIndices, curIndex + 1, max);
        }

        private static int GetSecondMaxNoRec(List<int> list)
        {
            int max = 0;
            var maxIndexes = new List<int>();
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] < max)
                    continue;
                
                if (list[i] > max)
                {
                    maxIndexes.Clear();
                    max = list[i];
                }

                maxIndexes.Add(i);
            }
            
            return maxIndexes.Count > 1 ? maxIndexes[1] : -1;
        }
    }
}