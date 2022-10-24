using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Queue<T>
    {
        private List<T> _queue;
        
        public Queue()
        {
            _queue = new List<T>();
        } 

        public void Enqueue(T item)
        {
            _queue.Insert(0, item);
        }

        public T Dequeue()
        {
            if (Size() == 0)
                return default(T);

            T item = _queue[_queue.Count - 1];
            _queue.RemoveAt(_queue.Count - 1);
            return item;
        }

        public int Size()
        {
            return _queue.Count;
        }

        public List<T> SpinQueue(int n)
        {
            for (int i = n; i > 0; i--)
            {
                T item = Dequeue();
                Enqueue(item);
            }

            return _queue;
        }
    }

    public class TwoStackQueue<T>
    {
        private System.Collections.Generic.Stack<T> _queue;
        private System.Collections.Generic.Stack<T> _helper;
        public TwoStackQueue()
        {
            _queue = new System.Collections.Generic.Stack<T>();
            _helper = new System.Collections.Generic.Stack<T>();
        }
        
        public void Enqueue(T item)
        {
            _queue.Push(item);
        }

        public T Dequeue()
        {
            if (_queue.Count == 0)
                return default(T);
            
            while (_queue.Count != 0)
            {
                _helper.Push(_queue.Pop());
            }

            T result = _helper.Pop();

            while (_helper.Count != 0)
            {
                _queue.Push(_helper.Pop());
            }

            return result;
        }

        public int Size()
        {
            return _queue.Count;
        }
        
        public T[] SpinQueue(int n)
        {
            for (int i = n; i > 0; i--)
            {
                T item = Dequeue();
                Enqueue(item);
            }

            T[] list = new T[_queue.Count];
            for (int i = _queue.Count - 1; i > -1; i--)
            {
                list[i] = Dequeue();
            }

            for (int i = list.Length-1; i > -1; i--)
            {
                Enqueue(list[i]);
            }
            
            return list;
        }
    }
}