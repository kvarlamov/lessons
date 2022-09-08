using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static string BigMinus(string s1, string s2)
        {
            string big = s1;
            string small = s2;
            
            //get bigger string
            if (s1.Length == s2.Length)
            {
                for (int i = 0; i < s1.Length; i++)
                {
                    double a = char.GetNumericValue(s1[i]);
                    double b = char.GetNumericValue(s2[i]);

                    if (a < b)
                    {
                        big = s2;
                        small = s1;
                        break;
                    }

                    if (a > b)
                    {
                        break;
                    }
                }
            }

            if (s1.Length < s2.Length)
            {
                big = s2;
                small = s1;
            }

            string result = string.Empty;

            int t = 0;
            int j = small.Length - 1;
            for (int i = big.Length - 1; i > -1; i--, j--)
            {
                int a = (int)char.GetNumericValue(big[i]);
                int b = 0;
                if (j > -1)
                    b = (int)char.GetNumericValue(small[j]);

                int localres;
                
                if (b > a - t)
                {
                    a += 10;
                    localres = a - b - t;
                    t = 1;
                }
                else
                {
                    localres = a - b - t;
                    t = 0;
                }

                result = result.Insert(0, localres.ToString());
            }

            while (result.Length > 1 && result[0] == '0')
            {
                result = result.Substring(1);
            }

            return result;
        }
    }
}