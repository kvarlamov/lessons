using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static string TheRabbitsFoot(string s, bool encode)
        {
            string s1 = s.Replace(" ", "");
            if (!encode)
                return Decode(s.Split(' '), s1.Length);

            return Encode(s1);
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
    }
}