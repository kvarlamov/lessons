using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class MisterRobotTests
    {
        [Test]
        public void Test0()
        {
            var arr = new int[] {1,3,4,5,6,2,7};

            var res = Level1.MisterRobot(arr.Length, arr);

            Assert.AreEqual(true, res);
        }
        
        [Test]
        public void Test01()
        {
            var arr = new int[] {1,3,4,6,5,2,7};

            var res = Level1.MisterRobot(arr.Length, arr);

            Assert.AreEqual(false, res);
        }
        
        [Test]
        public void Test02()
        {
            var arr = new int[] {1,3,4,2};

            var res = Level1.MisterRobot(arr.Length, arr);

            Assert.AreEqual(true, res);
        }
        
        [Test]
        public void Test1()
        {
            var arr = new int[] {1, 2, 4, 5, 8, 3, 7, 6};

            var res = Level1.MisterRobot(arr.Length, arr);

            Assert.AreEqual(true, res);
        }
        
        [Test]
        public void Test2()
        {
            var arr = new int[] {1, 2, 4, 5, 9,8,7,3,6};

            var res = Level1.MisterRobot(arr.Length, arr);

            Assert.AreEqual(false, res);
        }
        
        [Test]
        public void Test3()
        {
            var arr = new int[] {1, 2, 4, 5, 8,9,7,3,6};

            var res = Level1.MisterRobot(arr.Length, arr);

            Assert.AreEqual( true, res);
        }
        
        [Test]
        public void Test4()
        {
            var arr = new int[] {9,8,7,6,5,4,3,2,1};

            var res = Level1.MisterRobot(arr.Length, arr);

            Assert.AreEqual( true, res);
        }
        
        [Test]
        public void Test5()
        {
            var arr = new int[] {9,8,7,6,5,4,3,2,1, 10, 11 ,12, 14, 13, 15, 17, 16};

            var res = Level1.MisterRobot(arr.Length, arr);

            Assert.AreEqual( true, res);
        }
        
        [Test]
        public void Test6()
        {
            var arr = new int[] {9,8,7,6,5,4,3,2,1, 10, 11 ,12, 13, 14, 15, 17, 16};

            var res = Level1.MisterRobot(arr.Length, arr);

            Assert.AreEqual( false, res);
        }
    }
}