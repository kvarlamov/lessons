using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static string BiggerGreater(string input)
        {
            //check that all chars equal
            if (string.IsNullOrEmpty(input.Replace(input[0].ToString(), string.Empty)))
            {
                return string.Empty;
            }

            int l = input.Length;

            if (input[l - 1] > input[l - 2])
            {
                return Swap(input, l - 1, l - 2);
            }

            // firstly set first char next bigger
            char currentFind = input[0];
            char min = input[1];
            int index = 0;
            for (int i = 2; i < input.Length - 1; i++)
            {
                if (input[i] < min && input[i] > currentFind)
                {
                    currentFind = input[i];
                    index = i;
                }
            }

            if (index != 0)
                input = Swap(input, 0, index);

            var tail = input.Substring(1).ToCharArray();
            Array.Sort(tail);
            
            return string.Concat(input[0].ToString(), new string(tail));
        }

        private static string Swap(string input, int index1, int index2)
        {
            string a = input[index1].ToString();
            string b = input[index2].ToString();

            input = input.Remove(index1, 1).Insert(index1, b);
            input = input.Remove(index2, 1).Insert(index2, a);

            return input;
        }
    }
}