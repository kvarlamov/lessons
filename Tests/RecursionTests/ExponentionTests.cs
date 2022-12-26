using System.Collections.Generic;
using NUnit.Framework;
using Recursion;

namespace RecursionTests
{
    [TestFixture]
    public class ExponentionTests
    {
        [Test, TestCaseSource(nameof(GetData))]
        public void Test(int n, int m, long expected)
        {
            Assert.That(exponentiation.Exp(n,m), Is.EqualTo(expected));
        }

        private static IEnumerable<TestCaseData> GetData()
        {
            yield return new TestCaseData(2, 3, 8);
            yield return new TestCaseData(2, 4, 16);
            yield return new TestCaseData(2, 1, 2);
            yield return new TestCaseData(2, 0, 1);
            yield return new TestCaseData(3, 2, 9);
            yield return new TestCaseData(3, 5, 243);
        }
    }
}