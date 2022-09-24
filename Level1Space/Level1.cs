using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        private static int pointer = 0;
        private static int stopPointer = 0;
        private static List<string> shoeList = new List<string>() {string.Empty};
        private static bool wasFour = false;
        
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
                    current = pointer > 0 ? string.Concat(shoeList[pointer], arg) : arg;

                    if (wasFour)
                    {
                        wasFour = false;
                        string last =shoeList[pointer];
                        shoeList.Clear();
                        shoeList.Add(last);
                        shoeList.Add(current);
                        pointer = shoeList.Count - 1;
                        break;
                    }

                    pointer += 1;
                    shoeList.Add(current);
                    break;
                case "2":
                    int trim = int.Parse(arg);
                    if (pointer > 0 && trim < shoeList[pointer].Length)
                    {
                        current = shoeList[pointer].Substring(0, shoeList[pointer].Length - trim);
                    }

                    if (wasFour)
                    {
                        wasFour = false;
                        string last =shoeList[pointer];
                        shoeList.Clear();
                        shoeList.Add(last);
                        shoeList.Add(current);
                        pointer = shoeList.Count - 1;
                        break;
                    }

                    pointer += 1;
                    shoeList.Add(current);
                    break;
                case "3":
                    if (shoeList.Count == 0 || shoeList[pointer].Length - 1 < int.Parse(arg) || int.Parse(arg) < 0)
                        return string.Empty;
                    
                    return shoeList[pointer][int.Parse(arg)].ToString();
                case "4":
                    wasFour = true;

                    if (pointer - 1 <= 0)
                    {
                        pointer = 0;
                        return shoeList[pointer];
                    }

                    pointer -= 1;
                    return shoeList[pointer];
                case "5":
                    if (pointer + 1 > shoeList.Count - 1)
                        pointer = shoeList.Count - 1;
                    else
                        pointer++;

                    return shoeList[pointer];
                default:
                    return shoeList[pointer];

            }

            return shoeList[pointer];
        }
    }
}