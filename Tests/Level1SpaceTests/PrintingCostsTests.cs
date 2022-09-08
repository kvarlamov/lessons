using System.Globalization;
using System.Threading;
using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class PrintingCostsTests
    {
        [Test]
        public void StrangeChar()
        {
            CultureInfo ci = new CultureInfo("ru-Ru");
            Thread.CurrentThread.CurrentCulture = ci;
            string str = "☺ ";
            
            var res = Level1Solved.PrintingCosts(str);
            
            Assert.AreEqual(23, res);
        }

        [Test]
        [TestCase(@" &,28>DJPV\bhntz", 282)]
        [TestCase(@"!'-39?EKQW]ciou{", 292)]
        [TestCase(@"""(.4:@FLRX^djpv|", 267)]
        [TestCase(@"#)/5;AGMSY_ekqw}", 314)]
        [TestCase(@"$*06<BHNTZ`flrx~", 293)]
        [TestCase(@"%+17=CIOU[agmsy", 309)]
        public void AllCharsIsAscii(string column, int expected)
        {
            CultureInfo ci = new CultureInfo("ru-Ru");
            Thread.CurrentThread.CurrentCulture = ci;

            var res = Level1Solved.PrintingCosts(column);
            
            Assert.AreEqual(expected, res);
        }
        
    }
}