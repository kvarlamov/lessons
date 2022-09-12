using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class UnmannedTests
    {
        [Test]
        public void FirstCaseTest()
        {
            var res = Level1.Unmanned(10, 2, new int[][] {new[] {3, 5, 5}, new[] {5, 2, 2}});
            Assert.AreEqual(12, res);
        }
        
        [Test]
        public void Case2Test()
        {
            var res = Level1.Unmanned(15, 3, new int[][] {new[] {0, 4, 1}, new[] {3, 3, 3}, new []{8, 2, 2}});
            Assert.AreEqual(21, res);
        }
        
        [Test]
        public void Case3Test()
        {
            var res = Level1.Unmanned(10, 3, new int[][] {new[] {2, 2, 1}, new[] {5, 3, 3}, new []{10, 5, 2}});
            Assert.AreEqual(10, res);
        }
        
        [Test]
        public void CaseLastTest()
        {
            var res = Level1.Unmanned(10, 3, new int[][] {new[] {1, 1, 1}, new[] {4, 1, 1}, new []{7, 1, 1}});
            Assert.AreEqual(12, res);
        }
    }
}