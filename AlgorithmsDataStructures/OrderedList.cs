using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Node<T>
    {
        public T value;
        public Node<T> next, prev;

        public Node(T _value)
        {
            value = _value;
            next = null;
            prev = null;
        }
    }

    public class OrderedList<T>
    {
        public Node<T> head, tail;
        private bool _ascending;

        public OrderedList(bool asc)
        {
            head = null;
            tail = null;
            _ascending = asc;
        }

        public int Compare(T v1, T v2)
        {
            int result = 0;
            if(typeof(T) == typeof(String))
            {
                throw new NotImplementedException();
                // версия для лексикографического сравнения строк
            }
            else
            {
                //int only implemented for now
                result = CompareUniversal(v1, v2);
            }
      
            return result;
            // -1 if v1 < v2
            // 0 if v1 == v2
            // +1 if v1 > v2
        }

        public void Add(T value)
        {
            var newNode = new Node<T>(value);

            if (head == null)
            {
                InsertFirst(newNode);
                return;
            }
            
            var current = head;

            while (current.next != null)
            {
                // check that pair of values current and next are equal with insert
                if (
                    GetCompareResult(value, current.value) ||
                    (Compare(current.value, current.next.value) == 0 && Compare(value, current.value) == 0)
                    )
                {
                    current = current.next;
                    continue;
                }

                InsertBefore(current, newNode);

                return;
            }
            
            InsertHeadOrTail(current, newNode);
        }

        public Node<T> Find(T val)
        {
            var list = GetAll();

            return BinarySearch(list, 0, list.Count - 1, val);
            // var current = head;
            //
            // while (current != null)
            // {
            //     if (current.value.Equals(val))
            //         return current;
            //
            //     current = current.next;
            // }
        }

        private Node<T> BinarySearch(List<Node<T>> list, int left, int right, T val)
        {
            if (left > right)
            {
                return null;
            }

            int middle = (left + right) / 2;
            var compare = Compare(val, list[middle].value);
            if (compare == 0)
                return list[middle];

            bool compareStrategy = _ascending ? compare > 0 : compare < 0;
            if (compareStrategy)
                left++;
            else
                right--;

            return BinarySearch(list, left, right, val);
        }

        public void Delete(T val)
        {
            var current = head;

            while (current != null)
            {
                if (!current.value.Equals(val))
                {
                    current = current.next;
                    continue;
                }

                //head case
                if (current.prev == null)
                {
                    RemoveHead(current);
                    return;
                }

                //tail case
                if (current.next == null)
                {
                    tail = current.prev;
                    tail.next = null;
                    return;
                }

                // move pointers
                current.prev.next = current.next;
                current.next.prev = current.prev;
                return;
            }
        }

        

        public void Clear(bool asc)
        {
            _ascending = asc;
            head = null;
            tail = null;
        }

        public int Count()
        {
            int count = 0;
            var current = head;
            while (current != null)
            {
                count++;
                current = current.next;
            }

            return count;
        }

        public List<Node<T>> GetAll() // выдать все элементы упорядоченного 
            // списка в виде стандартного списка
        {
            var r = new List<Node<T>>();
            var node = head;
            
            while(node != null)
            {
                r.Add(node);
                node = node.next;
            }
            return r;
        }
        
        private int CompareUniversal(T v1, T v2)
        {
            if (typeof(T) != typeof(int))
            {
                return 0;
            }

            int a = (int) (object) v1;
            int b = (int) (object) v2;

            return a.CompareTo(b);
        }

        private void InsertFirst(Node<T> newNode)
        {
            head = newNode;
            tail = newNode;
        }

        private void InsertHeadOrTail(Node<T> current, Node<T> newNode)
        {
            if (GetCompareResult(newNode.value,current.value))
            {
                current.next = newNode;
                newNode.prev = current;
                tail = newNode;
                return;
            }

            var tmp = current.prev;
            if (tmp == null)
            {
                InsertBeforeHead(newNode);
                return;                
            }
            
            newNode.next = current;
            current.prev = newNode;
            newNode.prev = tmp;
            tmp.next = newNode;
        }
        
        private void InsertBeforeHead(Node<T> newNode)
        {
            newNode.next = head;
            head.prev = newNode;
            head = newNode;
        }
        
        private void InsertBefore(Node<T> current, Node<T> newNode)
        {
            if (current.prev == null)
            {
                InsertBeforeHead(newNode);
                return;
            }
            
            var tmp = current.prev;
            current.prev = newNode;
            newNode.prev = tmp;
            tmp.next = newNode;
            newNode.next = current;
        }
        
        private bool GetCompareResult(T value, T current)
        {
            int res = Compare(value, current);

            return _ascending ? res >= 0 : res < 0;
        }
        
        private void RemoveHead(Node<T> current)
        {
            head = current.next;

            //there was only one element and now empty
            if (head == null)
            {
                tail = null;
                return;
            }

            head.prev = null;

            //head and point the same
            if (head.next == null)
            {
                tail.prev = null;
            }
        }
    }
}