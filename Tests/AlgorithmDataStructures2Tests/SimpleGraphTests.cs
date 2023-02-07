using System.Collections.Generic;
using System.Linq;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace AlgorithmDataStructures2Tests
{
    [TestFixture]
    public class SimpleGraphTests
    {
        #region IsEdge

        [Test]
        public void IsEdge_Empty_False()
        {
            var graph = new SimpleGraph<int>(5);
            
            Assert.IsFalse(graph.IsEdge(0, 1));
        }

        [Test]
        public void IsEdge_OnlyOne_False()
        {
            var graph = new SimpleGraph<int>(5);
            graph.AddVertex(2);
            
            Assert.IsFalse(graph.IsEdge(0, 1));
        }

        [Test]
        public void IsEdge_AllWithoutEdges_False()
        {
            var graph = new SimpleGraph<int>(5);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);
            
            Assert.IsFalse(graph.IsEdge(0, 1));
            Assert.IsFalse(graph.IsEdge(1, 1));
            Assert.IsFalse(graph.IsEdge(2, 1));
            Assert.IsFalse(graph.IsEdge(3, 1));
            Assert.IsFalse(graph.IsEdge(4, 1));
            
            Assert.IsFalse(graph.IsEdge(0, 0));
            Assert.IsFalse(graph.IsEdge(1, 0));
            Assert.IsFalse(graph.IsEdge(2, 0));
            Assert.IsFalse(graph.IsEdge(3, 0));
            Assert.IsFalse(graph.IsEdge(4, 0));
            
            Assert.IsFalse(graph.IsEdge(0, 2));
            Assert.IsFalse(graph.IsEdge(1, 2));
            Assert.IsFalse(graph.IsEdge(2, 2));
            Assert.IsFalse(graph.IsEdge(3, 2));
            Assert.IsFalse(graph.IsEdge(4, 2));
            
            Assert.IsFalse(graph.IsEdge(0, 3));
            Assert.IsFalse(graph.IsEdge(1, 3));
            Assert.IsFalse(graph.IsEdge(2, 3));
            Assert.IsFalse(graph.IsEdge(3, 3));
            Assert.IsFalse(graph.IsEdge(4, 3));
            
            Assert.IsFalse(graph.IsEdge(0, 4));
            Assert.IsFalse(graph.IsEdge(1, 4));
            Assert.IsFalse(graph.IsEdge(2, 4));
            Assert.IsFalse(graph.IsEdge(3, 4));
            Assert.IsFalse(graph.IsEdge(4, 4));
        }

        [Test]
        public void IsEdge_AllWithEdges_True()
        {
            var graph = new SimpleGraph<int>(5);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            graph.AddEdge(0, 1);
            Assert.IsTrue(graph.IsEdge(0, 1));
        }

        #endregion
        
        #region AddEdge

        [Test]
        public void AddEdge_Correct()
        {
            // сначала связи нет
            // потом она должна появиться
            var graph = new SimpleGraph<int>(5);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            Assert.IsFalse(graph.IsEdge(0, 1));
            graph.AddEdge(0, 1);
            Assert.IsTrue(graph.IsEdge(0, 1));
        }

        #endregion
        
        #region RemoveEdge

        [Test]
        public void RemoveEdge_Correct()
        {
            // тест: до удаления связь между вершинами была, после удаления отсутствует);
            var graph = new SimpleGraph<int>(5);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            graph.AddEdge(0, 1);
            Assert.IsTrue(graph.IsEdge(0, 1));
            graph.RemoveEdge(0, 1);
            Assert.IsFalse(graph.IsEdge(0, 1));
        }

        #endregion
        
        #region AddVertex

        [Test]
        public void AddVertex_VertexFull_NotModified()
        {
            var graph = new SimpleGraph<int>(5);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);
            graph.AddVertex(6);
            
            Assert.IsFalse(graph.vertex.Any(x => x.Value == 6));
        }

        #endregion
        
        #region RemoveVertex

        [Test]
        public void RemoveVertex()
        {
            // (тест: до удаления некоторые вершины имеют связи с удаляемой вершиной, после удаления этих связей нету).
            var graph = new SimpleGraph<int>(5);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);
            
            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,0);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(2,0);
            graph.AddEdge(2,3);
            graph.AddEdge(3,0);
            graph.AddEdge(3,1);
            graph.AddEdge(3,2);
            graph.AddEdge(3,3);
            graph.AddEdge(3,4);
            graph.AddEdge(4,1);
            graph.AddEdge(4,3);
            
            Assert.IsTrue(graph.IsEdge(0,1));
            Assert.IsTrue(graph.IsEdge(0,2));
            Assert.IsTrue(graph.IsEdge(0,3));
            Assert.IsTrue(graph.IsEdge(1,0));
            Assert.IsTrue(graph.IsEdge(1,3));
            Assert.IsTrue(graph.IsEdge(1,4));
            Assert.IsTrue(graph.IsEdge(2,0));
            Assert.IsTrue(graph.IsEdge(2,3));
            Assert.IsTrue(graph.IsEdge(3,0));
            Assert.IsTrue(graph.IsEdge(3,1));
            Assert.IsTrue(graph.IsEdge(3,2));
            Assert.IsTrue(graph.IsEdge(3,3));
            Assert.IsTrue(graph.IsEdge(3,4));
            Assert.IsTrue(graph.IsEdge(4,1));
            Assert.IsTrue(graph.IsEdge(4,3));
            
            graph.RemoveVertex(3);

            for (int i = 0; i < 4; i++)
            {
                Assert.IsFalse(graph.IsEdge(3, i));
            }
            
            Assert.IsFalse(graph.IsEdge(0, 3));
        }

        #endregion

        #region DepthFirstSearch

        [Test]
        public void Test1()
        {
            var graph = new SimpleGraph<int>(5);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            var res = graph.DepthFirstSearch(0, 1);
            var res1 = graph.DepthFirstSearch(0, 2);
            var res2 = graph.DepthFirstSearch(0, 3);
            var res3 = graph.DepthFirstSearch(0, 4);
            var res4 = graph.DepthFirstSearch(1, 4);
            var res5 = graph.DepthFirstSearch(2, 4);
            
            Assert.That(res.Count, Is.EqualTo(0));
            Assert.That(res1.Count, Is.EqualTo(0));
            Assert.That(res2.Count, Is.EqualTo(0));
            Assert.That(res3.Count, Is.EqualTo(0));
            Assert.That(res4.Count, Is.EqualTo(0));
            Assert.That(res5.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void Test2()
        {
            var graph = new SimpleGraph<int>(5);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            graph.AddEdge(0, 1);
            graph.AddEdge(1, 0);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 2);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 0);
            graph.AddEdge(0, 4);

            var res = graph.DepthFirstSearch(0, 4);
            var resInt = res.Select(x => x.Value);
            var expected = new List<int>() {1, 5};
            
            Assert.That(res.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(expected, resInt);
        }
        
        [Test]
        public void Test3()
        {
            var graph = new SimpleGraph<int>(5);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            graph.AddEdge(0, 1);
            graph.AddEdge(1, 0);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 2);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 3);

            var res = graph.DepthFirstSearch(0, 4);
            var resInt = res.Select(x => x.Value);
            var expected = new List<int>() {1,2,3,4, 5};
            
            Assert.That(res.Count, Is.EqualTo(5));
            CollectionAssert.AreEqual(expected, resInt);
        }
        
        [Test]
        public void Test4()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(2,3);
            graph.AddEdge(3,4);

            var res = graph.DepthFirstSearch(0, 4);
            var resInt = res.Select(x => x.Value);
            var expected = new List<string>() {"A", "B", "E"};
            
            Assert.That(res.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(expected, resInt);
        }
        
        [Test]
        public void Test5()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(2,3);
            graph.AddEdge(3,4);

            var res = graph.DepthFirstSearch(0, 3);
            var resInt = res.Select(x => x.Value);
            var expected = new List<string>() {"A", "D"};
            
            Assert.That(res.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(expected, resInt);
        }

        [Test]
        public void Test6()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(3,4);
            
            var res = graph.DepthFirstSearch(0, 2);
            Assert.That(res.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void Test7()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(2,3);
            
            var res = graph.DepthFirstSearch(0, 4);
            Assert.That(res.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void Test8()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(2,3);
            graph.AddEdge(3,4);

            var res = graph.DepthFirstSearch(0, 1);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() {"A", "B"};
            
            Assert.That(res.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(expected, resMod);
        }
        
        [Test]
        public void Test9()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(2,3);
            graph.AddEdge(3,4);

            var res = graph.DepthFirstSearch(3, 4);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() {"D", "E"};
            
            Assert.That(res.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(expected, resMod);
        }
        
        [Test]
        public void Test10()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(2,3);
            graph.AddEdge(3,4);

            var res = graph.DepthFirstSearch(2, 3);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() {"C", "D"};
            
            Assert.That(res.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(expected, resMod);
        }
        
        [Test]
        public void Test11()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(2,3);
            graph.AddEdge(3,4);

            var res = graph.DepthFirstSearch(1, 2);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() {"B", "A", "C"};
            
            Assert.That(res.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(expected, resMod);
        }
        
        [Test]
        public void Test12()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(2,3);
            graph.AddEdge(3,4);

            var res = graph.DepthFirstSearch(2, 4);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() {"C", "A", "B", "E"};
            
            Assert.That(res.Count, Is.EqualTo(4));
            CollectionAssert.AreEqual(expected, resMod);
        }
        
        #endregion

        #region BreadthFirstSearch

        [Test]
        public void Test_NoEdges_ReturnEmptyList()
        {
            var graph = new SimpleGraph<int>(5);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            var res0 = graph.BreadthFirstSearch(0, 1);
            var res1 = graph.BreadthFirstSearch(0, 2);
            var res2 = graph.BreadthFirstSearch(0, 3);
            var res3 = graph.BreadthFirstSearch(0, 4);
            var res4 = graph.BreadthFirstSearch(1, 4);
            var res5 = graph.BreadthFirstSearch(2, 4);
            
            Assert.That(res0.Count, Is.EqualTo(0));
            Assert.That(res1.Count, Is.EqualTo(0));
            Assert.That(res2.Count, Is.EqualTo(0));
            Assert.That(res3.Count, Is.EqualTo(0));
            Assert.That(res4.Count, Is.EqualTo(0));
            Assert.That(res5.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestW2()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(2,3);
            graph.AddEdge(3,4);

            var res = graph.BreadthFirstSearch(0, 4);
            var resInt = res.Select(x => x.Value);
            var expected = new List<string>() {"A", "B", "E"};
            
            Assert.That(res.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(expected, resInt);
        }

        [Test]
        public void TestW3()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(2,3);
            graph.AddEdge(3,4);

            var res = graph.BreadthFirstSearch(0, 3);
            var resInt = res.Select(x => x.Value);
            var expected = new List<string>() {"A", "D"};
            
            Assert.That(res.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(expected, resInt);
        }
        
        [Test]
        public void TestW6_NotFound()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(3,4);
            
            var res = graph.BreadthFirstSearch(0, 2);
            Assert.That(res.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void TestW7()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(2,3);
            
            var res = graph.BreadthFirstSearch(0, 4);
            Assert.That(res.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void TestW8()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(2,3);
            graph.AddEdge(3,4);

            var res = graph.BreadthFirstSearch(0, 1);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() {"A", "B"};
            
            Assert.That(res.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(expected, resMod);
        }
        
        [Test]
        public void Test12W()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(0,3);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(2,3);
            graph.AddEdge(3,4);

            var res = graph.BreadthFirstSearch(2, 4);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() {"C", "D", "E"};
            
            Assert.That(res.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(expected, resMod);
        }

        [Test]
        public void TestW9()
        {
            var graph = new SimpleGraph<string>(6);
            graph.AddVertex("F");
            graph.AddVertex("E");
            graph.AddVertex("B");
            graph.AddVertex("D");
            graph.AddVertex("A");
            graph.AddVertex("C");

            graph.AddEdge(0,1);
            graph.AddEdge(0,3);
            graph.AddEdge(1,2);
            graph.AddEdge(2,4);
            graph.AddEdge(3,4);
            graph.AddEdge(3,5);
            graph.AddEdge(4,5);

            var res = graph.BreadthFirstSearch(0, 4);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() {"F", "D", "A"};
            
            Assert.That(res.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(expected, resMod);
        }
        
        [Test]
        public void TestW10()
        {
            var graph = new SimpleGraph<string>(6);
            graph.AddVertex("F");
            graph.AddVertex("E");
            graph.AddVertex("B");
            graph.AddVertex("D");
            graph.AddVertex("A");
            graph.AddVertex("C");

            graph.AddEdge(0,1);
            graph.AddEdge(0,3);
            graph.AddEdge(1,2);
            graph.AddEdge(2,4);
            graph.AddEdge(3,4);
            graph.AddEdge(3,5);
            graph.AddEdge(4,5);

            var res = graph.BreadthFirstSearch(4, 0);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() {"A", "D", "F"};
            
            Assert.That(res.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(expected, resMod);
        }
        
        [Test]
        public void Test11W()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("C");
            graph.AddVertex("B");
            graph.AddVertex("E");
            graph.AddVertex("D");
            graph.AddVertex("A");

            graph.AddEdge(0,4);
            graph.AddEdge(0,3);
            graph.AddEdge(3,4);
            graph.AddEdge(1,4);
            graph.AddEdge(1,3);
            graph.AddEdge(1,2);
            graph.AddEdge(2,3);

            var res = graph.BreadthFirstSearch(0, 2);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() {"C", "D", "E"};
            
            Assert.That(res.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(expected, resMod);

            var res2 = graph.BreadthFirstSearch(2, 0);
            var resMod2 = res2.Select(x => x.Value).ToList();
            var expected2 = new List<string>() {"E", "D", "C"};
            Assert.That(res2.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(expected2, resMod2);
        }
        
        [Test]
        public void Test13W()
        {
            var graph = new SimpleGraph<string>(5);
            graph.AddVertex("C");
            graph.AddVertex("B");
            graph.AddVertex("E");
            graph.AddVertex("D");
            graph.AddVertex("A");

            graph.AddEdge(0,4);
            graph.AddEdge(0,3);
            graph.AddEdge(3,4);
            graph.AddEdge(1,4);
            graph.AddEdge(1,3);
            graph.AddEdge(1,2);
            graph.AddEdge(2,3);

            var res = graph.BreadthFirstSearch(3, 4);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() { "D", "A"};
            
            Assert.That(res.Count, Is.EqualTo(expected.Count));
            CollectionAssert.AreEqual(expected, resMod);
        }
        
        [Test]
        public void TestW14()
        {
            var graph = new SimpleGraph<string>(6);
            graph.AddVertex("F");
            graph.AddVertex("E");
            graph.AddVertex("B");
            graph.AddVertex("D");
            graph.AddVertex("A");
            graph.AddVertex("C");

            graph.AddEdge(0,1);
            graph.AddEdge(0,3);
            graph.AddEdge(1,2);
            graph.AddEdge(2,4);
            graph.AddEdge(3,4);
            graph.AddEdge(3,5);
            graph.AddEdge(4,5);

            var res = graph.DepthFirstSearch(5, 2);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() {"C","D", "F", "E","B"};
            
            Assert.That(res.Count, Is.EqualTo(expected.Count));
            CollectionAssert.AreEqual(expected, resMod);
            
            var res2 = graph.BreadthFirstSearch(5, 2);
            var resMod2 = res2.Select(x => x.Value).ToList();
            var expected2 = new List<string>() {"C","A","B"};
            
            Assert.That(res2.Count, Is.EqualTo(expected2.Count));
            CollectionAssert.AreEqual(expected2, resMod2);
        }
        
        [Test]
        public void Test15()
        {
            var graph = new SimpleGraph<string>(3);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");

            graph.AddEdge(0,1);
            graph.AddEdge(1,2);
            graph.AddEdge(0,2);

            var res = graph.BreadthFirstSearch(0, 2);
            var resMod = res.Select(x => x.Value).ToList();
            var expected = new List<string>() {"A", "C"};
            
            Assert.That(res.Count, Is.EqualTo(expected.Count));
            CollectionAssert.AreEqual(expected, resMod);
        }

        #endregion

        #region WeakVertices

        [Test]
        public void WeakVertices_Initial()
        {
            var graph = new SimpleGraph<string>(9);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");
            graph.AddVertex("F");
            graph.AddVertex("G");
            graph.AddVertex("X");
            graph.AddVertex("Z");

            graph.AddEdge(0,1);
            graph.AddEdge(0,2);
            graph.AddEdge(1,2);
            graph.AddEdge(1,3);
            graph.AddEdge(2,3);
            graph.AddEdge(3,4);
            graph.AddEdge(2,6);
            graph.AddEdge(4,6);
            graph.AddEdge(5,6);
            graph.AddEdge(5,7);
            graph.AddEdge(6,7);
            graph.AddEdge(7,8);

            var res = graph.WeakVertices();
            var resSel = res.Select(x => x.Value).ToList();
            var expexted = new List<string>() {"E", "Z"};
            
            CollectionAssert.AreEqual(expexted, resSel);
        }
        
        [Test]
        public void WeakVertices_Triangle()
        {
            var graph = new SimpleGraph<string>(3);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");

            graph.AddEdge(0,1);
            graph.AddEdge(1,2);
            graph.AddEdge(0,2);

            var res = graph.WeakVertices();
            
            Assert.That(res.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void WeakVertices_Triangle_WithoutEdge()
        {
            var graph = new SimpleGraph<string>(3);
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");

            graph.AddEdge(0,1);
            graph.AddEdge(1,2);

            var res = graph.WeakVertices();
            
            var resSel = res.Select(x => x.Value).ToList();
            var expexted = new List<string>() {"A", "B", "C"};
            
            CollectionAssert.AreEqual(expexted, resSel);
        }
        
        [Test]
        public void WeakVertices_Two()
        {
            var graph = new SimpleGraph<string>(2);
            graph.AddVertex("A");
            graph.AddVertex("B");

            graph.AddEdge(0,1);

            var res = graph.WeakVertices();
            
            var resSel = res.Select(x => x.Value).ToList();
            var expexted = new List<string>() {"A", "B"};
            
            CollectionAssert.AreEqual(expexted, resSel);
        }
        
        [Test]
        public void WeakVertices_One()
        {
            var graph = new SimpleGraph<string>(1);
            graph.AddVertex("A");


            var res = graph.WeakVertices();
            
            var resSel = res.Select(x => x.Value).ToList();
            var expexted = new List<string>() {"A"};
            
            CollectionAssert.AreEqual(expexted, resSel);
        }

        #endregion
    }
}