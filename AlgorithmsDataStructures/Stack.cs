using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Stack<T>
    {
        private List<T> _stack;
        public Stack()
        {
            _stack = new List<T>();
        } 

        public int Size()
        {
            return _stack.Count;
        }

        public T Pop()
        {
            if (Size() == 0)
                return default(T); // null, if empty

            int tailIndex = _stack.Count - 1;
            T item = _stack[tailIndex];
            _stack.RemoveAt(tailIndex);
            return item;
        }
	  
        public void Push(T val)
        {
            _stack.Add(val);
        }

        public T Peek()
        {
            if (Size() == 0)
                return default(T); // null, if empty

            return _stack[_stack.Count - 1];
        }
    }

    public static class StackTasks
    {
        public static bool BraceTask(string s)
        {
            if (s.Length % 2 != 0)
                return false;

            var stack = new System.Collections.Generic.Stack<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    stack.Push(s[i]);
                    continue;
                }
                
                if (stack.Count == 0)
                    return false;

                //if we would have other braces [{, we need to check expected symbol on pop
                stack.Pop();
            }

            return stack.Count == 0;
        }
    }
    
    public class Stack2<T>
    {
        private LinkedList<T> _stack;
        
        public Stack2()
        {
            _stack = new LinkedList<T>();
        } 

        public int Size()
        {
            return _stack.Count;
        }

        public T Pop()
        {
            if (Size() == 0)
                return default(T); // null, if empty

            T item = _stack.First.Value;
            _stack.RemoveFirst();
            return item;
        }
	  
        public void Push(T val)
        {
            _stack.AddFirst(val);
        }

        public T Peek()
        {
            if (Size() == 0)
                return default(T); // null, if empty

            return _stack.First.Value;
        }
    }
}