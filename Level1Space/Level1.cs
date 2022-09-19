using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static string [] ShopOLAP(int N, string [] items)
        {
            SortedDictionary<string, int> dict = new SortedDictionary<string, int>();

            foreach (var item in items)
            {
                var spaceIndex = item.IndexOf(' ');
                string name = item.Substring(0, spaceIndex);
                int value = int.Parse(item.Substring(spaceIndex + 1));

                if (!dict.TryGetValue(name, out int v))
                {
                    dict.Add(name, value);
                }
                
                dict[name] = v + value;
            }

            return dict.OrderByValue();
        }

        private static string[] OrderByValue(this SortedDictionary<string, int> dic)
        {
            List<string> result = new List<string>();
            while (dic.Count > 0)
            {
                int maxValue = 0;
                string maxKey = string.Empty;
                foreach (var item in dic)
                {
                    if (item.Value > maxValue)
                    {
                        maxValue = item.Value;
                        maxKey = item.Key;
                    }
                }
                
                result.Add(string.Join(" ", maxKey, maxValue));
                dic.Remove(maxKey);
            }
            
            return result.ToArray();
        }
    }
}