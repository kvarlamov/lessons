using System.Collections.Generic;
using NUnit.Framework;
using Recursion;

namespace RecursionTests
{
    [TestFixture]
    public class FindSecondMaxTests
    {
        [Test, TestCaseSource(nameof(GetData))]
        public void Test(List<int> list, int expected)
        {
            Assert.That(FindSecondMax.Run(list), Is.EqualTo(expected));
        }

        private static IEnumerable<TestCaseData> GetData()
        {
            yield return new TestCaseData(new List<int>(){ 5,4,3,2,5 }, 5);
            yield return new TestCaseData(new List<int>(){ 2,4,2,2,4,3 }, 4);
            yield return new TestCaseData(new List<int>(){ 2,4,5,2,4,3 }, -1);
            yield return new TestCaseData(new List<int>(){ 2,2,2,3 }, -1);
            yield return new TestCaseData(new List<int>(){ 2,4,2,3, 6, 1, 2,6, 5 }, 6);
            yield return new TestCaseData(new List<int>() {0, 0, 0, 0, 0, 0}, 0);
        }
    }
}