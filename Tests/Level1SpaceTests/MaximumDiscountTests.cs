using Level1Space;
using NUnit.Framework;

namespace Level1SolvedSpaceTests
{
    public class MaximumDiscountTests
    {
        [Test]
        public void Test1()
        {
            var result = Level1Solved.MaximumDiscount(7, new[] {400, 350, 300, 250, 200, 150, 100});
            
            Assert.AreEqual(450, result);
        }

        [Test]
        public void Test2()
        {
            var result = Level1Solved.MaximumDiscount(1, new[] {400});
            
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Test3()
        {
            var result = Level1Solved.MaximumDiscount(8, new[] {500, 500, 500, 400, 300, 100, 100, 50});
            
            Assert.AreEqual(600, result);
        }

        [Test]
        public void Test4()
        {
            var result = Level1Solved.MaximumDiscount(10, new[] {10, 9, 15, 7, 8, 5, 8, 10, 15, 9});
            
            Assert.AreEqual(26, result);
        }
    }
}