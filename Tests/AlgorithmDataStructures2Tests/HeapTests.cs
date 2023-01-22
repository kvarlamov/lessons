using System.Collections.Generic;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace AlgorithmDataStructures2Tests
{
    [TestFixture]
    public class HeapTests
    {
        #region MakeHeap

        [Test]
        public void MakeHeap_MaxInTop()
        {
            Heap heap = new Heap()
            {
                HeapArray = new int[15]
            };

            List<int> initial = new List<int>()
            {
                9, 11, 6, 8, 7, 8, 1, 3, 5, 15
            };
            
            heap.MakeHeap(initial.ToArray(), 3);
            
            Assert.That(heap.lastPointer, Is.EqualTo(10));
            Assert.That(heap.HeapArray[0], Is.EqualTo(15));
        }
        
        [Test]
        public void MakeHeap_CorrectOrder()
        {
            Heap heap = new Heap()
            {
                HeapArray = new int[15]
            };

            List<int> initial = new List<int>()
            {
                15, 9, 11, 6, 8, 7, 8, 1, 3, 5 
            };
            
            heap.MakeHeap(initial.ToArray(), 3);
            
            Assert.That(heap.lastPointer, Is.EqualTo(10));
            Assert.That(heap.HeapArray[0], Is.EqualTo(15));
            Assert.That(heap.HeapArray[1], Is.EqualTo(9));
            Assert.That(heap.HeapArray[2], Is.EqualTo(11));
            Assert.That(heap.HeapArray[3], Is.EqualTo(6));
            Assert.That(heap.HeapArray[4], Is.EqualTo(8));
            Assert.That(heap.HeapArray[5], Is.EqualTo(7));
            Assert.That(heap.HeapArray[6], Is.EqualTo(8));
            Assert.That(heap.HeapArray[7], Is.EqualTo(1));
            Assert.That(heap.HeapArray[8], Is.EqualTo(3));
            Assert.That(heap.HeapArray[9], Is.EqualTo(5));
        }

        #endregion
        
        #region Delete

        [Test]
        public void Delete_ArrayNull()
        {
            Heap heap = new Heap();
            
            Assert.That(heap.GetMax(), Is.EqualTo(-1));
        }

        [Test]
        public void Delete_Array_Short1()
        {
            Heap heap = new Heap()
            {
                HeapArray = new int[1]
            };
            
            Assert.That(heap.GetMax(), Is.EqualTo(-1));
        }
        
        [Test]
        public void Delete_Array_Short2()
        {
            Heap heap = new Heap()
            {
                HeapArray = new int[1]
            };

            heap.Add(11);
            
            Assert.That(heap.GetMax(), Is.EqualTo(11));
        }
        
        [Test]
        public void Delete_Array_Short3()
        {
            Heap heap = new Heap()
            {
                HeapArray = new int[2]
            };

            heap.Add(11);
            
            Assert.That(heap.GetMax(), Is.EqualTo(11));
            Assert.That(heap.GetMax(), Is.EqualTo(-1));
        }
        
        [Test]
        public void Delete_Array_Short4()
        {
            Heap heap = new Heap()
            {
                HeapArray = new int[2]
            };

            heap.Add(11);
            heap.Add(9);
            
            Assert.That(heap.GetMax(), Is.EqualTo(11));
            Assert.That(heap.GetMax(), Is.EqualTo(9));
            Assert.That(heap.GetMax(), Is.EqualTo(-1));
        }
        
        [Test]
        public void Delete_Array_Short5()
        {
            Heap heap = new Heap()
            {
                HeapArray = new int[3]
            };

            heap.Add(11);
            heap.Add(9);
            heap.Add(4);
            
            Assert.That(heap.GetMax(), Is.EqualTo(11));
            Assert.That(heap.GetMax(), Is.EqualTo(9));
            Assert.That(heap.GetMax(), Is.EqualTo(4));
            Assert.That(heap.GetMax(), Is.EqualTo(-1));
        }

        [Test]
        public void Delete_ArrayEmpty()
        {
            Heap heap = new Heap()
            {
                HeapArray = new int[15]
            };
            
            Assert.That(heap.GetMax(), Is.EqualTo(-1));
        }

        [Test]
        public void Delete_OnlyRoot()
        {
            Heap heap = new Heap()
            {
                HeapArray = new int[15]
            };

            heap.Add(20);
            
            Assert.That(heap.GetMax(), Is.EqualTo(20));
            Assert.That(heap.lastPointer, Is.EqualTo(0));
        }

        [Test]
        public void Delete_Partly()
        {
            Heap heap = new Heap()
            {
                HeapArray = new int[15]
            };

            heap.Add(15);
            heap.Add(9);
            heap.Add(11);
            heap.Add(6);
            heap.Add(8);
            heap.Add(7);
            heap.Add(8);
            heap.Add(1);
            heap.Add(3);
            heap.Add(5);
            
            Assert.That(heap.GetMax(), Is.EqualTo(15));
            Assert.That(heap.lastPointer, Is.EqualTo(9));
            Assert.That(heap.HeapArray[0], Is.EqualTo(11));
            Assert.That(heap.HeapArray[1], Is.EqualTo(9));
            Assert.That(heap.HeapArray[2], Is.EqualTo(8));
            Assert.That(heap.HeapArray[3], Is.EqualTo(6));
            Assert.That(heap.HeapArray[5], Is.EqualTo(7));
            Assert.That(heap.HeapArray[6], Is.EqualTo(5));
            Assert.That(heap.HeapArray[8], Is.EqualTo(3));
        }

        #endregion

        #region Add
        
        [Test]
        public void Add_EmptyArray_True()
        {
            Heap heap = new Heap()
            {
                HeapArray = new int[15]
            };

            Assert.IsTrue(heap.Add(11));
            
            Assert.That(heap.HeapArray[0], Is.EqualTo(11));
        }

        [Test]
        public void Add_EmptyAndFillAll_True()
        {
            Heap heap = new Heap()
            {
                HeapArray = new int[15]
            };
            
            Assert.IsTrue(heap.Add(11));
            Assert.That(heap.HeapArray[0], Is.EqualTo(11));
            Assert.IsTrue(heap.Add(9));
            Assert.That(heap.HeapArray[1], Is.EqualTo(9));
            Assert.IsTrue(heap.Add(4));
            Assert.That(heap.HeapArray[2], Is.EqualTo(4));
        }

        [Test]
        public void Add_PartlyFilled_True()
        {
            Heap heap = new Heap
            {
                HeapArray = new int[15] {20, 15, 11, 6,9,7,8,1,3,5,0,0,0,0,0}
            };

            heap.lastPointer = 10;
            
            Assert.IsTrue(heap.Add(17));
            Assert.That(heap.HeapArray[10], Is.EqualTo(9));
            Assert.That(heap.HeapArray[4], Is.EqualTo(15));
            Assert.That(heap.HeapArray[9], Is.EqualTo(5));
            Assert.That(heap.HeapArray[1], Is.EqualTo(17));
        }

        [Test]
        public void Add_AllFilled_False()
        {
            Heap heap = new Heap
            {
                HeapArray = new[] {11, 9, 4, 7, 8, 3, 1, 2, 5, 6, 5, 2, 1, 0, 0}
            };

            heap.lastPointer = 15;

            Assert.IsFalse(heap.Add(10));
        }

        #endregion
        
    }
}