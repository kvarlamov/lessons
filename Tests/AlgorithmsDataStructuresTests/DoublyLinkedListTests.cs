using System.Collections.Generic;
using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class DoublyLinkedList2Tests
    {
        #region Delete

        [Test]
        public void Delete_EmptyList()
        {
            LinkedList2 list = new LinkedList2();

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
            LinkedList2 list = new LinkedList2();
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
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.Remove(2);
            var result = ConvertToList(list);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.head.next);
            Assert.AreEqual(null, list.tail.next);
            Assert.IsNull(list.head.prev);
            Assert.IsNull(list.tail.prev);
        }
        
        [Test]
        public void Delete_TwoElementListRemoveLast_Deleted()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.Remove(3);
            var result = ConvertToList(list);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(2, list.tail.value);
            Assert.AreEqual(null, list.head.next);
            Assert.AreEqual(null, list.tail.next);
            Assert.IsNull(list.head.prev);
            Assert.IsNull(list.tail.prev);
        }
        
        [Test]
        public void Delete_ValueInHead_Deleted()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(2));

            list.Remove(2);
            var result = ConvertToList(list);
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(3, list.head.value);
            Assert.AreEqual(2, list.tail.value);
            Assert.IsNull(list.head.prev);
        }
        
        [Test]
        public void Delete_ValueInTail_Deleted()
        {
            LinkedList2 list = new LinkedList2();
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
        
        [Test]
        public void Delete_Middle_Deleted()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(4));

            list.Remove(3);
            var result = ConvertToList(list);
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(4, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
            Assert.AreEqual(2, list.tail.prev.value);
        }
        
        [Test]
        public void Delete_Middle2_Deleted()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(4));
            list.AddInTail(new Node(5));

            list.Remove(4);
            var result = ConvertToList(list);
            
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(5, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
            Assert.AreEqual(3, list.tail.prev.value);
        }

        #endregion

        #region DeleteAll

        [Test]
        public void DeleteAll_EmptyList()
        {
            LinkedList2 list = new LinkedList2();

            list.RemoveAll(2);
            var result = ConvertToList(list);
            
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(null, list.head);
            Assert.AreEqual(null, list.tail);
        }
        
        [Test]
        public void DeleteAll_OneElementList_Deleted()
        {
            LinkedList2 list = new LinkedList2();
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
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.RemoveAll(2);
            var result = ConvertToList(list);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.head.next);
            Assert.AreEqual(null, list.tail.next);
            Assert.IsNull(list.head.prev);
            Assert.IsNull(list.tail.prev);
        }
        
        [Test]
        public void DeleteAll_TwoElementListRemoveLast_Deleted()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.RemoveAll(3);
            var result = ConvertToList(list);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(2, list.tail.value);
            Assert.AreEqual(null, list.head.next);
            Assert.AreEqual(null, list.tail.next);
            Assert.IsNull(list.head.prev);
            Assert.IsNull(list.tail.prev);
        }
        
        [Test]
        public void DeleteAll_ValueInHead_Deleted()
        {
            LinkedList2 list = new LinkedList2();
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
            Assert.IsNull(list.head.prev);
            Assert.IsNull(list.tail.prev);
        }
        
        [Test]
        public void DeleteAll_ValueInTail_Deleted()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(4));

            list.RemoveAll(4);
            var result = ConvertToList(list);
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
            Assert.IsNull(list.head.prev);
            Assert.AreEqual(3, list.head.next.value);
            Assert.AreEqual(2, list.tail.prev.value);
        }
        
        [Test]
        public void DeleteAll_TwoValues_Deleted()
        {
            LinkedList2 list = new LinkedList2();
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
            LinkedList2 list = new LinkedList2();

            list.Clear();
            var result = ConvertToList(list);
            
            Assert.IsNull(list.head);
            Assert.IsNull(list.tail);
        }
        
        [Test]
        public void Clear_Single_Cleared()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));

            list.Clear();
            var result = ConvertToList(list);
            
            Assert.IsNull(list.head);
            Assert.IsNull(list.tail);
        }
        
        [Test]
        public void Clear_NotEmpty_Cleared()
        {
            LinkedList2 list = new LinkedList2();
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
            LinkedList2 list = new LinkedList2();

            var result = list.FindAll(5);
            
            Assert.AreEqual(0, result.Count);
            Assert.IsNull(list.head);
            Assert.IsNull(list.tail);
        }
        
        [Test]
        public void FindAll_OneElement()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));

            var result = list.FindAll(2);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(2, list.tail.value);
        }
        
        [Test]
        public void FindAll_TwoElements()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(2));

            var result = list.FindAll(2);
            
            Assert.AreEqual(2, result.Count);
        }
        
        [Test]
        public void FindAll_ManyElements()
        {
            LinkedList2 list = new LinkedList2();
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
            LinkedList2 list = new LinkedList2();

            var result = list.Count();
            
            Assert.AreEqual(0, result);
        }
        
        [Test]
        public void Count_OneElement()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));

            var result = list.Count();
            
            Assert.AreEqual(1, result);
        }
        
        [Test]
        public void Count_TwoElements()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(2));

            var result = list.Count();
            
            Assert.AreEqual(2, result);
        }
        
        [Test]
        public void Count_ManyElements()
        {
            LinkedList2 list = new LinkedList2();
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
            LinkedList2 list = new LinkedList2();

            list.InsertAfter(new Node(2), new Node(1));
            var result = ConvertToList(list);
            
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(null, list.head);
            Assert.AreEqual(null, list.tail);
        }
        
        [Test]
        public void Enqueue_EmptyList2()
        {
            LinkedList2 list = new LinkedList2();

            list.InsertAfter(null, new Node(1));
            var result = ConvertToList(list);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, list.head.value);
            Assert.AreEqual(1, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
            Assert.IsNull(list.head.next);
            Assert.IsNull(list.head.prev);
            Assert.IsNull(list.tail.prev);
        }
        
        [Test]
        public void InsertAfter_OneElementList()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(1));

            list.InsertAfter(new Node(1), new Node(2));
            var result = ConvertToList(list);
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, list.head.value);
            Assert.AreEqual(2, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
            Assert.AreEqual(2, list.head.next.value);
            Assert.AreEqual(1, list.tail.prev.value);
        }
        
        [Test]
        public void Enqueue_OneElementList()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(1));

            list.InsertAfter(null, new Node(2));
            var result = ConvertToList(list);
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(1, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
            Assert.AreEqual(1, list.head.next.value);
            Assert.AreEqual(2, list.tail.prev.value);
        }
        
        [Test]
        public void InsertAfter_ToTail()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.InsertAfter(new Node(3), new Node(4));
            var result = ConvertToList(list);
            
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(4, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
            Assert.AreEqual(3, list.tail.prev.value);
            Assert.AreEqual(2, list.tail.prev.prev.value);
        }
        
        [Test]
        public void InsertAfter_TwoElementListCenter_Deleted()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.InsertAfter(new Node(2), new Node(4));
            var result = ConvertToList(list);
            
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(4, list.head.next.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
            Assert.AreEqual(4, list.tail.prev.value);
            Assert.AreEqual(2, list.tail.prev.prev.value);
        }

        #endregion

        private List<Node> ConvertToList(LinkedList2 list)
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