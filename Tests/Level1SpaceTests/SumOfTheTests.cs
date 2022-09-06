using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class SumOfTheTests
    {
        [Test]
        [TestCase(new int[]{5, -25, 10, -35, -45}, -45)]
        [TestCase(new int[] {100, 10, 90, -35, -25, -50, 90}, 90)]
        [TestCase(new int[] {10, -25, -45, -35, 5}, -45)]
        [TestCase(new int[] {5, 5}, 5)]
        [TestCase(new int[]{-500, 200, -50, 120,939,-90,70,-300,1000,-200,-199,888}, 939)]
        [TestCase(new int[]{-80, 20, -20, 50,50,-20,40}, 20)]
        [TestCase(new int[]{-80, 20, -20,-40, 0, 50,50,-20,40}, 0)]
        [TestCase(new int[]{-80, 20, -20,-40, -20, -20, 50,50,40,-20}, -20)]
        [TestCase(new int[] {-200, -200}, -200)]
        [TestCase(new int[] {-200, -400, -200}, -400)]
        [TestCase(new int[] {1, -3, 2,5, -3, 8, -6}, 2)]
        public void Test1(int[] data, int expected)
        {
            var result = Level1Solved.SumOfThe(data.Length, data);
            var testResult = Test(data.Length, data);
            
            Assert.AreEqual(expected, result);
            Assert.AreEqual(result, testResult);
        }
        
        private int Test(int N, int[] data)
        {
            for (int i = 0; i < N; i++)
            {
                var totalSum = 0;
                for (int j = 0; j < N; j++)
                {
                    if (j != i)
                    {
                        totalSum += data[j];
                    }
                }

                if (data[i] == totalSum)
                    return data[i];
            }

            return 0;
        }
    }
}