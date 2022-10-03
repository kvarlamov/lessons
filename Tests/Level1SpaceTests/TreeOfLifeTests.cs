using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class TreeOfLifeTests
    {
        [Test]
        [TestCase(4)]
        [TestCase(8)]
        public void TaskTest(int N)
        {
            int H = 3;
            int W = 4;
            string[] tree = new[] {".+..", "..+.", ".+.."};
            var expected = new[] {".+..", "..+.", ".+.."};

            var res = Level1Solved.TreeOfLife(H, W, N, tree);
            
            CollectionAssert.AreEqual(expected, res);            
        }
        
        [Test]
        public void TaskTest0()
        {
            int H = 3;
            int W = 4;
            int N = 6;
            string[] tree = new[] {".+..", "..+.", ".+.."};
            var expected = new[] {"...+", "+...", "...+"};

            var res = Level1Solved.TreeOfLife(H, W, N, tree);
            
            CollectionAssert.AreEqual(expected, res);            
        }
        
        [Test]
        [TestCase(5)]
        [TestCase(7)]
        public void TaskTest2(int N)
        {
            int H = 3;
            int W = 4;
            string[] tree = new[] {".+..", "..+.", ".+.."};
            var expected = new[] {"++++", "++++", "++++"};

            var res = Level1Solved.TreeOfLife(H, W, N, tree);
            
            CollectionAssert.AreEqual(expected, res);            
        }
        
        [Test]
        [TestCase(2, new [] {"+...+", ".....", ".....",".....","....+"})]
        [TestCase(3, new [] {"+++++", "+++++", "+++++","+++++","+++++"})]
        [TestCase(4, new [] {"..+..", ".+++.", "+++++","++++.","+++.."})]
        [TestCase(5, new [] {"+++++", "+++++", "+++++","+++++","+++++"})]
        [TestCase(6, new [] {"+...+", ".....", ".....",".....","....+"})]
        [TestCase(7, new [] {"+++++", "+++++", "+++++","+++++","+++++"})]
        public void TaskTest3(int N, string[] expected)
        {
            int H = 5;
            int W = 5;
            string[] tree = new[] {"..+..", ".+...", "...++","++...","+.+.."};

            var res = Level1Solved.TreeOfLife(H, W, N, tree);
            
            CollectionAssert.AreEqual(expected, res);            
        }
    }
}