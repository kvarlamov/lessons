using System;
using System.Collections.Generic;

namespace Recursion
{
    public class PrintOnlyEvenIndices<T>
    {
        public static void Print(List<T> list, int index = 0)
        {
            if (list.Count == 0 || index > list.Count - 1)
                return;
            
            Console.Write(list[index] + " ");
            
            Print(list, index + 2);
        }
    }
}