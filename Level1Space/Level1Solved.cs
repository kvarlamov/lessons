using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1Solved
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
        
        public static bool TankRush(int H1, int W1, string S1, int H2, int W2, string S2)
        {
            if (H2 > H1 || W2 > W1)
                return false;
            
            string[] split = S1.Split(' ');
            string[] subStr = S2.Split(' ');

            Queue<int> indexes = new Queue<int>();
            
            for (int i = 0; i < H1; i++)
            {
                //pointer to second arr
                int k = 0;
                
                if (split[i].Contains(subStr[k]))
                {
                    indexes = split[i].AllIndexesOf(subStr[k]);
                    k++;
                }

                while (indexes.Count != 0)
                {
                    if (H2 == 1)
                        return true;
                    int index = indexes.Dequeue();
                    for (int j = i + 1; j < H1 && k < H2; j++)
                    {
                        if (!split[j].Contains(subStr[k]) || split[j].Substring(index, subStr[k].Length) != subStr[k])
                            break;
                        if (k == H2 - 1)
                            return true;
                        k++;
                    }
                }
            }

            return false;
        }
        
        public static Queue<int> AllIndexesOf(this string str, string value) {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            Queue<int> indexes = new Queue<int>();
            for (int index = 0;; index += 1) {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Enqueue(index);
            }
        }
        
        public static int MaximumDiscount(int N, int [] price)
        {
            if (N < 3 || price.Length < 3)
                return 0;
            
            List<int> priceFree = new List<int>();
            const int number = 3;
            Array.Sort(price);
            Array.Reverse(price);

            for (int i = 2; i < N; i+=3)
            {
                priceFree.Add(price[i]);
            }

            int result = 0;
            foreach (var p in priceFree)
            {
                result += p;
            }
            
            return result;
        }
        
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
        
        public static bool MisterRobot(int N, int [] data)
        {
            int expected = 1;
            bool right = false;

            for (int i = 0; i < N; i++)
            {
                if (data[i] == expected)
                {
                    expected++;
                    continue;
                }

                var index = Array.IndexOf(data, expected);
                int leftIndex = i;
                int rightIndex = index;
                if (index - 3 >= i || index == i + 2)
                {
                    leftIndex = index - 2;
                    right = true;
                }

                while (right)
                {
                    int tmp = data[leftIndex + 1];
                    data[leftIndex + 1] = data[leftIndex];
                    data[leftIndex] = data[rightIndex];
                    data[rightIndex] = tmp;
                    
                    rightIndex = leftIndex;
                    if (rightIndex - 3 >= i || i+2 == rightIndex)
                    {
                        leftIndex = rightIndex - 2;
                    }
                    else
                    {
                        leftIndex = i;
                        right = false;
                        break;
                    }
                }

                if (leftIndex + 2 > N - 1 || (index == N - 1 && data[index] == N))
                    break;

                if (expected != data[i])
                {
                    int tmp2 = data[leftIndex + 2];
                    data[leftIndex + 2] = data[leftIndex];
                    data[leftIndex] = data[rightIndex];
                    data[rightIndex] = tmp2;
                }
                
                expected++;
            }

            return data[N-1] == N;
        }
        
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

        private static int _pointer = 0;
        private static List<string> _shoeList = new List<string>() {string.Empty};
        private static bool _wasFour = false;

        public static string BastShoe(string command)
        {
            string arg = string.Empty;
            string cmd = command;

            //so we should have two operands
            if (command.Length > 1)
            {
                var spaceIndex = command.IndexOf(' ');
                cmd = command.Substring(0, spaceIndex);
                arg = command.Substring(spaceIndex + 1);
            }

            string current = string.Empty;

            switch (cmd)
            {
                case "1":
                    current = _pointer > 0 ? string.Concat(_shoeList[_pointer], arg) : arg;

                    if (_wasFour)
                    {
                        _wasFour = false;
                        string last = _shoeList[_pointer];
                        _shoeList.Clear();
                        _shoeList.Add(last);
                        _shoeList.Add(current);
                        _pointer = _shoeList.Count - 1;
                        break;
                    }

                    _pointer += 1;
                    _shoeList.Add(current);
                    break;
                case "2":
                    int trim = int.Parse(arg);
                    if (_pointer > 0 && trim < _shoeList[_pointer].Length)
                    {
                        current = _shoeList[_pointer].Substring(0, _shoeList[_pointer].Length - trim);
                    }

                    if (_wasFour)
                    {
                        _wasFour = false;
                        string last = _shoeList[_pointer];
                        _shoeList.Clear();
                        _shoeList.Add(last);
                        _shoeList.Add(current);
                        _pointer = _shoeList.Count - 1;
                        break;
                    }

                    _pointer += 1;
                    _shoeList.Add(current);
                    break;
                case "3":
                    if (_shoeList.Count == 0 || _shoeList[_pointer].Length - 1 < int.Parse(arg) ||
                        int.Parse(arg) < 0)
                        return string.Empty;

                    return _shoeList[_pointer][int.Parse(arg)].ToString();
                case "4":
                    _wasFour = true;

                    if (_pointer - 1 <= 0)
                    {
                        _pointer = 0;
                        return _shoeList[_pointer];
                    }

                    _pointer -= 1;
                    return _shoeList[_pointer];
                case "5":
                    if (_pointer + 1 > _shoeList.Count - 1)
                        _pointer = _shoeList.Count - 1;
                    else
                        _pointer++;

                    return _shoeList[_pointer];
                default:
                    return _shoeList[_pointer];

            }

            return _shoeList[_pointer];
        }
        
        public static string BiggerGreater(string input)
        {
            //check that all chars equal
            if (string.IsNullOrEmpty(input.Replace(input[0].ToString(), string.Empty)))
            {
                return string.Empty;
            }

            int l = input.Length;

            if (input.Length == 2 && input[l - 1] < input[l - 2])
                return string.Empty;

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
        
        public static string [] TreeOfLife(int H, int W, int N, string [] tree)
        {
            bool isEven = true;
            int yearCounter = 0;
            char[,] resTree = new char[H, W];
            
            // fill the tree
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    var c = tree[i][j] == '+' ? '1' : tree[i][j];
                    resTree[i, j] = c;
                }
            }
            
            while (N > 0)
            {
                bool shouldDestroy = false;
                // increase
                for (int i = 0; i < H; i++)
                {
                    for (int j = 0; j < W; j++)
                    {
                        // adding 48 because '0' has 48 code
                        char c = resTree[i, j] == '.' ? '1' : (char) (char.GetNumericValue(resTree[i, j]) + 1 + 48);
                        resTree[i, j] = c;
                        
                        if (char.GetNumericValue(c) >= 3)
                            shouldDestroy = true;
                    }
                }

                if (shouldDestroy && !isEven)
                {
                    ClearTree(resTree, H, W);
                }

                isEven = !isEven;
                N--;
            }

            for (int i = 0; i < H; i++)
            {
                string currentLine = string.Empty;
                for (int j = 0; j < W; j++)
                {
                    var c = resTree[i, j] == '.' ? '.' : '+';
                    currentLine = string.Concat(currentLine, c);
                }

                tree[i] = currentLine;
            }

            return tree;
        }

        private static void ClearTree(char[,] tree, int H, int W)
        {
            bool[,] mask = new bool[H, W];

            // fill mask for clearing
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    if (char.GetNumericValue(tree[i, j]) < 3) continue;
                    
                    mask[i, j] = true;
                    // left
                    if (j > 0)
                    {
                        mask[i, j - 1] = true;
                    }
                    // right
                    if (j < W - 1)
                    {
                        mask[i, j + 1] = true;
                    }
                    // up
                    if (i > 0)
                    {
                        mask[i - 1, j] = true;
                    }
                    // down
                    if (i < H - 1)
                    {
                        mask[i + 1, j] = true;
                    }
                }
            }
            
            // clearing
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    if (mask[i, j])
                        tree[i, j] = '.';
                }
            }
        }
    }
}