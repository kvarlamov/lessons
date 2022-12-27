using System;
using System.Collections.Generic;

namespace Recursion
{
    public class PrintOnlyEven
    {
        public static void EvenPrint(List<int> list, int indx = 0)
        {
            if (list.Count==0 || indx > list.Count - 1)
            {
                return;
            }
            
            if (list[indx] % 2 == 0)
                Console.Write(list[indx] + " ");
            
            EvenPrint(list, indx + 1);
        }
    }
}