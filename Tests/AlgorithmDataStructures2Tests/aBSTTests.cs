using System.Collections.Generic;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace AlgorithmDataStructures2Tests
{
    [TestFixture]
    public class aBSTTests
    {
        #region Find

        [Test]
        public void FindInEmptyTree()
        {
            var tree = new aBST(0);
            
            Assert.IsNull(tree.FindKeyIndex(1));
        }
        
        [Test]
        public void FindOnlyOneTree()
        {
            var tree = new aBST(0);
            tree.Tree[0] = 1;
            
            Assert.That(tree.FindKeyIndex(1), Is.EqualTo(0));
        }

        [Test]
        [TestCase(100)]
        [TestCase(49)]
        [TestCase(54)]
        [TestCase(30)]
        public void NotFind(int key)
        {
            var tree = new aBST(3);
            FillTree(tree.Tree);

            var res = tree.FindKeyIndex(key);
            
            Assert.IsNull(res);
        }

        [Test]
        [TestCase(50, 0)]
        [TestCase(25, 1)]
        [TestCase(37,4)]
        [TestCase(75,2)]
        [TestCase(62,5)]
        [TestCase(84,6)]
        [TestCase(31,9)]
        [TestCase(43,10)]
        [TestCase(55,11)]
        [TestCase(92,14)]
        public void FindNormal(int key, int expectedIndex)
        {
            var tree = new aBST(3);
            FillTree(tree.Tree);

            var resultIndex = tree.FindKeyIndex(key);
            Assert.That(resultIndex, Is.EqualTo(expectedIndex));
        }

        [Test]
        [TestCase(24, -3)]
        [TestCase(64, -12)]
        [TestCase(80, -13)]
        public void FindEmptySlot(int key, int expectedIndex)
        {
            var tree = new aBST(3);
            FillTree(tree.Tree);

            var resultIndex = tree.FindKeyIndex(key);
            Assert.That(resultIndex, Is.EqualTo(expectedIndex));
        }

        #endregion

        #region Add

        [Test]
        public void AddEmpty()
        {
            
        }

        [Test]
        [TestCase(50, 0)]
        [TestCase(25, 1)]
        [TestCase(37,4)]
        [TestCase(75,2)]
        [TestCase(62,5)]
        [TestCase(84,6)]
        [TestCase(31,9)]
        [TestCase(43,10)]
        [TestCase(55,11)]
        [TestCase(92,14)]
        public void AddExisting(int key, int index)
        {
            var tree = new aBST(3);
            FillTree(tree.Tree);

            var res = tree.AddKey(key);
            
            Assert.That(res, Is.EqualTo(index));
        }

        [Test]
        [TestCase(24, 3)]
        [TestCase(64, 12)]
        [TestCase(80, 13)]
        public void AddNormal(int key, int index)
        {
            var tree = new aBST(3);
            FillTree(tree.Tree);

            var res = tree.AddKey(key);
            
            Assert.That(res, Is.EqualTo(index));
        }

        #endregion
        
        private void FillTree(int?[] tree)
        {
            var list = new List<int?>()
            {
                50, 25, 75, null, 37, 62, 84, null, null, 31, 43, 55, null, null, 92
            };

            for (int i = 0; i < list.Count; i++)
            {
                tree[i] = list[i];
            }
        }
    }
}