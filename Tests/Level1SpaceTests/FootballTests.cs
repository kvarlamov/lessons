using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class FootballTests
    {
        [Test]
        [TestCase((new int[] {1,2,3}), false)]
        [TestCase((new int[] {1,3,2}), true)]
        [TestCase((new int[] {1, 7, 5, 3, 9}), true)]
        [TestCase((new int[] {1, 5, 3, 4, 2, 6}), true)]
        [TestCase((new int[] {5, 2, 3, 4, 1, 6}), true)]
        [TestCase((new int[] {3, 5, 1, 4, 2, 6}), false)]
        [TestCase((new int[] {9, 5, 3, 7, 1}), false)]
        [TestCase((new int[] {3,2,1}), true)]
        [TestCase((new int[] {15,4,2}), true)]
        [TestCase((new int[] {1,4,3,2}), true)]
        [TestCase((new int[] {3,2,1,4}), true)]
        [TestCase((new int[] {1,2,6,5,4,3}), true)]
        [TestCase((new int[] {1,2,6,5,4,3,7}), true)]
        [TestCase((new int[] {1,4,3,2,5}), true)]
        [TestCase((new int[] {3,8,7}), true)]
        [TestCase((new int[] {9,8,7}), true)]
        [TestCase((new int[] {9,8,7,3,1}), true)]
        public void Test(int[] arr, bool expected)
        {
            var result = Level1Solved.Football(arr, arr.Length);
            
            Assert.AreEqual(expected, result);
        }
    }
}