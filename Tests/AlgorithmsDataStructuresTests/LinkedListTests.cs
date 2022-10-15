using System.Collections.Generic;
using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class LinkedListTests
    {
        #region Delete

        [Test]
        public void Delete_EmptyList()
        {
            LinkedList list = new LinkedList();

            var res = list.Remove(2);
            var result = ConvertToList(list);
            
            Assert.IsFalse(res);
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(null, list.head);
            Assert.AreEqual(null, list.tail);
        }
        
        [Test]
        public void Delete_OneElementList_Deleted()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));

            list.Remove(2);
            var result = ConvertToList(list);
            
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(null, list.head);
            Assert.AreEqual(null, list.tail);
        }
        
        [Test]
        public void Delete_TwoElementListRemoveFirst_Deleted()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.Remove(2);
            var result = ConvertToList(list);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.head.next);
            Assert.AreEqual(null, list.tail.next);
        }
        
        [Test]
        public void Delete_TwoElementListRemoveLast_Deleted()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.Remove(3);
            var result = ConvertToList(list);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(2, list.tail.value);
            Assert.AreEqual(null, list.head.next);
            Assert.AreEqual(null, list.tail.next);
        }
        
        [Test]
        public void Delete_ValueInHead_Deleted()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(2));

            list.Remove(2);
            var result = ConvertToList(list);
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(3, list.head.value);
            Assert.AreEqual(2, list.tail.value);
        }
        
        [Test]
        public void Delete_ValueInTail_Deleted()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(4));

            list.Remove(4);
            var result = ConvertToList(list);
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
        }

        #endregion

        #region DeleteAll

        [Test]
        public void DeleteAll_EmptyList()
        {
            LinkedList list = new LinkedList();

            list.RemoveAll(2);
            var result = ConvertToList(list);
            
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(null, list.head);
            Assert.AreEqual(null, list.tail);
        }
        
        [Test]
        public void DeleteAll_OneElementList_Deleted()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));

            list.RemoveAll(2);
            var result = ConvertToList(list);
            
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(null, list.head);
            Assert.AreEqual(null, list.tail);
        }
        
        [Test]
        public void DeleteAll_TwoElementListRemoveFirst_Deleted()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.RemoveAll(2);
            var result = ConvertToList(list);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.head.next);
            Assert.AreEqual(null, list.tail.next);
        }
        
        [Test]
        public void DeleteAll_TwoElementListRemoveLast_Deleted()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.RemoveAll(3);
            var result = ConvertToList(list);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(2, list.tail.value);
            Assert.AreEqual(null, list.head.next);
            Assert.AreEqual(null, list.tail.next);
        }
        
        [Test]
        public void DeleteAll_ValueInHead_Deleted()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(2));

            list.RemoveAll(2);
            var result = ConvertToList(list);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.head.next);
            Assert.AreEqual(null, list.tail.next);
        }
        
        [Test]
        public void DeleteAll_ValueInTail_Deleted()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(4));

            list.RemoveAll(4);
            var result = ConvertToList(list);
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
        }
        
        [Test]
        public void DeleteAll_TwoValues_Deleted()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(4));
            list.AddInTail(new Node(4));
            list.AddInTail(new Node(5));

            list.RemoveAll(4);
            var result = ConvertToList(list);
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(5, list.tail.value);
            Assert.AreEqual(5, list.head.next.value);
            Assert.AreEqual(null, list.tail.next);
        }

        #endregion

        #region Clear

        [Test]
        public void Clear_Empty_Cleared()
        {
            LinkedList list = new LinkedList();

            list.Clear();
            var result = ConvertToList(list);
            
            Assert.IsNull(list.head);
            Assert.IsNull(list.tail);
        }
        
        [Test]
        public void Clear_Single_Cleared()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));

            list.Clear();
            var result = ConvertToList(list);
            
            Assert.IsNull(list.head);
            Assert.IsNull(list.tail);
        }
        
        [Test]
        public void Clear_NotEmpty_Cleared()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(4));
            list.AddInTail(new Node(4));
            list.AddInTail(new Node(5));

            list.Clear();
            var result = ConvertToList(list);
            
            Assert.IsNull(list.head);
            Assert.IsNull(list.tail);
        }

        #endregion

        #region FindAll

        [Test]
        public void FindAll_Empty()
        {
            LinkedList list = new LinkedList();

            var result = list.FindAll(5);
            
            Assert.AreEqual(0, result.Count);
            Assert.IsNull(list.head);
            Assert.IsNull(list.tail);
        }
        
        [Test]
        public void FindAll_OneElement()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));

            var result = list.FindAll(2);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(2, list.tail.value);
        }
        
        [Test]
        public void FindAll_TwoElements()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(2));

            var result = list.FindAll(2);
            
            Assert.AreEqual(2, result.Count);
        }
        
        [Test]
        public void FindAll_ManyElements()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(4));
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(2));

            var result = list.FindAll(2);
            
            Assert.AreEqual(3, result.Count);
        }

        #endregion

        #region count

        [Test]
        public void Count_Empty()
        {
            LinkedList list = new LinkedList();

            var result = list.Count();
            
            Assert.AreEqual(0, result);
        }
        
        [Test]
        public void Count_OneElement()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));

            var result = list.Count();
            
            Assert.AreEqual(1, result);
        }
        
        [Test]
        public void Count_TwoElements()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(2));

            var result = list.Count();
            
            Assert.AreEqual(2, result);
        }
        
        [Test]
        public void Count_ManyElements()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(4));
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(2));

            var result = list.Count();
            
            Assert.AreEqual(5, result);
        }

        #endregion

        #region InsertAfter

        [Test]
        public void InsertAfter_EmptyList()
        {
            LinkedList list = new LinkedList();

            list.InsertAfter(new Node(2), new Node(1));
            var result = ConvertToList(list);
            
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(null, list.head);
            Assert.AreEqual(null, list.tail);
        }
        
        [Test]
        public void InsertAfter_EmptyList2()
        {
            LinkedList list = new LinkedList();

            list.InsertAfter(null, new Node(1));
            var result = ConvertToList(list);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, list.head.value);
            Assert.AreEqual(1, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
        }
        
        [Test]
        public void InsertAfter_OneElementList()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(1));

            list.InsertAfter(new Node(1), new Node(2));
            var result = ConvertToList(list);
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, list.head.value);
            Assert.AreEqual(2, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
        }
        
        [Test]
        public void InsertAfter_TwoElementList()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.InsertAfter(new Node(3), new Node(4));
            var result = ConvertToList(list);
            
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(4, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
        }
        
        [Test]
        public void InsertAfter_TwoElementListCenter_Deleted()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.InsertAfter(new Node(2), new Node(4));
            var result = ConvertToList(list);
            
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
        }

        #endregion

        [Test]
        public void SumOfLinkedList_NotEqual()
        {
            LinkedList first = new LinkedList();
            first.AddInTail(new Node(2));
            first.AddInTail(new Node(3));
            LinkedList second = new LinkedList();
            second.AddInTail(new Node(3));

            var result = LinkedList.SummOfLinkedLists(first, second);
            
            Assert.AreEqual(0, result.Count);
        }
        
        [Test]
        public void SumOfLinkedList_Equal()
        {
            LinkedList first = new LinkedList();
            first.AddInTail(new Node(2));
            first.AddInTail(new Node(3));
            LinkedList second = new LinkedList();
            second.AddInTail(new Node(3));
            second.AddInTail(new Node(2));

            var result = LinkedList.SummOfLinkedLists(first, second);
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(5, result[0]);
            Assert.AreEqual(5, result[1]);
        }

        private List<Node> ConvertToList(LinkedList list)
        {
            var result = new List<Node>();
            
            Node node = list.head;
            while (node != null)
            {
                result.Add(node);
                node = node.next;
            }

            return result;
        }
    }
}