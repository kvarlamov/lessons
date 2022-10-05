using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class MatrixTurnTests
    {
        [Test]
        public void TestCase1()
        {
            string[] arr = new[] {"123456", "234567", "345678", "456789"};
            var expected = new string[] {"212345", "343456", "456767", "567898"};
            
            Level1Solved.MatrixTurn(arr, 4, 6, 1);

            CollectionAssert.AreEqual(expected, arr);
        }
        
        [Test]
        public void ThreeRotate()
        {
            string[] arr = new[] {"123456", "234567", "345678", "456789"};
            var expected = new string[] {"432123", "565434", "676545", "789876"};
            
            Level1Solved.MatrixTurn(arr, 4, 6, 3);

            CollectionAssert.AreEqual(expected, arr);
        }

        [Test]
        public void Test4()
        {
            string[] arr = new[] {"23", "74", "65"};
            var expected = new string[] {"56", "47", "32"};
            
            Level1Solved.MatrixTurn(arr, 3, 2, 3);

            CollectionAssert.AreEqual(expected, arr);
        }
        
        [Test]
        public void Test5()
        {
            string[] arr = new[] {"1234567", "2123458", "1412369","0365470","9210981","8765432"};
            var expected = new[] {"2123456", "1412347", "0361258","9254369","8109870","7654321"};
            
            Level1Solved.MatrixTurn(arr, 6, 7, 1);

            CollectionAssert.AreEqual(expected, arr);
        }
    }
}