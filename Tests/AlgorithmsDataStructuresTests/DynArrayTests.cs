using System;
using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class DynArrayTests
    {
        [Test]
        public void Insert_1()
        {
            DynArray<int> array = new DynArray<int>();
            FillArray(array);
            
            //act
            array.Insert(2, 1);
            
            //assert
            Assert.AreEqual(2, array.GetItem(1));
            Assert.AreEqual(1, array.GetItem(0));
            Assert.AreEqual(7, array.GetItem(6));
            Assert.AreEqual(7, array.count);
            Assert.AreEqual(16, array.capacity);
        }
        
        [Test]
        public void Insert_0()
        {
            DynArray<int> array = new DynArray<int>();
            FillArrayFullCapacity(array);
            
            //act
            array.Insert(2, 0);
            
            //assert
            Assert.AreEqual(1, array.GetItem(1));
            Assert.AreEqual(2, array.GetItem(0));
            Assert.AreEqual(1, array.GetItem(16));
            Assert.AreEqual(17, array.count);
            Assert.AreEqual(32, array.capacity);
        }
        
        [Test]
        public void Insert_Tail()
        {
            DynArray<int> array = new DynArray<int>();
            FillArray(array);
            
            //act
            array.Insert(8, 6);
            
            //assert
            Assert.AreEqual(1, array.GetItem(0));
            Assert.AreEqual(7, array.GetItem(5));
            Assert.AreEqual(8, array.GetItem(6));
            Assert.AreEqual(7, array.count);
            Assert.AreEqual(16, array.capacity);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(16)]
        public void WrongInsert(int i)
        {
            DynArray<int> array = new DynArray<int>();
            FillArray(array);

            Assert.Throws<IndexOutOfRangeException>(() => array.Insert(1, i));
        }

        [Test]
        public void RemoveWithoutChange_Head()
        {
            DynArray<int> array = new DynArray<int>();
            FillArrayFullCapacity2(array);
            
            Assert.AreEqual(64, array.capacity);
            
            array.Remove(0);
            
            Assert.AreEqual(1, array.GetItem(0));
            Assert.AreEqual(64, array.capacity);
            Assert.AreEqual(32, array.GetItem(32));
            Assert.AreEqual(32, array.count);
        }
        
        [Test]
        public void RemoveWithShrink_Head()
        {
            DynArray<int> array = new DynArray<int>();
            FillArrayFullCapacity2(array);
            
            Assert.AreEqual(64, array.capacity);
            
            array.Remove(0);
            array.Remove(0);
            
            Assert.AreEqual(2, array.GetItem(0));
            Assert.AreEqual(42, array.capacity);
            Assert.AreEqual(32, array.GetItem(31));
            Assert.AreEqual(31, array.count);
        }
        
        [Test]
        public void RemoveWithShrinkAndException_Head()
        {
            DynArray<int> array = new DynArray<int>();
            FillArrayFullCapacity2(array);
            
            Assert.AreEqual(64, array.capacity);
            
            array.Remove(0);
            array.Remove(0);
            Assert.Throws<IndexOutOfRangeException>(() => array.Remove(32));
        }

        private void FillArray(DynArray<int> arr)
        {
            arr.Append(1);
            arr.Append(3);
            arr.Append(4);
            arr.Append(5);
            arr.Append(6);
            arr.Append(7);
        }
        
        private void FillArrayFullCapacity(DynArray<int> arr)
        {
            //16
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
            arr.Append(1);
        }
        
        private void FillArrayFullCapacity2(DynArray<int> arr)
        {
            //33
            for (int i = 0; i < 33; i++)
            {
                arr.Append(i);
            }
        }
    }
}