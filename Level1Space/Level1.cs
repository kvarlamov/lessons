using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        private static Stack<string> history = new Stack<string>();
        private static Stack<string> undoStack = new Stack<string>();
        private static List<string> bufer = new List<string>();
        
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
                    if (history.Count > 0 && history.Peek() == "4" && undoStack.Count > 0)
                        undoStack.Clear();
                    current = bufer.Count > 0 ? string.Concat(bufer[bufer.Count - 1], arg) : arg;
                    bufer.Add(current);
                    break;
                case "2":
                    if (history.Count > 0 && history.Peek() == "4" && undoStack.Count > 0)
                        undoStack.Clear();
                    int trim = int.Parse(arg);
                    if (bufer.Count > 0 && trim < bufer[bufer.Count - 1].Length)
                    {
                        current = bufer[bufer.Count - 1].Substring(0, bufer[bufer.Count - 1].Length - trim);
                    }
                    
                    bufer.Add(current);
                    break;
                case "3":
                    history.Push(cmd);
                    if (bufer.Count == 0 || bufer[bufer.Count - 1].Length - 1 < int.Parse(arg) || int.Parse(arg) < 0)
                        return string.Empty;

                    return bufer[bufer.Count - 1][int.Parse(arg)].ToString();
                case "4":
                    if (bufer.Count == 0)
                        return string.Empty;
                    var previousCommand = history.Pop();
                    if (previousCommand == "4")
                    {
                        previousCommand = history.Peek();
                    }

                    history.Push(cmd);
                    
                    if (previousCommand == "4" || previousCommand == "3") 
                        return bufer[bufer.Count - 1];
                    
                    undoStack.Push(bufer[bufer.Count-1]);
                    bufer.RemoveAt(bufer.Count-1);
                    if (bufer.Count == 0)
                        return string.Empty;
                    
                    return bufer[bufer.Count - 1];
                
                case "5":
                    if (undoStack.Count > 0)
                    {
                        bufer.Add(undoStack.Pop());
                    }
                    break;
                case "":
                    break;
            }
            
            history.Push(cmd);
            return bufer[bufer.Count - 1];
        }
    }
}