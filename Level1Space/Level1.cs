using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static bool SherlockValidString(string s)
        {
            if (s.Length == 2)
                return true;
            
            Dictionary<char, int> dic = new Dictionary<char, int>();

            int maxFreq = 0;

            foreach (var ch in s)
            {
                int freq = 0;
                
                if (!dic.TryGetValue(ch, out freq))
                {
                    dic.Add(ch, freq);
                }

                
                dic[ch] = freq + 1;
            }

            Dictionary<int, int> res = new Dictionary<int, int>();
            List<int> keys = new List<int>();

            foreach (var value in dic.Values)
            {
                int freq = 0;

                if (!res.TryGetValue(value, out freq))
                {
                    res.Add(value, freq);
                    keys.Add(value);
                }

                res[value] = freq + 1;
            }

            if (res.Count == 1)
                return true;
            
            if (res.Count > 2 || !res.ContainsValue(1) || Math.Abs(keys[0] - keys[1]) > 1)
                return false;

            return true;
        }
    }
}