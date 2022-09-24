using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
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
                        string last =_shoeList[_pointer];
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
                        string last =_shoeList[_pointer];
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
                    if (_shoeList.Count == 0 || _shoeList[_pointer].Length - 1 < int.Parse(arg) || int.Parse(arg) < 0)
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
    }
}