using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class LineAnalysisTests
    {
        [Test]
        [TestCase("*..*..*..*..*..*..*", true)]
        [TestCase("*..*...*..*..*..*..*", false)]
        [TestCase("*..*..*..*..*..**..*", false)]
        [TestCase("*", true)]
        [TestCase("***", true)]
        [TestCase("**", true)]
        [TestCase("*.......*.......*", true)]
        [TestCase("*.*", true)]
        [TestCase("*.*.*.*.*.*.*.*.*.*", true)]
        public void TestEx(string line, bool expected)
        {
            var result = Level1.LineAnalysis(line);
        }
    }
}