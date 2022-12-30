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
            yield return new TestCaseData(new List<int>(){ 2,4,5,2,4,3 }, 4);
            yield return new TestCaseData(new List<int>(){ 2,2,2,3 }, 2);
            yield return new TestCaseData(new List<int>(){ 2,4,2,3, 6, 1, 2,6, 5 }, 6);
            yield return new TestCaseData(new List<int>() {0, 0, 0, 0, 0, 0}, 0);
            yield return new TestCaseData(new List<int>() {1, 2, 2, 3, 4}, 3);
            yield return new TestCaseData(new List<int>() {1, 2}, 1);
            yield return new TestCaseData(new List<int>() {2, 1}, 1);
            yield return new TestCaseData(new List<int>() {1, 1}, 1);
            yield return new TestCaseData(new List<int>() {-1000, -10000, 5, 31, 8, -1500}, 8);
            yield return new TestCaseData(new List<int>() {-1000, -10000, int.MinValue, -1500}, -1500);
            yield return new TestCaseData(new List<int>() {int.MinValue, int.MinValue}, int.MinValue);
            yield return new TestCaseData(new List<int>() {int.MinValue, 1}, int.MinValue);
            yield return new TestCaseData(new List<int>() {int.MaxValue, 1}, 1);
            yield return new TestCaseData(new List<int>() {5,4,3,2,1}, 4);
            yield return new TestCaseData(new List<int>() {5,5,3,2,1}, 5);
            yield return new TestCaseData(new List<int>() {5,3,2,1}, 3);
            yield return new TestCaseData(new List<int>() {5,2,1}, 2);
            yield return new TestCaseData(new List<int>() {5,1, 5}, 5);
            yield return new TestCaseData(new List<int>() {5,1}, 1);

        }
    }
}