using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Deque<T>
    {
        private List<T> _deque;
        public Deque()
        {
            _deque = new List<T>();
        }

        public void AddFront(T item)
        {
            _deque.Add(item);
        }

        public void AddTail(T item)
        {
            _deque.Insert(0, item);
        }

        public T RemoveFront()
        {
            if (Size() == 0)
                return default(T);

            T item = _deque[_deque.Count - 1];
            _deque.RemoveAt(_deque.Count - 1);
            return item;
        }

        public T RemoveTail()
        {
            if (Size() == 0)
                return default(T);

            T item = _deque[0];
            _deque.RemoveAt(0);
            return item;
        }
        
        public int Size()
        {
            return _deque.Count;
        }
    }

    public static class DequeTasks
    {
        public static bool IsPalindrome(string s)
        {
            Deque<char> deque = new Deque<char>();
            string pattern = "[^A-Za-zЁёА-я]";
            s = System.Text.RegularExpressions.Regex.Replace(s, pattern, string.Empty);
            for (int i = 0; i < s.Length; i++)
            {
                deque.AddFront(s[i]);
            }
            int stop = 0;
            if (s.Length % 2 != 0)
                stop = 1;
            
            while (deque.Size() > stop)
            {
                //a
                //121
                //1221
                var first = deque.RemoveFront().ToString();
                var second = deque.RemoveTail().ToString();
                if (!string.Equals(first, second, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }
    }
}