using System;
using System.Collections.Generic;

namespace Recursion
{
    public class FindSecondMax
    {
        public static int Run(List<int> list)
        {
            if (list == null || list.Count == 0 || list.Count < 2)
                throw new ArgumentException("Wrong argument");

            int max, secondMax;
            if (list[0] >= list[1])
            {
                max = list[0];
                secondMax = list[1];
            }
            else
            {
                max = list[1];
                secondMax = list[0];
            }
            
            return GetSecondMax(list, 2, max, secondMax);
            // return GetSecondMaxNoRec(list, max, secondMax);
        }
        
        private static int GetSecondMax(List<int> list, int i, int max, int secondMax)
        {
            //if list finished
            if (i > list.Count - 1)
                return secondMax;

            if (list[i] > max )
            {
                secondMax = max;
                max = list[i];
            } else if (list[i] > secondMax)
            {
                secondMax = list[i];
            }

            return GetSecondMax(list, i + 1, max, secondMax);
        }

        private static int GetSecondMaxNoRec(List<int> list, int max, int secondMax)
        {
            for (var i = 2; i < list.Count; i++)
            {
                if (list[i] > max )
                {
                    secondMax = max;
                    max = list[i];
                } else if (list[i] > secondMax)
                {
                    secondMax = list[i];
                }
            }
            
            return secondMax;
        }
    }
}