using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class MassVoteTests
    {
        [Test]
        public void Case1()
        {
            var res = Level1.MassVote(5, new[] {60, 10, 10, 15, 5});
            
            Assert.AreEqual("majority winner 1", res);
        }
        
        [Test]
        public void Case2()
        {
            var res = Level1.MassVote(5, new[] {10, 15, 10});
            
            Assert.AreEqual("minority winner 2", res);
        }
        
        [Test]
        public void Case3()
        {
            var res = Level1.MassVote(5, new[] {110, 111, 110, 111});
            
            Assert.AreEqual("no winner", res);
        }

        [Test]
        public void OneCandidate()
        {
            var res = Level1.MassVote(1, new[] {100});
            
            Assert.AreEqual("majority winner 1", res);
        }

        [Test]
        public void LittleMoreThan50Persents()
        {
            var res = Level1.MassVote(3, new[] {10, 50, 39});
            
            Assert.AreEqual("majority winner 2", res);
        }
        
        [Test]
        [TestCase(3, new[] {10, 39, 49}, 3)]
        [TestCase(4, new[] {5,5,  39, 49}, 4)]
        public void LittleLessThan50Persents(int n, int[] arr, int index)
        {
            var res = Level1.MassVote(n, arr);
            
            Assert.AreEqual($"minority winner {index}", res);
        }
    }
}