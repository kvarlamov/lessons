using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    public class NodeLinkedList
    {
        public int value;
        public NodeLinkedList next;
        public NodeLinkedList(int _value) { value = _value; }
    }

    public class LinkedList
    {
        public NodeLinkedList head;
        public NodeLinkedList tail;

        public LinkedList()
        {
            head = null;
            tail = null;
        }

        public void AddInTail(NodeLinkedList _item)
        {
            if (head == null)
                head = _item;
            else
                tail.next = _item;
            tail = _item;
        }

        public NodeLinkedList Find(int _value)
        {
            NodeLinkedList node = head;
            while (node != null)
            {
                if (node.value.Equals(_value))
                    return node;
                node = node.next;
            }
            return null;
        }

        public List<NodeLinkedList> FindAll(int _value)
        {
            List<NodeLinkedList> nodes = new List<NodeLinkedList>();
            NodeLinkedList current = head;
            while (current != null)
            {
                if (current.value.Equals(_value))
                    nodes.Add(current);
                current = current.next;
            }

            return nodes;
        }

        public bool Remove(int _value)
        {
            // list is empty
            if (head == null)
            {
                return false; 
            }

            NodeLinkedList current = head;

            if (current.value.Equals(_value))
            {
                head = current.next;
                if (head == null)
                    tail = null;

                return true;
            }

            NodeLinkedList next = current.next;

            while (next != null)
            {
                if (next.value.Equals(_value))
                {
                    current.next = next.next;
                    if (next.next == null)
                        tail = current;
                    return true;
                }

                current = current.next;
                next = next.next;
            }
            
            // not deleted
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
            NodeLinkedList node = head;
            while (node != null)
            {
                count++;
                node = node.next;
            }

            return count;
        }

        public void InsertAfter(NodeLinkedList _nodeAfter, NodeLinkedList _nodeToInsert)
        {
            //if null push to head
            if (_nodeAfter == null)
            {
                if (head == null)
                {
                    tail = _nodeToInsert;
                    head = _nodeToInsert;
                    return;
                }
                    
                _nodeToInsert.next = head;
                head = _nodeToInsert;
                
                return;
            }

            NodeLinkedList current = head;
            while (current != null)
            {
                if (_nodeAfter.value.Equals(current.value))
                {
                    NodeLinkedList tmp = current.next;
                    if (tmp == null) 
                        tail = _nodeToInsert;
                    current.next = _nodeToInsert;
                    _nodeToInsert.next = tmp;
                    
                    break;
                }

                current = current.next;
            }
        }

        public static List<int> SummOfLinkedLists(LinkedList first, LinkedList second)
        {
            if (first.Count() != second.Count())
                return new List<int>();

            List<int> list = new List<int>();

            var firstCurrent = first.head;
            var secondCurrent = second.head;
            
            while (firstCurrent != null)
            {
                int sum = firstCurrent.value + secondCurrent.value;
                list.Add(sum);
                firstCurrent = firstCurrent.next;
                secondCurrent = secondCurrent.next;
            }

            return list;
        }
    }
}