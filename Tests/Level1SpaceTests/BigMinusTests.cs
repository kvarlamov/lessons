using System;
using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class BigMinusTests
    {
        [Test]
        [TestCase("76","105", "29")]
        [TestCase("150","11", "139")]
        [TestCase("100","55", "45")]
        [TestCase("55","190", "135")]
        public void SomeTestsSmallNumbers(string s1, string s2, string expected)
        {
            var res = Level1.BigMinus(s1, s2);
            
            Assert.AreEqual(expected, res);
            Assert.AreEqual(LongCalc(long.Parse(s1), long.Parse(s2)), res);
        }

        [Test]
        [TestCase("100","190", "90")]
        [TestCase("2147483648","2547483648", "400000000")]
        [TestCase("2147483648","8147483648", "6000000000")]
        [TestCase("2147483645","2147483648", "3")]
        [TestCase("2147483645","2147483645", "0")]
        [TestCase("22","22", "0")]
        public void EqualLenghStrings(string s1, string s2, string expected)
        {
            var res = Level1.BigMinus(s1, s2);
            
            Assert.AreEqual(expected, res);
            Assert.AreEqual(LongCalc(long.Parse(s1), long.Parse(s2)), res);
        }

        [Test]
        [TestCase("1234567891","1", "1234567890")]
        [TestCase("1","1234567891", "1234567890")]
        [TestCase("1","321", "320")]
        public void TestFromTask(string s1, string s2, string expected)
        {
            var res = Level1.BigMinus(s1, s2);
            
            Assert.AreEqual(expected, res);
            Assert.AreEqual(LongCalc(long.Parse(s1), long.Parse(s2)), res);
        }
        
        [Test]
        [TestCase("2147483648","1", "2147483647")]
        [TestCase("2147483648","9", "2147483639")]
        [TestCase("1000000055555555","1", "1000000055555554")]
        [TestCase("1000000055555555","55555555", "1000000000000000")]
        [TestCase("1111111111111111","111111111111111", "1000000000000000")]
        public void LongStrings(string s1, string s2, string expected)
        {
            var res = Level1.BigMinus(s1, s2);
            
            Assert.AreEqual(expected, res);
            Assert.AreEqual(LongCalc(long.Parse(s1), long.Parse(s2)), res);
        }

        private string LongCalc(long a, long b)
        {
            var res = Math.Abs(a - b);
            return res.ToString();
        }
    }
}