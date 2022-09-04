using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class WordSearchTests
    {
        [Test]
        [TestCase(6, "где", new int[]{0,0,0,0,0,1,0,0})]
        [TestCase(6, "кар", new int[]{0,0,0,0,0,0,0,0})]
        [TestCase(12, "фазан", new int[]{0,0,0,0,1})]
        [TestCase(12, "где", new int[]{0,0,0,1,0})]
        [TestCase(12, "что", new int[]{0,0,0,0,0})]
        public void WordSearch_Fazan(int len, string subs, int[] expected)
        {
            // Arrange
            var s = "Каждый охотник желает знать где сидит фазан";

            var res = Level1Solved.WordSearch(len, s, subs);
            
            // Assert
            Assert.AreEqual(expected, res);
        }
        
        [Test]
        public void WordSearch_LongWorldWith12()
        {
            // Arrange
            var s = "1) строка разбивается на набор строк через выравнивание по заданной ширине.";

            var res = Level1Solved.WordSearch(12, s, "строк");
            
            // Assert
            Assert.AreEqual(new int[] {0, 0, 0, 1, 0, 0, 0}, res);
        }
        
        [Test]
        public void WordSearch_LongWorldWith10()
        {
            // Arrange
            var s = "1) строка разбивается на набор строк через выравнивание по заданной ширине.";

            var res = Level1Solved.WordSearch(10, s, "строк");
            
            // Assert
            Assert.AreEqual(new int[] {0, 0, 0, 1, 0, 0, 0, 0, 0}, res);
        }
        
        [Test]
        public void WordSearch_Multiple()
        {
            // Arrange
            var s = "любой тест на нахождение строки тест здесь.";

            var res = Level1Solved.WordSearch(4, s, "тест");
            
            // Assert
            Assert.AreEqual(new int[] {0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0}, res);
        }
    }
}