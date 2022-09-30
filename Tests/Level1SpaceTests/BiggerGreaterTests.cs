using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class BiggerGreaterTests
    {
        [Test]
        [TestCase("нклм", "нкмл")]
        [TestCase("ая", "яа")]
        [TestCase("вибк", "викб")]
        [TestCase("вкиб", "ибвк")]
        [TestCase("za", "")]
        public void Test1(string input, string expected)
        {
            var res = Level1Solved.BiggerGreater(input);
            
            Assert.AreEqual(expected, res);
        }
    }
}