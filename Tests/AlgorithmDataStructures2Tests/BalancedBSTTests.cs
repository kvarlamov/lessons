using System;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace AlgorithmDataStructures2Tests
{
    [TestFixture]
    public class BalancedBSTTests
    {
        [Test]
        public void EmptyArr()
        {
            Assert.IsNull(sBalancedBST.GenerateBBSTArray(null));
        }

        [Test]
        public void OneElementArray()
        {
            var arrayInitial = new int[] {8};
            var copy = new int[arrayInitial.Length];
            Array.Copy(arrayInitial, copy, arrayInitial.Length);
            
            var res = sBalancedBST.GenerateBBSTArray(copy);
            
            CollectionAssert.AreEqual(arrayInitial, res);
        }

        [Test]
        public void TheeElemArray()
        {
            var arrayInitial = new int[] {8, 4, 12};
            var copy = new int[arrayInitial.Length];
            Array.Copy(arrayInitial, copy, arrayInitial.Length);
            
            var res = sBalancedBST.GenerateBBSTArray(copy);
            
            CollectionAssert.AreEqual(arrayInitial, res);
        }
        
        [Test]
        public void t1()
        {
            var arrayInitial = new int[] {50, 25, 75, 20, 37, 62, 84, 10, 22, 31, 43, 55, 64, 83, 92};
            var copy = new int[arrayInitial.Length];
            Array.Copy(arrayInitial, copy, arrayInitial.Length);

            var res = sBalancedBST.GenerateBBSTArray(copy);
            
            CollectionAssert.AreEqual(arrayInitial, res);
        }

        [Test]
        public void t2()
        {
            var arrayInitial = new int[] {8,4,12,2,6,10,14,1,3,5,7,9,11,13,15};
            var copy = new int[arrayInitial.Length];
            Array.Copy(arrayInitial, copy, arrayInitial.Length);

            var res = sBalancedBST.GenerateBBSTArray(copy);
            
            CollectionAssert.AreEqual(arrayInitial, res);
        }
        
        [Test]
        public void t3()
        {
            var arrayInitial = new int[] {8,4,12,2,6,10,14};
            var copy = new int[arrayInitial.Length];
            Array.Copy(arrayInitial, copy, arrayInitial.Length);

            var res = sBalancedBST.GenerateBBSTArray(copy);
            
            CollectionAssert.AreEqual(arrayInitial, res);
        }
    }
}