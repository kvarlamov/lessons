﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Recursion
{
    public class FindSecondMax
    {
        public static int Run(List<int> list)
        {
            if (list == null || list.Count == 0)
                throw new ArgumentException("Wrong argument");
            
            int max = list.GetMax();

            return GetSecondMax(list, max, 0);
        }
        
        private static int GetSecondMax(List<int> list, int max, int curIndex, bool isSecond = false)
        {
            // if not found
            if (curIndex > list.Count - 1)
                return -1;

            // return second max
            if (isSecond && list[curIndex] == max)
                return list[curIndex];
            
            // find first max, set flag to true
            if (list[curIndex] == max)
                isSecond = true;

            return GetSecondMax(list, max, curIndex + 1, isSecond);
        }
    }

    public static class ListExtensions
    {
        public static int GetMax(this List<int> list)
        {
            int max = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] > max)
                    max = list[i];
            }

            return max;
        }
    }
}