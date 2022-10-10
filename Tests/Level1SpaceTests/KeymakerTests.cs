using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class KeymakerTests
    {
        [Test]
         [TestCase(3, "100")]
         [TestCase(4, "1001")]
         [TestCase(5, "10010")]
         [TestCase(6, "100100")]
         [TestCase(7, "1001000")]
         [TestCase(8, "10010000")]
         [TestCase(9, "100100001")]
        [TestCase(10, "1001000010")]
        [TestCase(11, "10010000100")]
        [TestCase(12, "100100001000")]
        [TestCase(13, "1001000010000")]
        [TestCase(14, "10010000100000")]
        [TestCase(15, "100100001000000")]
        [TestCase(16, "1001000010000001")]
        [TestCase(17, "10010000100000010")]
        [TestCase(18, "100100001000000100")]
        [TestCase(19, "1001000010000001000")]
        [TestCase(20, "10010000100000010000")]
        [TestCase(21, "100100001000000100000")]
        [TestCase(22, "1001000010000001000000")]
        [TestCase(23, "10010000100000010000000")]
        [TestCase(24, "100100001000000100000000")]
        [TestCase(25, "1001000010000001000000001")]
        [TestCase(26, "10010000100000010000000010")]
        [TestCase(27, "100100001000000100000000100")]
        [TestCase(28, "1001000010000001000000001000")]
        [TestCase(29, "10010000100000010000000010000")]
        [TestCase(30, "100100001000000100000000100000")]
        [TestCase(31, "1001000010000001000000001000000")]
        [TestCase(32, "10010000100000010000000010000000")]
        [TestCase(33, "100100001000000100000000100000000")]
        [TestCase(34, "1001000010000001000000001000000000")]
        [TestCase(35, "10010000100000010000000010000000000")]
        [TestCase(36, "100100001000000100000000100000000001")]
        public void Test(int k, string expected)
        {
            //если есть корень из числа - то последний 1
            //every 1 added when
            //2^2
            //3^2
            //4^2
            //5^2
            //6^2
            //7^2
            var result = Level1.Keymaker(k);
            Assert.AreEqual(expected, result);
        }
    }
}