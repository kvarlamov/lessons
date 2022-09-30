using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class SherlockValidStringTests
    {
        [Test]
        [TestCase("xyz", true)]
        [TestCase("xyzaa", true)]
        [TestCase("xxyyz", true)]
        [TestCase("xyzzz", false)]
        [TestCase("xxyyza", false)]
        [TestCase("xxyyzabc", false)]
        [TestCase("xyyzabc", true)]
        [TestCase("xy", true)]
        [TestCase("xx", true)]
        [TestCase("xyyzabcc", false)]
        public void Test(string input, bool expected)
        {
            var res = Level1.SherlockValidString(input);
            
            Assert.AreEqual(expected, res);
        }
    }
}