using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Level1Space
{
    public static class Level1
    {
        public static bool white_walkers(string village)
        {
            if (string.IsNullOrEmpty(village) || !village.Contains("="))
                return false;

            bool flag = false;

            village = Regex.Replace(village, "[A-Za-z]", "");
            
            for (int i = 0; i < village.Length; i++)
            {
                int whiteWalkers = 0;
                
                if (!int.TryParse(village[i].ToString(), out int numFirst))
                {
                    continue;
                }

                int j = i + 1;
                int numSecond = 0;
                bool isWalkers = false;
                while (j < village.Length && !int.TryParse(village[j].ToString(), out numSecond))
                {
                    if (village[j] == '=')
                    {
                        isWalkers = true;
                        whiteWalkers++;
                    }

                    j++;
                }

                if (!isWalkers || numFirst + numSecond != 10)
                    continue;
                
                if (numFirst + numSecond == 10 && whiteWalkers != 3)
                    return false;

                flag = true;
                i = j-1;
            }
            
            return flag;
        }
    }
}