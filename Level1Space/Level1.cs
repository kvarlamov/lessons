using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static string Keymaker(int k)
        {
           System.Text.StringBuilder sb = new System.Text.StringBuilder(k);
            
            for (int i = 0; i < k; i++)
            {
                // open all doors
                sb.Append('1');
            }
            
            for (int i = 1; i < k; i+=2)
            {
                // close every 2 door
                sb[i] = '0';
            }

            for (int j = 2; j < k; j++)
            {
                for (int i = j; i < k; i+=j+1)
                {
                    sb[i] = sb[i] == '0' ? '1' : '0';
                }
            }
            
            return sb.ToString();
        }
    }
}