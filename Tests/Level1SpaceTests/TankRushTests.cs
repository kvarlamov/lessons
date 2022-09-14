using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class TankRushTests
    {
        [Test]
        public void TaskTest0()
        {
            var result = Level1Solved.TankRush(5, 15, "000000000000000 000000000000000 000000100000000 000000000000000 000000000000000", 3, 5, "00000 00000 00001");
            
            Assert.IsTrue(result);
        }
        
        [Test]
        public void TaskTest()
        {
            var result = Level1Solved.TankRush(3, 4, "1234 2345 0987", 2, 2, "34 98");
            
            Assert.IsTrue(result);
        }
        
        [Test]
        [TestCase(2,1,"9 3", true)]
        [TestCase(2,1,"9 4", false)]
        [TestCase(1,1,"6", false)]
        [TestCase(1,1,"7", true)]
        [TestCase(2,3,"795 830", true)]
        [TestCase(2,3,"795 831", false)]
        public void TaskTest1(int H2, int W2, string s2, bool expected)
        {
            int H1 = 3;
            int W1 = 4;
            var s1 = "1234 4795 7830";
            var result = Level1Solved.TankRush(H1, W1, s1, H2, W2, s2);
            
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        [TestCase(2,1,"3 4", true)]
        [TestCase(1,1,"4", true)]
        [TestCase(1,1,"5", false)]
        public void TaskTest2(int H2, int W2, string s2, bool expected)
        {
            int H1 = 4;
            int W1 = 1;
            var s1 = "1 2 3 4";
            var result = Level1Solved.TankRush(H1, W1, s1, H2, W2, s2);
            
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        [TestCase(2,1,"2 3", false)]
        [TestCase(1,5,"12345", false)]
        public void TaskTest3(int H2, int W2, string s2, bool expected)
        {
            int H1 = 1;
            int W1 = 4;
            var s1 = "1234";
            var result = Level1Solved.TankRush(H1, W1, s1, H2, W2, s2);
            
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        [TestCase(2,2,"23 23", true)]
        [TestCase(1,2,"22", true)]
        [TestCase(1,2,"27", false)]
        [TestCase(2,2,"23 24", true)]
        [TestCase(2,2,"24 23", true)]
        [TestCase(4,2,"23 23 24 23", true)]
        [TestCase(4,1,"3 3 4 3", true)]
        [TestCase(4,1,"3 3 4 4", false)]
        [TestCase(1,1,"5", true)]
        [TestCase(1,1,"6", false)]
        [TestCase(4,5,"12345 12350 12450 22350", true)]
        [TestCase(4,4,"2345 2350 2450 2350", true)]
        public void TaskTest4(int H2, int W2, string s2, bool expected)
        {
            int H1 = 4;
            int W1 = 5;
            var s1 = "12345 12350 12450 22350";
            var result = Level1Solved.TankRush(H1, W1, s1, H2, W2, s2);
            
            Assert.AreEqual(expected, result);
        }
    }
}