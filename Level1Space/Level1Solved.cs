using System;
using System.Collections.Generic;

namespace Level1Space
{
    public class Level1Solved
    {
        public static int squirrel(int N)
        {
            long result = 1;

            for (int i = 2; i <= N; i++)
            {
                result = result * i;
            }

            while (result >= 10)
            {
                result = result / 10;
            }
            
            return (int)result ;
        }
        
        public static int odometer(int [] oksana)
        {
            int s = 0;
            int previousTime = 0;

            for (int i = 0; i < oksana.Length-1; i+=2)
            {
                int speed = oksana[i];
                int time = oksana[i + 1] - previousTime;
                previousTime = oksana[i + 1];

                s += time * speed;
            }

            return s;
        }
        
        public static int ConquestCampaign(int N, int M, int L, int [] battalion)
        {
            int day=0;
            int counter = 0;
            int allFields = N * M;
            bool[,] field = new bool[N, M];
            
            List<int> nextStep = new List<int>(battalion);
            for (int i = 0; i < nextStep.Count; i++)
            {
                nextStep[i]--;
            }
            List<int> toInvade;
            char[] directions = new[] {'n','s','w','e'};

            while (allFields > counter)
            {
                toInvade = nextStep;
                nextStep = new List<int>();
	
                for(int i = 0; i < toInvade.Count - 1; i+=2)
                {
                    foreach(var d in directions) 
                    {
                        int row = toInvade[i];
                        int column = toInvade[i+1];

                        if (day > 0)
                        {
                            switch (d)
                            {
                                case 'n':
                                    column += 1;
                                    break;
                                case 's':
                                    column -= 1;
                                    break;
                                case 'w':
                                    row -= 1;
                                    break;
                                case 'e':
                                    row += 1;
                                    break;
                            }
                        }

                        if (row > N-1 || column > M-1 || row < 0 || column < 0)
                            continue;
		
                        if (!field[row,column])
                        {
                            field[row,column] = true;
                            nextStep.Add(row);
                            nextStep.Add(column);
                            counter++;
                        }
                        
                        if (day == 0)
                            break;
                    }
                }
	
                day++;
                if (allFields == counter)
                    break;
            }

            return day;
        }
        
        public static int [] MadMax(int N, int [] Tele)
        {
            int l = Tele.Length;
            int[] result = new int[l];
            Array.Sort(Tele);
            int maxIndex = N / 2;
    
            result[maxIndex] = Tele[l-1];
    
            for(int i = 0; i < maxIndex; i++)
            {
                result[i] = Tele[i];
                result[i + maxIndex + 1] = Tele[l-2-i];
            }
    
            return result;
        }
        
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
        
        public static string PatternUnlock(int N, int [] hits)
        {
            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            var separator = ci.NumberFormat.CurrencyDecimalSeparator;

            if (N == 0)
                return string.Empty;
            
            if (N == 1)
                return hits[0].ToString();
            
            double sqrTwo = 1.414213562373;
            var diagonals = new Dictionary<int, int[]>();
            diagonals.Add(1, new int [] {5,8});
            diagonals.Add(2, new int [] {6,9,4,7});
            diagonals.Add(3, new int [] {5,8});
            diagonals.Add(4, new int [] {2});
            diagonals.Add(5, new int [] {1,3});
            diagonals.Add(6, new int [] {2});
            diagonals.Add(7, new int [] {2});
            diagonals.Add(8, new int [] {1,3});
            diagonals.Add(9, new int [] {2});

            double sum = 0;

            for(int i = 0; i < N - 1; i++)
            {
                bool flag = diagonals.TryGetValue(hits[i], out var array);
                if (flag && Array.IndexOf(array, hits[i + 1]) != -1)
                {
                    sum += sqrTwo;
                }
                else
                {
                    sum += 1;
                }
            }

            string sums = sum.ToString();
            string[] split = sums.Split(separator[0]);
            if (split.Length > 1)
            {
                double roundChar = Char.GetNumericValue(split[1][5]);
                if (roundChar > 4)
                {
                    double toChange = Char.GetNumericValue(split[1][4]) + 1;
                    split[1] = split[1].Remove(4, 1);
                    split[1] = split[1].Insert(4, toChange.ToString());
                }

                split[1] = split[1].Substring(0, 5);
            }

            string result = string.Join("", split);
            result = result.Replace(separator, "").Replace("0", "");

            return result;
        }
        
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
        
        public static int SumOfThe(int N, int [] data)
        {
            int totalSum = 0;

            foreach (var sum in data)
            {
                totalSum += sum;
            }

            for (int i = 0; i < N; i++)
            {
                if (totalSum - data[i] == data[i])
                    return data[i];
            }

            return 0;
        }
    }
}