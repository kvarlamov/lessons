using System.Collections.Generic;
using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class BloomFilterTests
    {
        [Test, TestCaseSource(nameof(GetStrings))]
        public void Test(List<string> list, bool expected)
        {
            BloomFilter filter = new BloomFilter(32);

            foreach (var s in list)
            {
                filter.Add(s);
            }

            foreach (var s in list)
            {
                Assert.That(filter.IsValue(s), Is.True);
            }

            foreach (var s in list)
            {
                var smod = s + "t";
                Assert.IsFalse(filter.IsValue(smod));
            }
        }

        private static IEnumerable<object[]> GetStrings()
        {
            yield return new object[]
            {
                new List<string>
                {
                    "0123456789"
                },
                true
            } ;
            
            yield return new object[]
            {
                new List<string>
                {
                    "0123456789",
                    "1234567890",
                    "2345678901",
                    "3456789012",
                    "4567890123",
                    "5678901234",
                    "6789012345",
                    "7890123456",
                    "8901234567",
                    "9012345678"
                },
                true
            } ;
        }
    }
}