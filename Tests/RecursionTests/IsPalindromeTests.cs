using System.Collections.Generic;
using NUnit.Framework;
using Recursion;

namespace RecursionTests
{
    [TestFixture]
    public class IsPalindromeTests
    {
        [Test, TestCaseSource(nameof(GetData))]
        public void Test(string s, bool exp)
        {
            Assert.That(IsPalindrome.CheckString(s), Is.EqualTo(exp));
        }

        private static IEnumerable<TestCaseData> GetData()
        {
            yield return new TestCaseData("1221", true);
            yield return new TestCaseData("казак", true);
            yield return new TestCaseData("121", true);
            yield return new TestCaseData("12", false);
            yield return new TestCaseData("1234554321", true);
            yield return new TestCaseData("123454321", true);
            yield return new TestCaseData("1234554421", false);
            
        }
    }
}