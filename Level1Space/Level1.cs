using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static bool LineAnalysis(string line)
        {
            if (line[0] != '*' || line[line.Length - 1] != '*')
                return false;

            if (!line.Contains("."))
                return true;

            string pattern = string.Empty;
            string current = string.Empty;

            int i = 0;
            while (i < line.Length)
            {
                int to = line.IndexOf('*', i + 1 );
                if (to < 0)
                    break;

                current = line.Substring(i, to - i);
                if (!string.IsNullOrEmpty(pattern) && !pattern.Equals(current))
                {
                    return false;
                }

                pattern = current;
                i=to;
            }

            return true;
        }
    }
}