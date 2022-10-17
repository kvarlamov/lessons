using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Node
    {
        public int value;
        public Node next, prev;

        public Node(int _value) { 
            value = _value; 
            next = null;
            prev = null;
        }
    }

    public class LinkedList2
    {
        public Node head;
        public Node tail;

        public LinkedList2()
        {
            head = null;
            tail = null;
        }

        public void AddInTail(Node _item)
        {
            if (head == null) {
                head = _item;
                head.next = null;
                head.prev = null;
            } else {
                tail.next = _item;
                _item.prev = tail;
            }
            tail = _item;
        }

        public Node Find(int _value)
        {
            Node current = head;
            while (current != null)
            {
                if (current.value.Equals(_value))
                    return current;

                current = current.next;
            }
            
            return null;
        }

        public List<Node> FindAll(int _value)
        {
            List<Node> nodes = new List<Node>();

            Node current = head;
            while (current != null)
            {
                if (current.value.Equals(_value))
                {
                    nodes.Add(current);
                }

                current = current.next;
            }
            
            return nodes;
        }

        public bool Remove(int _value)
        {
            Node current = head;

            while (current != null)
            {
                if (!current.value.Equals(_value))
                {
                    current = current.next;
                    continue;
                }

                //head case
                if (current.prev == null)
                {
                    head = current.next;
                    
                    //there was only one element and now empty
                    if (head == null)
                    {
                        tail = null;
                        return true;
                    }
                    
                    head.prev = null;
                    
                    //head and point the same
                    if (head.next == null)
                    {
                        tail.prev = null;
                    }

                    return true;
                }

                //tail case
                if (current.next == null)
                {
                    tail = current.prev;
                    tail.next = null;
                    return true;
                }

                // move pointers
                current.prev.next = current.next;
                current.next.prev = current.prev;
                return true;
            }

            return false;
        }

        public void RemoveAll(int _value)
        {
            while (Remove(_value))
            {
            }
        }

        public void Clear()
        {
            head = null;
            tail = null;
        }

        public int Count()
        {
            int count = 0;
            Node current = head;
            while (current != null)
            {
                count++;
                current = current.next;
            }

            return count;
        }

        public void InsertAfter(Node _nodeAfter, Node _nodeToInsert)
        {
            if (_nodeAfter == null)
            {
                Enqueue(_nodeToInsert);
                return;
            }

            Node current = head;

            while (current != null)
            {
                if (!current.value.Equals(_nodeAfter.value))
                {
                    current = current.next;
                    continue;
                }

                //next is tail
                if (current.next == null)
                {
                    _nodeToInsert.next = null;
                    tail = _nodeToInsert;
                }
                //next not tail
                else
                {
                    _nodeToInsert.next = current.next;
                    _nodeToInsert.next.prev = _nodeToInsert;
                }
                
                _nodeToInsert.prev = current;
                current.next = _nodeToInsert;
                return;
            }

        }

        private void Enqueue(Node _nodeToInsert)
        {
            if (head == null)
            {
                head = _nodeToInsert;
                head.next = null;
                head.prev = null;
                tail = _nodeToInsert;
            }
            else
            {
                _nodeToInsert.next = head;
                head.prev = _nodeToInsert;
                head = _nodeToInsert;
            }
        }
    }
}