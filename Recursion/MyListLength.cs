using System;
using System.Collections.Generic;

namespace Recursion
{
    public class MyListLength<T>
    {
        public static int GetLength(List<T> list, int len = 0)
        {
            if (!list.Pop()) 
                return len;
            
            return GetLength(list, len + 1);
        }
    }

    public static class ListExt
    {
        public static bool Pop<T>(this List<T> list)
        {
            try
            {
                list.RemoveAt(0);
                return true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return false;
            }
        }
    }
}