using System.Linq;
using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class DummyLinkedListTests
    {
        #region Delete

        [Test]
        public void DummyDelete_EmptyList()
        {
            LinkedListDummy list = new LinkedListDummy();

            var res = list.Remove(2);
            var result = list.GetValuesFromList();
            
            Assert.IsFalse(res);
            Assert.AreEqual(0, result.Count);
        }
        
        [Test]
        public void DummyDelete_OneElementList_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));

            list.Remove(2);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(0, result.Count);
        }
        
        [Test]
        public void DummyDelete_TwoElementListRemoveFirst_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.Remove(2);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, result[0]);
        }
        
        [Test]
        public void DummyDelete_TwoElementListRemoveLast_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.Remove(3);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, result[0]);
        }
        
        [Test]
        public void DummyDelete_ValueInHead_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(2));

            list.Remove(2);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(3, result[0]);
            Assert.AreEqual(2, result[1]);
        }
        
        [Test]
        public void DummyDelete_ValueInTail_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(4));

            list.Remove(4);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(3, result[1]);
        }
        
        [Test]
        public void DummyDelete_Middle_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(4));

            list.Remove(3);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(4, result[1]);
        }
        
        [Test]
        public void DummyDelete_Middle2_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(4));
            list.AddInTail(new Node(5));

            list.Remove(4);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(5, result.Last());
            Assert.AreEqual(3, result[result.Count-2]);
        }

        #endregion
        
        #region DeleteAll

        [Test]
        public void DeleteAll_EmptyList()
        {
            LinkedListDummy list = new LinkedListDummy();

            list.RemoveAll(2);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(0, result.Count);
        }
        
        [Test]
        public void DeleteAll_OneElementList_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));

            list.RemoveAll(2);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(0, result.Count);
        }
        
        [Test]
        public void DeleteAll_TwoElementListRemoveFirst_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.RemoveAll(2);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, result[0]);
        }
        
        [Test]
        public void DeleteAll_TwoElementListRemoveLast_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.RemoveAll(3);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, result[0]);
        }
        
        [Test]
        public void DeleteAll_ValueInHead_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(2));

            list.RemoveAll(2);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, result[0]);
        }
        
        [Test]
        public void DeleteAll_ValueInTail_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(4));

            list.RemoveAll(4);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(3, result[1]);
        }
        
        [Test]
        public void DeleteAll_TwoValues_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(4));
            list.AddInTail(new Node(4));
            list.AddInTail(new Node(5));

            list.RemoveAll(4);
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(5, result[1]);
        }

        #endregion

        #region Clear

        [Test]
        public void Clear_Empty_Cleared()
        {
            LinkedListDummy list = new LinkedListDummy();

            list.Clear();
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(0, list.Count());
        }
        
        [Test]
        public void Clear_Single_Cleared()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));

            list.Clear();
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(0, list.Count());
        }
        
        [Test]
        public void Clear_NotEmpty_Cleared()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(4));
            list.AddInTail(new Node(4));
            list.AddInTail(new Node(5));

            list.Clear();
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(0, list.Count());
        }

        #endregion

        #region FindAll

        [Test]
        public void FindAll_Empty()
        {
            LinkedListDummy list = new LinkedListDummy();

            var result = list.FindAll(5);
            
            Assert.AreEqual(0, result.Count);
        }
        
        [Test]
        public void FindAll_OneElement()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));

            var result = list.FindAll(2);
            
            Assert.AreEqual(1, result.Count);
        }
        
        [Test]
        public void FindAll_TwoElements()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(2));

            var result = list.FindAll(2);
            
            Assert.AreEqual(2, result.Count);
        }
        
        [Test]
        public void FindAll_ManyElements()
        {
            LinkedListDummy list = new LinkedListDummy();
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
            LinkedListDummy list = new LinkedListDummy();

            var result = list.Count();
            
            Assert.AreEqual(0, result);
        }
        
        [Test]
        public void Count_OneElement()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));

            var result = list.Count();
            
            Assert.AreEqual(1, result);
        }
        
        [Test]
        public void Count_TwoElements()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(2));

            var result = list.Count();
            
            Assert.AreEqual(2, result);
        }
        
        [Test]
        public void Count_ManyElements()
        {
            LinkedListDummy list = new LinkedListDummy();
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
            LinkedListDummy list = new LinkedListDummy();

            list.InsertAfter(new Node(2), new Node(1));
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(0, result.Count);
        }
        
        [Test]
        public void Enqueue_EmptyList2()
        {
            LinkedListDummy list = new LinkedListDummy();

            list.InsertAfter(null, new Node(1));
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0]);
        }
        
        [Test]
        public void InsertAfter_OneElementList()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(1));

            list.InsertAfter(new Node(1), new Node(2));
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
        }
        
        [Test]
        public void Enqueue_OneElementList()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(1));

            list.InsertAfter(null, new Node(2));
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(1, result[1]);
        }
        
        [Test]
        public void InsertAfter_ToTail()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.InsertAfter(new Node(3), new Node(4));
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(4, result[2]);
            Assert.AreEqual(3, result[1]);
        }
        
        [Test]
        public void InsertAfter_TwoElementListCenter_Deleted()
        {
            LinkedListDummy list = new LinkedListDummy();
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));

            list.InsertAfter(new Node(2), new Node(4));
            var result = list.GetValuesFromList();
            
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(4, result[1]);
            Assert.AreEqual(3, result[2]);
        }

        #endregion
    }
}