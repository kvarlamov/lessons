using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class DummyNode : Node
    {
        public DummyNode(int _value) : base(_value)
        {
        }

        public DummyNode() : this(0)
        {
            IsDummy = true;
        }
    }
    
    public class LinkedListDummy
    {
        private Node _head;
        private Node _tail;

        public LinkedListDummy()
        {
            _head = new DummyNode();
            _tail = new DummyNode();
            _head.next = _tail;
            _tail.prev = _head;
        }

        public List<int> GetValuesFromList()
        {
            List<int> list = new List<int>();
            Node curr = _head.next;
            while (!curr.IsDummy)
            {
                list.Add(curr.value);
                curr = curr.next;
            }

            return list;
        }
        
        public void AddInTail(Node _item)
        {
            if (_head.next.IsDummy)
            {
                _head.next = _item;
                _item.prev = _head;
            }
            else
            {
                Node tmp = _tail.prev;
                _item.prev = tmp;
                tmp.next = _item;
            }
            
            _item.next = _tail;
            _tail.prev = _item;
        }
        
        public Node Find(int _value)
        {
            Node current = _head.next;
            while (!current.IsDummy)
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

            Node current = _head.next;
            while (!current.IsDummy)
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
            Node current = _head.next;

            while (!current.IsDummy)
            {
                if (!current.value.Equals(_value))
                {
                    current = current.next;
                    continue;
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
            _head.next = _tail;
            _tail.prev = _head;
        }

        public int Count()
        {
            int count = 0;
            Node current = _head.next;
            while (!current.IsDummy)
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

            Node current = _head.next;

            while (!current.IsDummy)
            {
                if (!current.value.Equals(_nodeAfter.value))
                {
                    current = current.next;
                    continue;
                }

                Node tmp = current.next;
                _nodeToInsert.prev = current;
                _nodeToInsert.next = tmp;
                current.next = _nodeToInsert;
                tmp.prev = _nodeToInsert;
                return;
            }
        }

        private void Enqueue(Node _nodeToInsert)
        {
            if (_head.next.IsDummy)
            {
                _tail.prev = _nodeToInsert;
                _nodeToInsert.next = _tail;
            }
            else
            {
                Node tmp = _head.next;
                tmp.prev = _nodeToInsert;
                _nodeToInsert.next = tmp;
            }
            
            _head.next = _nodeToInsert;
            _nodeToInsert.prev = _head;
        }
    }
}