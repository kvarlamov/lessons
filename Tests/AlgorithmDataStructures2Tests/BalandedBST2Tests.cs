using System;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace AlgorithmDataStructures2Tests
{
    [TestFixture]
    public class BalandedBST2Tests
    {
        [Test]
        public void Test()
        {
            var arrayInitial = new int[] {50, 25, 75, 20, 37, 62, 84, 10, 22, 31, 43, 55, 64, 83, 92};
            var copy = new int[arrayInitial.Length];
            Array.Copy(arrayInitial, copy, arrayInitial.Length);

            BalancedBST tree = new BalancedBST();
            tree.GenerateTree(copy);
            
            Assert.That(tree.Root.Level, Is.EqualTo(0));
            Assert.That(tree.Root.NodeKey, Is.EqualTo(50));
            Assert.That(tree.Root.LeftChild.Level, Is.EqualTo(1));
            Assert.That(tree.Root.LeftChild.NodeKey, Is.EqualTo(25));
            Assert.That(tree.Root.RightChild.Level, Is.EqualTo(1));
            Assert.That(tree.Root.RightChild.NodeKey, Is.EqualTo(75));
            
            Assert.IsTrue(tree.IsBalanced(tree.Root));

            var nodes = tree.GetNodes();
            foreach (var node in nodes)
            {
                if (node.LeftChild != null)
                    Assert.That(node.LeftChild.NodeKey, Is.LessThan(node.NodeKey));
                if (node.RightChild != null)
                    Assert.That(node.RightChild.NodeKey, Is.GreaterThanOrEqualTo(node.NodeKey));
            }
        }

        [Test]
        public void NotBalanced()
        {
            BalancedBST tree = new BalancedBST();
            var root = new BSTNode(7, null);
            tree.Root = root;
            var child3 = new BSTNode(3, root)
            {
                Level = 1
            };
            
            var child11 = new BSTNode(11, root)
            {
                Level = 1
            };
            var child9 = new BSTNode(9, child11)
            {
                Level = 2
            };
            var child13 = new BSTNode(13, child11)
            {
                Level = 2
            };
            var child12 = new BSTNode(12, child13)
            {
                Level = 3
            };

            root.LeftChild = child3;
            root.RightChild = child11;
            child11.LeftChild = child9;
            child11.RightChild = child13;
            child13.LeftChild = child12;
            
            Assert.IsFalse(tree.IsBalanced(root));
        }
        
        [Test]
        public void Balanced()
        {
            BalancedBST tree = new BalancedBST();
            var root = new BSTNode(7, null);
            tree.Root = root;
            var child3 = new BSTNode(3, root)
            {
                Level = 1
            };
            
            var child11 = new BSTNode(11, root)
            {
                Level = 1
            };
            var child9 = new BSTNode(9, child11)
            {
                Level = 2
            };
            var child13 = new BSTNode(13, child11)
            {
                Level = 2
            };

            root.LeftChild = child3;
            root.RightChild = child11;
            child11.LeftChild = child9;
            child11.RightChild = child13;
            
            Assert.IsTrue(tree.IsBalanced(root));
        }
    }
}