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
        
        private static int GetSecondMax(List<int> list, List<int> maxValues, int curIndex, int max = 0)
        {
            //if list finished and found 2 max
            if (curIndex > list.Count - 1 && maxValues.Count > 1)
                return maxValues[1];
            
            // if list finished and not found 2 max
            if (curIndex > list.Count - 1 && maxValues.Count <= 1)
                return -1;

            if (list[curIndex] > max)
            {
                maxValues.Clear();
                max = list[curIndex];
            }

            if (list[curIndex] >= max)
            {
                maxValues.Add(list[curIndex]);
            }

            return GetSecondMax(list, maxValues, curIndex + 1, max);
        }

        private static int GetSecondMaxNoRec(List<int> list)
        {
            int max = 0;
            var maxValues = new List<int>();
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] < max)
                    continue;
                
                if (list[i] > max)
                {
                    maxValues.Clear();
                    max = list[i];
                }

                maxValues.Add(list[i]);
            }
            
            return maxValues.Count > 1 ? maxValues[1] : -1;
        }
    }
}