using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class ShopOLAPTests
    {
        [Test]
        public void Test()
        {
            string[] items = {"платье1 5", "сумка32 2", "платье1 1", "сумка23 2", "сумка128 4"};
            var result = Level1Solved.ShopOLAP(items.Length, items);

            string[] expected = {"платье1 6", "сумка128 4", "сумка23 2", "сумка32 2"};
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void test2()
        {
            string[] items = {"something 4"};
            var result = Level1Solved.ShopOLAP(items.Length, items);

            string[] expected = {"something 4"};
            CollectionAssert.AreEqual(expected, result);
        }
    }
}