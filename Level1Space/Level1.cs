using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static int [] WordSearch(int len, string s, string subs)
        {
            List<string> searchList = new List<string>();
            int counter = 0;
            bool spacesInLine = false;
            int spaceIndexLocal = 0;
            int spaceIndexGlobal = 0;
            string currentLine = string.Empty;
            
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    spacesInLine = true;
                    spaceIndexGlobal = i;
                    spaceIndexLocal = currentLine.Length;
                }
                
                currentLine += s[i];
            
                if (i == s.Length - 1)
                {
                    searchList.Add(currentLine.Trim());
                }
                else if (currentLine.Length == len)
                {
                    int j = i + 1;

                    if (s[j] == ' ')
                    {
                        searchList.Add(currentLine.Trim());
                        i = j;
                    }
                    else
                    {
                        while (j < s.Length && s[j] != ' ')
                        {
                            currentLine += s[j];
                            j++;
                        }

                        int wordLength = spacesInLine ? j - spaceIndexGlobal - 1 : currentLine.Length;
                        if (wordLength <= len)
                        {
                            //cut to previous word
                            currentLine = currentLine.Substring(0, spaceIndexLocal);
                            if (spacesInLine)
                                i = spaceIndexGlobal;
                        }
                        else
                        {
                            //cut current long word
                            currentLine = currentLine.Substring(0, len);
                            //пока длина остатка слова больше len добавляем в массив и переносим указатель
                        }
                        
                        searchList.Add(currentLine.Trim());
                    }
                    
                    spacesInLine = false;
                    
                    currentLine = string.Empty;
                    if (counter++ >= s.Length)
                        return null; //error
                }
            }
            
            int[] result = new int[searchList.Count];
            
            for (var i = 0; i < searchList.Count; i++)
            {
                if (!searchList[i].Contains(subs))
                    result[i] = 0;
                else
                {
                    var arr = searchList[i].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var str in arr)
                    {
                        if (str.Equals(subs))
                        {
                            result[i] = 1;
                            break;
                        }
                    }
                }
            }

            return result;
        }

    }
}