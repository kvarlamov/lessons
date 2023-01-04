using System.Linq;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace AlgorithmDataStructures2Tests
{
    [TestFixture]
    public class SimpleTreeTests
    {
        #region Add

        [Test]
        public void Add_OnlyRoot_GetRoot()
        {
            var tree = new SimpleTree<int>(new SimpleTreeNode<int>(1, null));

            var list = tree.GetAllNodes();
            
            Assert.That(tree.Count(), Is.EqualTo(1));
            Assert.That(list.First().NodeValue, Is.EqualTo(1));
        }

        [Test]
        public void Add_Null_GetEmpty()
        {
            
        }

        [Test]
        public void Add_Full_GetAll()
        {
            var tree = GetNotEmptyTree();

            var list = tree.GetAllNodes();
            
            Assert.That(tree.Count(), Is.EqualTo(9));
            Assert.That(list.First().NodeValue, Is.EqualTo(9));
            Assert.That(list.First().Children.Count, Is. EqualTo(2));
            Assert.That(list.First().Children.First().NodeValue, Is. EqualTo(4));
            Assert.That(list.First().Children.First().Parent, Is. EqualTo(tree.Root));
            Assert.That(list.First().Children.Last().NodeValue, Is. EqualTo(17));
            Assert.That(list.First().Children.Last().Parent, Is. EqualTo(tree.Root));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 3));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 6));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 5));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 7));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 22));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 20));
            Assert.IsNull(list.FirstOrDefault(x => x.NodeValue == 30));
        }

        #endregion

        #region Delete

        [Test]
        public void Delete_Empty()
        {
            var root = new SimpleTreeNode<int>(1, null);
            var tree = new SimpleTree<int>(root);
            
            tree.DeleteNode(root);

            var res = tree.GetAllNodes();
            
            Assert.That(tree.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Delete_Child_NotEmpty()
        {
            var root = new SimpleTreeNode<int>(9, null);
            var tree = new SimpleTree<int>(root);
            var child = new SimpleTreeNode<int>(2, root);
            tree.AddChild(root, child);
            
            tree.DeleteNode(child);
            var res = tree.GetAllNodes();
            
            Assert.That(tree.Count(), Is.EqualTo(1));
            Assert.That(tree.Root.NodeValue, Is.EqualTo(9));
            Assert.That(tree.Root.Children.Count, Is.EqualTo(0));
        }

        [Test]
        public void Delete_Add_notEmpty()
        {
            var root = new SimpleTreeNode<int>(9, null);
            var tree = new SimpleTree<int>(root);
            
            tree.DeleteNode(root);
            tree.AddChild(null, root);

            var res = tree.GetAllNodes();
            
            Assert.That(tree.Count(), Is.EqualTo(1));
            Assert.That(res.First().NodeValue, Is.EqualTo(9));
            Assert.That(res.First().Children, Is.Null);
        }

        [Test]
        public void Delete_FullTree_NodeAndChildRemoved()
        {
            var root = new SimpleTreeNode<int>(9, null);;
            var child4 = new SimpleTreeNode<int>(4, root);
            var child17 = new SimpleTreeNode<int>(17, root);
            var tree = new SimpleTree<int>(root);
            tree.AddChild(root, child4);
            tree.AddChild(root, child17);
            var child3 = new SimpleTreeNode<int>(3, child4);
            var child6 = new SimpleTreeNode<int>(6, child4);
            var child22 = new SimpleTreeNode<int>(22, child17);
            var child5 = new SimpleTreeNode<int>(5, child6);
            var child7 = new SimpleTreeNode<int>(7, child6);
            var child20 = new SimpleTreeNode<int>(20, child22);
            tree.AddChild(child4, child3);
            tree.AddChild(child4, child6);
            tree.AddChild(child6, child5);
            tree.AddChild(child6, child7);
            tree.AddChild(child17, child22);
            tree.AddChild(child22, child20);
            
            tree.DeleteNode(child6);

            var list = tree.GetAllNodes();
            
            Assert.That(tree.Count(), Is.EqualTo(6));
            Assert.That(list.First().NodeValue, Is.EqualTo(9));
            Assert.That(list.First().Children.Count, Is. EqualTo(2));
            Assert.That(list.First().Children.First().NodeValue, Is. EqualTo(4));
            Assert.That(list.First().Children.First().Parent, Is. EqualTo(tree.Root));
            Assert.That(list.First().Children.Last().NodeValue, Is. EqualTo(17));
            Assert.That(list.First().Children.Last().Parent, Is. EqualTo(tree.Root));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 3));
            Assert.IsNull(list.FirstOrDefault(x => x.NodeValue == 6));
            Assert.IsNull(list.FirstOrDefault(x => x.NodeValue == 5));
            Assert.IsNull(list.FirstOrDefault(x => x.NodeValue == 7));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 22));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 20));
        }
        
        [Test]
        public void Delete_FullTree2_NodeAndChildRemoved()
        {
            var root = new SimpleTreeNode<int>(9, null);;
            var child4 = new SimpleTreeNode<int>(4, root);
            var child17 = new SimpleTreeNode<int>(17, root);
            var tree = new SimpleTree<int>(root);
            tree.AddChild(root, child4);
            tree.AddChild(root, child17);
            var child3 = new SimpleTreeNode<int>(3, child4);
            var child6 = new SimpleTreeNode<int>(6, child4);
            var child22 = new SimpleTreeNode<int>(22, child17);
            var child5 = new SimpleTreeNode<int>(5, child6);
            var child7 = new SimpleTreeNode<int>(7, child6);
            var child20 = new SimpleTreeNode<int>(20, child22);
            tree.AddChild(child4, child3);
            tree.AddChild(child4, child6);
            tree.AddChild(child6, child5);
            tree.AddChild(child6, child7);
            tree.AddChild(child17, child22);
            tree.AddChild(child22, child20);
            
            tree.DeleteNode(child7);

            var list = tree.GetAllNodes();
            
            Assert.That(tree.Count(), Is.EqualTo(8));
            Assert.That(list.First().NodeValue, Is.EqualTo(9));
            Assert.That(list.First().Children.Count, Is. EqualTo(2));
            Assert.That(list.First().Children.First().NodeValue, Is. EqualTo(4));
            Assert.That(list.First().Children.First().Parent, Is. EqualTo(tree.Root));
            Assert.That(list.First().Children.Last().NodeValue, Is. EqualTo(17));
            Assert.That(list.First().Children.Last().Parent, Is. EqualTo(tree.Root));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 3));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 6));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 5));
            Assert.IsNull(list.FirstOrDefault(x => x.NodeValue == 7));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 22));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 20));
        }
        
        [Test]
        public void Delete_FullTree3_NodeAndChildRemoved()
        {
            var root = new SimpleTreeNode<int>(9, null);;
            var child4 = new SimpleTreeNode<int>(4, root);
            var child17 = new SimpleTreeNode<int>(17, root);
            var tree = new SimpleTree<int>(root);
            tree.AddChild(root, child4);
            tree.AddChild(root, child17);
            var child3 = new SimpleTreeNode<int>(3, child4);
            var child6 = new SimpleTreeNode<int>(6, child4);
            var child22 = new SimpleTreeNode<int>(22, child17);
            var child5 = new SimpleTreeNode<int>(5, child6);
            var child7 = new SimpleTreeNode<int>(7, child6);
            var child20 = new SimpleTreeNode<int>(20, child22);
            tree.AddChild(child4, child3);
            tree.AddChild(child4, child6);
            tree.AddChild(child6, child5);
            tree.AddChild(child6, child7);
            tree.AddChild(child17, child22);
            tree.AddChild(child22, child20);
            
            tree.DeleteNode(child4);

            var list = tree.GetAllNodes();
            
            Assert.That(tree.Count(), Is.EqualTo(4));
            Assert.That(list.First().NodeValue, Is.EqualTo(9));
            Assert.That(list.First().Children.Count, Is. EqualTo(1));
            Assert.IsNull(list.First().Children.FirstOrDefault(x => x.NodeValue == 4));
            Assert.That(list.First().Children.First().NodeValue, Is. EqualTo(17));
            Assert.That(list.First().Children.First().Parent, Is. EqualTo(tree.Root));
            Assert.IsNull(list.FirstOrDefault(x => x.NodeValue == 3));
            Assert.IsNull(list.FirstOrDefault(x => x.NodeValue == 6));
            Assert.IsNull(list.FirstOrDefault(x => x.NodeValue == 5));
            Assert.IsNull(list.FirstOrDefault(x => x.NodeValue == 7));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 22));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 20));
        }
        
        [Test]
        public void Delete_FullTree4_NodeAndChildRemoved()
        {
            var root = new SimpleTreeNode<int>(9, null);;
            var child4 = new SimpleTreeNode<int>(4, root);
            var child17 = new SimpleTreeNode<int>(17, root);
            var tree = new SimpleTree<int>(root);
            tree.AddChild(root, child4);
            tree.AddChild(root, child17);
            var child3 = new SimpleTreeNode<int>(3, child4);
            var child6 = new SimpleTreeNode<int>(6, child4);
            var child22 = new SimpleTreeNode<int>(22, child17);
            var child5 = new SimpleTreeNode<int>(5, child6);
            var child7 = new SimpleTreeNode<int>(7, child6);
            var child20 = new SimpleTreeNode<int>(20, child22);
            tree.AddChild(child4, child3);
            tree.AddChild(child4, child6);
            tree.AddChild(child6, child5);
            tree.AddChild(child6, child7);
            tree.AddChild(child17, child22);
            tree.AddChild(child22, child20);
            
            tree.DeleteNode(root);
            
            Assert.That(tree.Count(), Is.EqualTo(0));
            Assert.IsNull(tree.Root);
        }

        #endregion

        #region FindNodesByValue

        [Test]
        [TestCase(9)]
        [TestCase(4)]
        [TestCase(17)]
        [TestCase(22)]
        [TestCase(20)]
        public void FindNodesByValue_OnlyOne(int val)
        {
            var tree = GetNotEmptyTree();

            var res = tree.FindNodesByValue(val);
            Assert.That(res.Count, Is.EqualTo(1));
            Assert.That(res.First().NodeValue, Is.EqualTo(val));
        }

        [Test]
        public void FindNodesByValue_NotFound()
        {
            var tree = GetNotEmptyTree();

            var res = tree.FindNodesByValue(50);
            
            Assert.That(res.Count, Is.EqualTo(0));
        }

        [Test]
        public void FindNodesByValue_FoundMany()
        {
            var root = new SimpleTreeNode<int>(9, null);;
            var child4 = new SimpleTreeNode<int>(4, root);
            var child17 = new SimpleTreeNode<int>(4, root);
            var tree = new SimpleTree<int>(root);
            tree.AddChild(root, child4);
            tree.AddChild(root, child17);
            var child3 = new SimpleTreeNode<int>(4, child4);
            var child6 = new SimpleTreeNode<int>(6, child4);
            var child22 = new SimpleTreeNode<int>(4, child17);
            var child5 = new SimpleTreeNode<int>(4, child6);
            var child7 = new SimpleTreeNode<int>(7, child6);
            var child20 = new SimpleTreeNode<int>(20, child22);
            tree.AddChild(child4, child3);
            tree.AddChild(child4, child6);
            tree.AddChild(child6, child5);
            tree.AddChild(child6, child7);
            tree.AddChild(child17, child22);
            tree.AddChild(child22, child20);
            
            var res = tree.FindNodesByValue(4);
            
            Assert.That(res.Count, Is.EqualTo(5));
        }

        #endregion

        #region MoveNodes

        [Test]
        public void MoveFirst()
        {
            var root = new SimpleTreeNode<int>(9, null);;
            var child4 = new SimpleTreeNode<int>(4, root);
            var child17 = new SimpleTreeNode<int>(17, root);
            var tree = new SimpleTree<int>(root);
            tree.AddChild(root, child4);
            tree.AddChild(root, child17);
            var child3 = new SimpleTreeNode<int>(3, child4);
            var child6 = new SimpleTreeNode<int>(6, child4);
            var child22 = new SimpleTreeNode<int>(22, child17);
            var child5 = new SimpleTreeNode<int>(5, child6);
            var child7 = new SimpleTreeNode<int>(7, child6);
            var child20 = new SimpleTreeNode<int>(20, child22);
            tree.AddChild(child4, child3);
            tree.AddChild(child4, child6);
            tree.AddChild(child6, child5);
            tree.AddChild(child6, child7);
            tree.AddChild(child17, child22);
            tree.AddChild(child22, child20);
            
            tree.MoveNode(child4, child22);

            var list = tree.GetAllNodes();
            
            Assert.That(tree.Count(), Is.EqualTo(9));
            Assert.That(tree.Root.NodeValue, Is.EqualTo(9));
            Assert.That(tree.Root.Children.Count, Is. EqualTo(1));
            Assert.That(tree.Root.Children.First().Parent, Is. EqualTo(tree.Root));
            Assert.That(tree.Root.Children.First().NodeValue, Is. EqualTo(17));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 3));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 6));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 5));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 7));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 22));
            Assert.IsNotNull(list.FirstOrDefault(x => x.NodeValue == 20));
            var moved = list.First(x => x.NodeValue == 4);
            Assert.That(moved.Children.Count, Is.EqualTo(2));
            Assert.That(moved.Parent, Is.EqualTo(child22));
            Assert.IsTrue(moved.Children.Contains(child3));
            Assert.IsTrue(moved.Children.Contains(child6));
        }

        #endregion

        [Test]
        public void CountLeafs_1()
        {
            var tree = GetNotEmptyTree();

            var leafs = tree.LeafCount();
            
            Assert.That(leafs, Is.EqualTo(4));
        }

        [Test]
        public void SetLevel_CheckIsCorrect()
        {
            var root = new SimpleTreeNode<int>(9, null);;
            var child4 = new SimpleTreeNode<int>(4, root);
            var child17 = new SimpleTreeNode<int>(17, root);
            var tree = new SimpleTree<int>(root);
            tree.AddChild(root, child4);
            tree.AddChild(root, child17);
            var child3 = new SimpleTreeNode<int>(3, child4);
            var child6 = new SimpleTreeNode<int>(6, child4);
            var child22 = new SimpleTreeNode<int>(22, child17);
            var child5 = new SimpleTreeNode<int>(5, child6);
            var child7 = new SimpleTreeNode<int>(7, child6);
            var child20 = new SimpleTreeNode<int>(20, child22);
            tree.AddChild(child4, child3);
            tree.AddChild(child4, child6);
            tree.AddChild(child6, child5);
            tree.AddChild(child6, child7);
            tree.AddChild(child17, child22);
            tree.AddChild(child22, child20);
            
            tree.SetLevel();
            
            Assert.That(tree.Root.Level, Is.EqualTo(1));
            Assert.That(child4.Level, Is.EqualTo(2));
            Assert.That(child17.Level, Is.EqualTo(2));
            Assert.That(child3.Level, Is.EqualTo(3));
            Assert.That(child6.Level, Is.EqualTo(3));
            Assert.That(child22.Level, Is.EqualTo(3));
            Assert.That(child5.Level, Is.EqualTo(4));
            Assert.That(child7.Level, Is.EqualTo(4));
            Assert.That(child20.Level, Is.EqualTo(4));
        }

        private SimpleTree<int> GetNotEmptyTree()
        {
            var root = new SimpleTreeNode<int>(9, null);;
            var child4 = new SimpleTreeNode<int>(4, root);
            var child17 = new SimpleTreeNode<int>(17, root);
            var tree = new SimpleTree<int>(root);
            tree.AddChild(root, child4);
            tree.AddChild(root, child17);
            var child3 = new SimpleTreeNode<int>(3, child4);
            var child6 = new SimpleTreeNode<int>(6, child4);
            var child22 = new SimpleTreeNode<int>(22, child17);
            var child5 = new SimpleTreeNode<int>(5, child6);
            var child7 = new SimpleTreeNode<int>(7, child6);
            var child20 = new SimpleTreeNode<int>(20, child22);
            tree.AddChild(child4, child3);
            tree.AddChild(child4, child6);
            tree.AddChild(child6, child5);
            tree.AddChild(child6, child7);
            tree.AddChild(child17, child22);
            tree.AddChild(child22, child20);
            
            return tree;
        }
    }
}