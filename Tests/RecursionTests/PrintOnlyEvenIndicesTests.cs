using System.Collections.Generic;
using NUnit.Framework;
using Recursion;

namespace RecursionTests
{
    [TestFixture]
    public class PrintOnlyEvenIndicesTests
    {
        [Test]
        public void Test()
        {
            PrintOnlyEvenIndices<int>.Print(new List<int>(){ 1,2,3,4,5,6,7,8});
            TestContext.WriteLine("\n***");
            PrintOnlyEvenIndices<int>.Print(new List<int>(){ 1});
            TestContext.WriteLine("\n***");
            PrintOnlyEvenIndices<string>.Print(new List<string>(){"1","2","3","4"});
            TestContext.WriteLine("\n***");
            PrintOnlyEvenIndices<string>.Print(new List<string>(){"1","2","3"});
            TestContext.WriteLine("\n***");
            PrintOnlyEvenIndices<int>.Print(new List<int>(){0, 1,2,3,4,5,6,7,8,9,10});
        }
    }
}