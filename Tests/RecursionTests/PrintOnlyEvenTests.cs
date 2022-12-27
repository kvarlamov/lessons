using System.Collections.Generic;
using NUnit.Framework;
using Recursion;

namespace RecursionTests
{
    [TestFixture]
    public class PrintOnlyEvenTests
    {
        [Test]
        public void Test()
        {
            PrintOnlyEven.EvenPrint(new List<int>(){ 1,2,3,4,5,6,7,8});
            TestContext.WriteLine("\n***");
            PrintOnlyEven.EvenPrint(new List<int>(){ 1,2,3,4,5,6,7});
            TestContext.WriteLine("\n***");
            PrintOnlyEven.EvenPrint(new List<int>(){ 1,2});
            TestContext.WriteLine("\n***");
            PrintOnlyEven.EvenPrint(new List<int>(){ 1});
            TestContext.WriteLine("\n***");
            PrintOnlyEven.EvenPrint(new List<int>(){ 2});
        }
    }
}