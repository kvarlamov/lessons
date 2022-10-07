using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class white_walkersTests
    {
        [Test]
        [TestCase("axxb6===4xaf5===eee5", true)]
        [TestCase("5==ooooooo=5=5", false)]
        [TestCase("abc=7==hdjs=3gg1=======5", true)]
        [TestCase("aaS=8", false)]
        [TestCase("9===1===9===1===9", true)]
        [TestCase("abc=8==hdjs=3gg1=======5", false)]
        public void Test1(string village, bool expected)
        {
            var result = Level1.white_walkers(village);
            Assert.AreEqual(expected, result);
        }
    }
}