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
            var result = Level1.ShopOLAP(items.Length, items);

            string[] expected = {"платье1 6", "сумка128 4", "сумка23 2", "сумка32 2"};
            CollectionAssert.AreEqual(expected, result);
        }
    }
}