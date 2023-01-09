using System.Net.NetworkInformation;
using System.Reflection;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace AlgorithmDataStructures2Tests
{
    [TestFixture]
    public class BstTests
    {
        #region Find

        [Test]
        public void NotFoundLeft()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;
            
            // Act
            var result = tree.FindNodeByKey(-1);
            
            // Assert
            Assert.IsFalse(result.NodeHasKey);
            Assert.IsTrue(result.ToLeft);
            Assert.That(result.Node.NodeKey, Is.EqualTo(1));
            
        }

        [Test]
        public void FoundStrange()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child11 = new BSTNode<int>(11, 11, child10);
            root.RightChild = child12;
            child12.LeftChild = child10;
            child10.RightChild = child11;
            
            // Act
            var result = tree.FindNodeByKey(11);
            
            Assert.IsTrue(result.NodeHasKey);
            Assert.That(result.Node.NodeValue, Is.EqualTo(11));
        }
        
        [Test]
        public void FoundStrangeNotFound()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child11 = new BSTNode<int>(15, 15, child10);
            root.RightChild = child12;
            child12.LeftChild = child10;
            child10.RightChild = child11;
            
            // Act
            var result = tree.FindNodeByKey(11);
            
            Assert.IsFalse(result.NodeHasKey);
            Assert.IsTrue(result.ToLeft);
        }

        [Test]
        public void NotFoundRight()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;
            
            // Act
            var result = tree.FindNodeByKey(16);
            
            // Assert
            Assert.IsFalse(result.NodeHasKey);
            Assert.IsFalse(result.ToLeft);
            Assert.That(result.Node.NodeKey, Is.EqualTo(15));
        }

        [Test]
        [TestCase(8)]
        [TestCase(4)]
        [TestCase(2)]
        [TestCase(6)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(12)]
        [TestCase(10)]
        [TestCase(9)]
        [TestCase(11)]
        [TestCase(14)]
        [TestCase(13)]
        [TestCase(15)]
        public void FindNormal(int key)
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;
            
            // Act
            var result = tree.FindNodeByKey(key);
            
            // Assert
            Assert.IsTrue(result.NodeHasKey);
            Assert.That(result.Node.NodeValue, Is.EqualTo(key));
        }

        [Test]
        public void NotFound()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(null);

            var result = tree.FindNodeByKey(5);
            
            Assert.IsNull(result.Node);
        }
        
        [Test]
        public void NotFoundLeftroot()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);

            var result = tree.FindNodeByKey(4);
            
            // Assert
            Assert.IsFalse(result.NodeHasKey);
            Assert.IsTrue(result.ToLeft);
            Assert.That(result.Node.NodeKey, Is.EqualTo(8));
        }
        
        [Test]
        public void NotFoundRightroot()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);

            var result = tree.FindNodeByKey(10);
            
            // Assert
            Assert.IsFalse(result.NodeHasKey);
            Assert.IsFalse(result.ToLeft);
            Assert.That(result.Node.NodeKey, Is.EqualTo(8));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(6)]
        public void FoundOther1(int key)
        {
            // Arrange
            var root = new BSTNode<int>(6, 6, null);
            var tree = new BST<int>(root);
            var child3 = new BSTNode<int>(3, 3, root); 
            var child2 = new BSTNode<int>(2, 2, child3);
            var child1 = new BSTNode<int>(1, 1, child2);
            root.LeftChild = child3;
            child3.LeftChild = child2;
            child2.LeftChild = child1;

            var res = tree.FindNodeByKey(key);
            
            Assert.IsTrue(res.NodeHasKey);
        }

        [Test]
        [TestCase(6)]
        [TestCase(2)]
        [TestCase(8)]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(9)]
        public void FoundAnother1(int key)
        {
            // Arrange
            var root = new BSTNode<int>(6, 6, null);
            var tree = new BST<int>(root);
            var child2 = new BSTNode<int>(2, 2, root);
            var child8 = new BSTNode<int>(8, 8, root);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child4 = new BSTNode<int>(4, 4, child2);
            var child5 = new BSTNode<int>(5, 5, child4);
            var child7 = new BSTNode<int>(7, 7, child8);
            var child9 = new BSTNode<int>(9, 9, child8);
            root.LeftChild = child2;
            root.RightChild = child8;
            child2.LeftChild = child1;
            child2.RightChild = child4;
            child4.RightChild = child5;
            child8.LeftChild = child7;
            child8.RightChild = child9;

            var res = tree.FindNodeByKey(key);
            
            Assert.IsTrue(res.NodeHasKey);
        }

        [Test]
        [TestCase(5)]
        [TestCase(2)]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(3)]
        public void CrazyTreeFind(int key)
        {
            // Arrange
            var root = new BSTNode<int>(5, 5, null);
            var tree = new BST<int>(root);
            var child2 = new BSTNode<int>(2, 2, root);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child4 = new BSTNode<int>(4, 4, child2);
            var child3 = new BSTNode<int>(3, 3, child4);
            var child8 = new BSTNode<int>(8, 8, child4);
            root.LeftChild = child2;
            child2.LeftChild = child1;
            child2.RightChild = child4;
            child4.LeftChild = child3;
            child4.RightChild = child8;
            
            Assert.IsTrue(tree.FindNodeByKey(key).NodeHasKey);
        }

        #endregion

        #region Add

        [Test]
        public void AddEmpty()
        {
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(null);

            var res = tree.AddKeyValue(8,8);
            
            Assert.IsTrue(res);
            var added = tree.FindNodeByKey(8);
            Assert.IsTrue(added.NodeHasKey);
            Assert.That(added.Node.NodeValue, Is.EqualTo(8));
        }

        [Test]
        public void Add_OnlyRoot_Left()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);

            var result = tree.AddKeyValue(4, 4);
            
            Assert.IsTrue(result);
            var newNode = tree.FindNodeByKey(4);
            
            Assert.IsNotNull(newNode.Node);
            Assert.That(newNode.Node.Parent.NodeValue, Is.EqualTo(8));
            Assert.That(newNode.Node.Parent.LeftChild.NodeValue, Is.EqualTo(4));
        }
        
        [Test]
        public void Add_OnlyRoot_Right()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);

            var result = tree.AddKeyValue(12, 12);
            
            Assert.IsTrue(result);
            var newNode = tree.FindNodeByKey(12);
            
            Assert.IsNotNull(newNode.Node);
            Assert.That(newNode.Node.Parent.NodeValue, Is.EqualTo(8));
            Assert.That(newNode.Node.Parent.RightChild.NodeValue, Is.EqualTo(12));
        }

        [Test]
        [TestCase(8)]
        [TestCase(4)]
        [TestCase(2)]
        [TestCase(6)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(12)]
        [TestCase(10)]
        [TestCase(9)]
        [TestCase(11)]
        [TestCase(14)]
        [TestCase(13)]
        [TestCase(15)]
        public void Add_ExistingKey(int key)
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;
            
            //act
            var res = tree.AddKeyValue(key, key);
            
            //assert
            Assert.IsFalse(res);
        }

        #endregion

        #region MaxMin

        [Test]
        public void GetMin_OnlyRoot()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);

            var min = tree.FinMinMax(root, false);
            
            Assert.IsNotNull(min);
            Assert.That(min.NodeValue, Is.EqualTo(8));
        }

        [Test]
        public void GetMax_OnlyRoot()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);

            var min = tree.FinMinMax(root, true);
            
            Assert.IsNotNull(min);
            Assert.That(min.NodeValue, Is.EqualTo(8));
        }

        [Test]
        public void GetMin()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;

            var min = tree.FinMinMax(root, false);
            
            Assert.That(min.NodeValue, Is.EqualTo(1));
        }

        [Test]
        public void GetMax()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;

            var min = tree.FinMinMax(root, true);
            
            Assert.That(min.NodeValue, Is.EqualTo(15));
        }

        [Test]
        public void Min_SubTree()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;

            var min = tree.FinMinMax(child12, false);
            
            Assert.That(min.NodeValue, Is.EqualTo(9));
        }
        
        [Test]
        public void Min_SubTree2()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;

            var min = tree.FinMinMax(child9, false);
            
            Assert.That(min.NodeValue, Is.EqualTo(9));
        }
        
        [Test]
        public void Min_SubTree3()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;

            var min = tree.FinMinMax(child12, true);
            
            Assert.That(min.NodeValue, Is.EqualTo(15));
        }
        
        [Test]
        public void Min_SubTree4()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;

            var min = tree.FinMinMax(child10, true);
            
            Assert.That(min.NodeValue, Is.EqualTo(11));
        }

        #endregion

        #region Delete

        [Test]
        public void Delete_EmptyTree()
        {
            var tree = new BST<int>(null);

            var res = tree.DeleteNodeByKey(4);
            
            Assert.IsFalse(res);
        }
        
        [Test]
        public void Delete_RootTree()
        {
            var root = new BSTNode<int>(8, 8, null); 
            var tree = new BST<int>(root);

            var res = tree.DeleteNodeByKey(8);
            
            Assert.IsTrue(res);
            Assert.That(tree.Count(), Is.EqualTo(0));
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(9)]
        [TestCase(13)]
        public void Delete_LeafLeft(int key)
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;
            
            //Act
            var res = tree.DeleteNodeByKey(key);
            
            Assert.IsTrue(res);
            var deleted = tree.FindNodeByKey(key);
            
            Assert.IsFalse(deleted.NodeHasKey);
        }

        [Test]
        public void Delete_LeafLeftOne()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            root.LeftChild = child4;

            var res = tree.DeleteNodeByKey(4);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(4);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.IsNull(root.LeftChild);
        }
        
        [Test]
        public void Delete_LeafRightOne()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child12 = new BSTNode<int>(12, 12, root);
            root.RightChild = child12;
            
            var res = tree.DeleteNodeByKey(12);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(12);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.IsNull(root.RightChild);
        }

        [Test]
        [TestCase(3)]
        [TestCase(7)]
        [TestCase(11)]
        [TestCase(15)]
        public void Delete_LeafRight(int key)
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;
            
            //Act
            var res = tree.DeleteNodeByKey(key);
            
            Assert.IsTrue(res);
            var deleted = tree.FindNodeByKey(key);
            
            Assert.IsFalse(deleted.NodeHasKey);
        }

        [Test]
        public void Delete_LeftNode()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;
            
            // act
            var res = tree.DeleteNodeByKey(4);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(4);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.That(root.LeftChild.NodeKey, Is.EqualTo(5));
            Assert.That(child5.LeftChild.NodeKey, Is.EqualTo(2));
            Assert.That(child5.RightChild.NodeKey, Is.EqualTo(6));
            Assert.That(child6.Parent.NodeKey, Is.EqualTo(5));
            Assert.That(child2.Parent.NodeKey, Is.EqualTo(5));
        }

        [Test]
        public void Delete_RightNode()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;
            
            // act
            var res = tree.DeleteNodeByKey(12);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(12);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.That(root.RightChild.NodeKey, Is.EqualTo(13));
            Assert.That(child13.LeftChild.NodeKey, Is.EqualTo(10));
            Assert.That(child13.RightChild.NodeKey, Is.EqualTo(14));
            Assert.That(child10.Parent.NodeKey, Is.EqualTo(13));
            Assert.That(child14.Parent.NodeKey, Is.EqualTo(13));
        }

        [Test]
        [TestCase(17)]
        [TestCase(-1)]
        public void Delete_NotFound(int key)
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;

            var res = tree.DeleteNodeByKey(key);
            
            Assert.IsFalse(res);
        }
        
        [Test]
        public void Delete_LeftNode_WithoutLeaf()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;
            
            // act
            var res = tree.DeleteNodeByKey(4);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(4);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.That(root.LeftChild.NodeKey, Is.EqualTo(6));
            Assert.That(child6.LeftChild.NodeKey, Is.EqualTo(2));
            Assert.That(child6.RightChild.NodeKey, Is.EqualTo(7));
            Assert.That(child2.Parent.NodeKey, Is.EqualTo(6));
            Assert.That(child7.Parent.NodeKey, Is.EqualTo(6));
        }
        
        [Test]
        public void Delete_RightNode_WithoutLeaf()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.RightChild = child15;
            
            // act
            var res = tree.DeleteNodeByKey(12);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(12);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.That(root.RightChild.NodeKey, Is.EqualTo(14));
            Assert.That(child14.LeftChild.NodeKey, Is.EqualTo(10));
            Assert.That(child14.RightChild.NodeKey, Is.EqualTo(15));
            Assert.That(child10.Parent.NodeKey, Is.EqualTo(14));
            Assert.That(child15.Parent.NodeKey, Is.EqualTo(14));
        }

        #endregion

        #region Count

        [Test]
        public void CountFirst()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;

            var res = tree.Count();
            
            Assert.That(res, Is.EqualTo(15));
        }

        [Test]
        public void AnotherTree_DeleteLeaf()
        {
            // Arrange
            var root = new BSTNode<int>(6, 6, null);
            var tree = new BST<int>(root);
            var child2 = new BSTNode<int>(2, 2, root);
            var child8 = new BSTNode<int>(8, 8, root);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child4 = new BSTNode<int>(4, 4, child2);
            var child3 = new BSTNode<int>(3, 3, child4);
            var child5 = new BSTNode<int>(5, 5, child4);
            var child7 = new BSTNode<int>(7, 7, child8);
            var child9 = new BSTNode<int>(9, 9, child8);
            root.LeftChild = child2;
            root.RightChild = child8;
            child2.LeftChild = child1;
            child2.RightChild = child4;
            child4.LeftChild = child3;
            child4.RightChild = child5;
            child8.LeftChild = child7;
            child8.RightChild = child9;

            var res = tree.DeleteNodeByKey(1);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(1);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.IsNull(child2.LeftChild);
            Assert.That(child2.RightChild.NodeKey, Is.EqualTo(4));
        }
        
        [Test]
        public void AnotherTree_DeleteLeaf2()
        {
            // Arrange
            var root = new BSTNode<int>(6, 6, null);
            var tree = new BST<int>(root);
            var child2 = new BSTNode<int>(2, 2, root);
            var child8 = new BSTNode<int>(8, 8, root);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child4 = new BSTNode<int>(4, 4, child2);
            var child3 = new BSTNode<int>(3, 3, child4);
            var child5 = new BSTNode<int>(5, 5, child4);
            var child7 = new BSTNode<int>(7, 7, child8);
            var child9 = new BSTNode<int>(9, 9, child8);
            root.LeftChild = child2;
            root.RightChild = child8;
            child2.LeftChild = child1;
            child2.RightChild = child4;
            child4.LeftChild = child3;
            child4.RightChild = child5;
            child8.LeftChild = child7;
            child8.RightChild = child9;

            var res = tree.DeleteNodeByKey(5);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(5);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.IsNull(child4.RightChild);
            Assert.That(child4.LeftChild.NodeKey, Is.EqualTo(3));
            Assert.That(child4.Parent.NodeKey, Is.EqualTo(2));
        }
        
        [Test]
        public void AnotherTree_Delete4()
        {
            // Arrange
            var root = new BSTNode<int>(6, 6, null);
            var tree = new BST<int>(root);
            var child2 = new BSTNode<int>(2, 2, root);
            var child8 = new BSTNode<int>(8, 8, root);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child4 = new BSTNode<int>(4, 4, child2);
            var child3 = new BSTNode<int>(3, 3, child4);
            var child5 = new BSTNode<int>(5, 5, child4);
            var child7 = new BSTNode<int>(7, 7, child8);
            var child9 = new BSTNode<int>(9, 9, child8);
            root.LeftChild = child2;
            root.RightChild = child8;
            child2.LeftChild = child1;
            child2.RightChild = child4;
            child4.LeftChild = child3;
            child4.RightChild = child5;
            child8.LeftChild = child7;
            child8.RightChild = child9;

            var res = tree.DeleteNodeByKey(4);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(4);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.That(child2.RightChild.NodeKey, Is.EqualTo(5));
            Assert.That(child5.LeftChild.NodeKey, Is.EqualTo(3));
            Assert.That(child3.Parent.NodeKey, Is.EqualTo(5));
        }
        
        [Test]
        public void AnotherTree_Delete2()
        {
            // Arrange
            var root = new BSTNode<int>(6, 6, null);
            var tree = new BST<int>(root);
            var child2 = new BSTNode<int>(2, 2, root);
            var child8 = new BSTNode<int>(8, 8, root);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child4 = new BSTNode<int>(4, 4, child2);
            var child3 = new BSTNode<int>(3, 3, child4);
            var child5 = new BSTNode<int>(5, 5, child4);
            var child7 = new BSTNode<int>(7, 7, child8);
            var child9 = new BSTNode<int>(9, 9, child8);
            root.LeftChild = child2;
            root.RightChild = child8;
            child2.LeftChild = child1;
            child2.RightChild = child4;
            child4.LeftChild = child3;
            child4.RightChild = child5;
            child8.LeftChild = child7;
            child8.RightChild = child9;

            var res = tree.DeleteNodeByKey(2);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(2);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.That(root.LeftChild.NodeKey, Is.EqualTo(3));
            Assert.That(child3.Parent.NodeKey, Is.EqualTo(6));
            Assert.That(child3.LeftChild.NodeKey, Is.EqualTo(1));
            Assert.That(child3.RightChild.NodeKey, Is.EqualTo(4));
            Assert.That(child4.Parent.NodeKey, Is.EqualTo(3));
            Assert.IsNull(child4.LeftChild);
            Assert.That(child4.RightChild.NodeKey, Is.EqualTo(5));
        }
        
        [Test]
        public void AnotherTree_Mod_Delete2()
        {
            // Arrange
            var root = new BSTNode<int>(6, 6, null);
            var tree = new BST<int>(root);
            var child2 = new BSTNode<int>(2, 2, root);
            var child8 = new BSTNode<int>(8, 8, root);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child4 = new BSTNode<int>(4, 4, child2);
            var child5 = new BSTNode<int>(5, 5, child4);
            var child7 = new BSTNode<int>(7, 7, child8);
            var child9 = new BSTNode<int>(9, 9, child8);
            root.LeftChild = child2;
            root.RightChild = child8;
            child2.LeftChild = child1;
            child2.RightChild = child4;
            child4.RightChild = child5;
            child8.LeftChild = child7;
            child8.RightChild = child9;

            var res = tree.DeleteNodeByKey(2);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(2);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.That(root.LeftChild.NodeKey, Is.EqualTo(4));
            Assert.That(root.LeftChild.NodeKey, Is.EqualTo(4));
            Assert.That(child4.LeftChild.NodeKey, Is.EqualTo(1));
            Assert.That(child4.RightChild.NodeKey, Is.EqualTo(5));
            Assert.That(child1.Parent.NodeKey, Is.EqualTo(4));
            Assert.That(child5.Parent.NodeKey, Is.EqualTo(4));
            Assert.That(child4.Parent.NodeKey, Is.EqualTo(6));
        }
        
        [Test]
        public void OtherTree_Mod_Delete2()
        {
            // Arrange
            var root = new BSTNode<int>(6, 6, null);
            var tree = new BST<int>(root);
            var child2 = new BSTNode<int>(2, 2, root);
            var child1 = new BSTNode<int>(1, 1, child2);
            root.LeftChild = child2;
            child2.LeftChild = child1;

            var res = tree.DeleteNodeByKey(2);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(2);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.That(root.LeftChild.NodeKey, Is.EqualTo(1));
            Assert.That(child1.Parent.NodeKey, Is.EqualTo(6));
        }
        
        [Test]
        public void OtherTree_Mod_Delete1()
        {
            // Arrange
            var root = new BSTNode<int>(6, 6, null);
            var tree = new BST<int>(root);
            var child2 = new BSTNode<int>(2, 2, root);
            var child1 = new BSTNode<int>(1, 1, child2);
            root.LeftChild = child2;
            child2.LeftChild = child1;

            var res = tree.DeleteNodeByKey(1);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(1);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.That(root.LeftChild.NodeKey, Is.EqualTo(2));
            Assert.That(child2.Parent.NodeKey, Is.EqualTo(6));
            Assert.IsNull(child2.LeftChild);
        }
        
        [Test]
        public void OtherTree_Mod_OnlyLeftLong()
        {
            // Arrange
            var root = new BSTNode<int>(6, 6, null);
            var tree = new BST<int>(root);
            var child3 = new BSTNode<int>(3, 3, root); 
            var child2 = new BSTNode<int>(2, 2, child3);
            var child1 = new BSTNode<int>(1, 1, child2);
            root.LeftChild = child3;
            child3.LeftChild = child2;
            child2.LeftChild = child1;

            var res = tree.DeleteNodeByKey(3);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(3);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.That(root.LeftChild.NodeKey, Is.EqualTo(2));
            Assert.That(child2.Parent.NodeKey, Is.EqualTo(6));
            Assert.That(child2.LeftChild.NodeKey, Is.EqualTo(1));
        }
        
        [Test]
        public void OtherTree_Mod_OnlyRight()
        {
            // Arrange
            var root = new BSTNode<int>(6, 6, null);
            var tree = new BST<int>(root);
            var child8 = new BSTNode<int>(8, 8, root);
            var child9 = new BSTNode<int>(9, 9, child8);
            root.RightChild = child8;
            child8.RightChild = child9;

            var res = tree.DeleteNodeByKey(8);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(8);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.That(root.RightChild.NodeKey, Is.EqualTo(9));
            Assert.That(child9.Parent.NodeKey, Is.EqualTo(6));
        }
        
        [Test]
        public void OtherTree_Mod_OnlyRightLong()
        {
            // Arrange
            var root = new BSTNode<int>(6, 6, null);
            var tree = new BST<int>(root);
            var child8 = new BSTNode<int>(8, 8, root);
            var child9 = new BSTNode<int>(9, 9, child8);
            var child10 = new BSTNode<int>(10, 10, child9);
            root.RightChild = child8;
            child8.RightChild = child9;
            child9.RightChild = child10;

            var res = tree.DeleteNodeByKey(8);
            
            Assert.IsTrue(res);

            var deleted = tree.FindNodeByKey(8);
            
            Assert.IsFalse(deleted.NodeHasKey);
            Assert.That(root.RightChild.NodeKey, Is.EqualTo(9));
            Assert.That(child9.Parent.NodeKey, Is.EqualTo(6));
            Assert.That(child9.RightChild.NodeKey, Is.EqualTo(10));
        }

        [Test]
        public void Count_One()
        {
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);

            var res = tree.Count();
            
            Assert.That(res, Is.EqualTo(1));
        }

        #endregion
    }
}