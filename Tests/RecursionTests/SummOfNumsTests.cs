using System.Collections.Generic;
using NUnit.Framework;
using Recursion;

namespace RecursionTests
{
    [TestFixture]
    public class SummOfNumsTests
    {
        [Test, TestCaseSource(nameof(GetData))]
        public void Test(int n, int exp)
        {
            Assert.That(DigitNumberSumm.SummOfNums(n), Is.EqualTo(exp));
        }

        private static IEnumerable<TestCaseData> GetData()
        {
            yield return new TestCaseData(123, 6);
            yield return new TestCaseData(1, 1);
            yield return new TestCaseData(0, 0);
            yield return new TestCaseData(5690, 20);
            yield return new TestCaseData(11111, 5);
            yield return new TestCaseData(1111111111, 10);
            yield return new TestCaseData(100000001, 2);
            yield return new TestCaseData(100000000, 1);
            yield return new TestCaseData(100000300, 4);
        }

    }
}