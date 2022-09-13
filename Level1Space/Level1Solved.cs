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
        
        public static string TheRabbitsFoot(string s, bool encode)
        {
            string s1 = s.Replace(" ", "");
            return encode ?  Encode(s1) : Decode(s.Split(' '), s1.Length);
        }

        private static string Decode(string[] arr, int len)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            int k = 0;
            
            while (sb.Length < len)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (k < arr[i].Length)
                        sb.Append(arr[i].Substring(k, 1));
                }

                k++;
            }
            
            return sb.ToString();
        }

        private static string Encode(string s1)
        {
            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            var separator = ci.NumberFormat.CurrencyDecimalSeparator;
            double n = Math.Sqrt(s1.Length);
            string[] sqrt = n.ToString().Split(separator[0]);
            int row = int.Parse(sqrt[0]);
            int column = 1;
            if (sqrt.Length > 1)
                column = int.Parse(sqrt[1].Substring(0,1));

            while (row * column < s1.Length)
                row++;

            char[,] arr = new char[row, column];
            
            int k = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column && k < s1.Length; j++)
                {
                    arr[i, j] = s1[k++];
                }
            }

            List<string> arrRes = new List<string>();
            k = 0;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row && k < s1.Length; j++)
                {
                    if (arr[j, i] != 0)
                        sb.Append(arr[j, i]); 
                }
                arrRes.Add(sb.ToString());
                sb.Clear();
            }

            return string.Join(" ", arrRes);
        }
        
        public static int PrintingCosts(string Line)
        {
            int res = 0;
            Dictionary<char, int> tonerCost = GetDictionary();
            Console.WriteLine(tonerCost.Count);
            const int CostDefault = 23;
            
            foreach (var ch in Line)
            {
                if (!tonerCost.TryGetValue(ch, out int cost))
                    res += CostDefault;
                
                res += cost;
            }

            return res;
        }

        private static Dictionary<char,int> GetDictionary()
        {
            return new Dictionary<char, int>()
            {
                {' ', 0},
                {'&', 24},
                {',', 7},
                {'2', 22},
                {'8', 23},
                {'>', 10},
                {'D', 26},
                {'J', 18},
                {'P', 23},
                {'V', 19},
                {'\\', 10},
                {'b', 25},
                {'h', 21},
                {'n', 18},
                {'t', 17},
                {'z', 19},
                {'!', 9},
                {'\'', 3},
                {'-', 7},
                {'3', 23},
                {'9', 26},
                {'?', 15},
                {'E', 26},
                {'K', 21},
                {'Q', 31},
                {'W', 26},
                {']', 18},
                {'c', 17},
                {'i', 15},
                {'o', 20},
                {'u', 17},
                {'{', 18},
                {'\"', 6},
                {'(', 12},
                {'.', 4},
                {'4', 21},
                {':', 8},
                {'@', 32},
                {'F', 20},
                {'L', 16},
                {'R', 28},
                {'X', 18},
                {'^', 7},
                {'d', 25},
                {'j', 20},
                {'p', 25},
                {'v', 13},
                {'|', 12},
                {'#', 24},
                {')', 12},
                {'/', 10},
                {'5', 27},
                {';', 11},
                {'A', 24},
                {'G', 25},
                {'M', 28},
                {'S', 25},
                {'Y', 14},
                {'_', 8},
                {'e', 23},
                {'k', 21},
                {'q', 25},
                {'w', 19},
                {'}', 18},
                {'$', 29},
                {'*', 17},
                {'0', 22},
                {'6', 26},
                {'<', 10},
                {'B', 29},
                {'H', 25},
                {'N', 25},
                {'T', 16},
                {'Z', 22},
                {'`', 3},
                {'f', 18},
                {'l', 16},
                {'r', 13},
                {'x', 13},
                {'~', 9},
                {'%', 22},
                {'+', 13},
                {'1', 19},
                {'7', 16},
                {'=', 14},
                {'C', 20},
                {'I', 18},
                {'O', 26},
                {'U', 23},
                {'[', 18},
                {'a', 23},
                {'g', 30},
                {'m', 22},
                {'s', 21},
                {'y', 24}
            };
        }
        
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
        
        public static string MassVote(int N, int [] Votes)
        {
            const string case1 = "majority winner ";
            const string case2 = "minority winner ";
            const string case3 = "no winner";
            
            if (N == 1)
            {
                return case1 + "1";
            }
            
            double allVotes = Votes[0];
            List<int> maxIndices = new List<int>();
            //number of candidate (index + 1)
            maxIndices.Add(1);
            double leader = Votes[0];

            for (var i = 1; i < Votes.Length; i++)
            {
                allVotes += Votes[i];
                
                if (Votes[i] == leader)
                {
                    maxIndices.Add(i + 1);
                }
                
                if (Votes[i] > leader)
                {
                    maxIndices.Clear();
                    leader = Votes[i];
                    maxIndices.Add(i + 1);
                }
            }

            if (maxIndices.Count > 1)
            {
                return case3;
            }

            var leaderPercent = Math.Round(100 * (leader / allVotes), 3);

            if (leaderPercent > 50)
            {
                return case1 + maxIndices[0];
            }

            return case2 + maxIndices[0];
        }
        
        public static int [] UFO(int N, int [] data, bool octal)
        {
            int a = octal ? 8 : 16;
            int[] result = new int[N];
            for (var i = 0; i < N; i++)
            {
                int deg = 0;
                double localResult = 0;
                while (data[i] > 0)
                {
                    var t = data[i] % 10;
                    data[i] = data[i] / 10;
                    localResult += Math.Pow(a, deg++) * t;
                }

                result[i] = (int)localResult;
            }

            return result;
        }
        
        public static int Unmanned(int L, int N, int [][] track)
        {
            int travelTime = 0;
            int carCoordinates = 0;

            for (int i = 0; i < N && L > 0; i++)
            {
                int signalInterval = track[i][1] + track[i][2];
                //go to traffic light
                while (carCoordinates < track[i][0] && L > 0)
                {
                    travelTime++;
                    carCoordinates++;
                    L--;
                }
                
                if (L <= 0)
                    break;
                
                int currentTimePointOfCar = travelTime - signalInterval * (travelTime / signalInterval);
                
                while(currentTimePointOfCar < track[i][1])
                {
                    //red - wait for green
                    travelTime++;
                    currentTimePointOfCar = travelTime - signalInterval * (travelTime / signalInterval);
                }

                L--;
                carCoordinates++;
                travelTime++;
            }

            while (L > 0)
            {
                travelTime++;
                L--;
            }
            
            return travelTime;
        }
    }
}