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
            SimpleGraph graph = new SimpleGraph(5);
            
            Assert.IsFalse(graph.IsEdge(0, 1));
        }

        [Test]
        public void IsEdge_OnlyOne_False()
        {
            SimpleGraph graph = new SimpleGraph(5);
            graph.AddVertex(2);
            
            Assert.IsFalse(graph.IsEdge(0, 1));
        }

        [Test]
        public void IsEdge_AllWithoutEdges_False()
        {
            SimpleGraph graph = new SimpleGraph(5);
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
            SimpleGraph graph = new SimpleGraph(5);
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
            SimpleGraph graph = new SimpleGraph(5);
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
            SimpleGraph graph = new SimpleGraph(5);
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
            SimpleGraph graph = new SimpleGraph(5);
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
            SimpleGraph graph = new SimpleGraph(5);
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
        }

        #endregion
    }
}