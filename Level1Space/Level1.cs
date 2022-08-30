using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static int [] SynchronizingTables(int N, int [] ids, int [] salary)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>(N);
            int[] idsSort = new int[N];
            Array.Copy(ids, idsSort, N);
            Array.Sort(idsSort);
            Array.Sort(salary);

            for (var i = 0; i < N; i++)
            {
                dictionary.Add(idsSort[i], salary[i]);
            }

            for (int i = 0; i < N; i++)
            {
                salary[i] = dictionary[ids[i]];
            }

            return salary;
        }
    }
}