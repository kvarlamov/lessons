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
        public void Test1(string input, string expected)
        {
            var res = Level1.BiggerGreater(input);
            
            Assert.AreEqual(expected, res);
        }
    }
}