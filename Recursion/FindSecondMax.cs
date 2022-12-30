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

            return GetSecondMax(list);
        }
        
        private static int GetSecondMax(List<int> list, int i = 0, int maxFirst = -1, int maxSecond = -1)
        {
            //if list finished
            if (i > list.Count - 1)
                return maxSecond;

            if (list[i] == maxFirst)
                maxSecond = maxFirst;
            
            if (list[i] > maxFirst)
            {
                maxSecond = -1;
                maxFirst = list[i];
            }

            return GetSecondMax(list, i + 1, maxFirst, maxSecond);
        }

        private static int GetSecondMaxNoRec(List<int> list)
        {
            int maxFirst = -1;
            int maxSecond = -1;
            
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] < maxFirst)
                    continue;
                
                if (list[i] == maxFirst)
                    maxSecond = maxFirst;
                
                if (list[i] > maxFirst)
                {
                    maxSecond = -1;
                    maxFirst = list[i];
                }
            }
            
            return maxSecond;
        }
    }
}