using System.Collections.Generic;
using NUnit.Framework;
using Recursion;

namespace RecursionTests
{
    [TestFixture]
    public class ListLenTests
    {
        [Test, TestCaseSource(nameof(GetStringData))]
        public void Test(List<string> list, int exp)
        {
            var res = MyListLength<string>.GetLength(list);
            Assert.That(res, Is.EqualTo(exp));
        }

        private static IEnumerable<TestCaseData> GetStringData()
        {
            yield return new TestCaseData(new List<string>() {"1","2", "3","4" }, 4);
            yield return new TestCaseData(new List<string>() {"1","2" }, 2);
            yield return new TestCaseData(new List<string>() {"1" }, 1);
            yield return new TestCaseData(new List<string>() , 0);
            yield return new TestCaseData(new List<string>() {"1","2", "3","4", "1","2", "3","4" }, 8);
        }
    }
}