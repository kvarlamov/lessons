using System;
using System.Collections.Generic;
using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class UFOTests
    {
        [Test]
        public void HexTest1()
        {
            var data = new[] {1234, 1777};
            var res = Level1Solved.UFO(2, data, false);
            
            Assert.AreEqual(new int [] {4660,6007}, res);
            Assert.AreEqual(TestCorrect(new[] {1234, 1777}, false), res);
        }
        
        [Test]
        public void OctTest1()
        {
            var data = new[] {1234, 1777};
            var expectedFromTest = TestCorrect(data, true);
            var res = Level1Solved.UFO(2, data, true);
            
            Assert.AreEqual(new int [] {668,1023}, res);
            Assert.AreEqual(expectedFromTest, res);
        }
        
        [Test]
        [TestCase(new[] {9999, 896373}, false, new[] {39321, 9003891})]
        [TestCase(new[] {7777, 123434}, true, new[] {4095, 42780})]
        [TestCase(new[] {743223}, false, new[] {7615011})]
        [TestCase(new[] {1000, 100001}, false, new[] {4096, 1048577})]
        [TestCase(new[] {1000, 100001}, true, new[] {512, 32769})]
        public void Test2(int[] data, bool flag, int[] expectedRes)
        {
            var expectedFromTest = TestCorrect(data, flag);
            var res = Level1Solved.UFO(data.Length, data, flag);
            
            Assert.AreEqual(expectedRes, res);
            Assert.AreEqual(expectedFromTest, res);
        }

        private int[] TestCorrect(int[] data, bool flag)
        {
            List<int> result = new List<int>();

            foreach (var i in data)
            {
                if (flag)
                    result.Add(Convert.ToInt32(i.ToString(), 8));
                else
                    result.Add(int.Parse(i.ToString(), System.Globalization.NumberStyles.HexNumber));
            }

            return result.ToArray();
        }
    }
}