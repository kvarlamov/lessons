using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class DequeTests
    {
        [Test]
        public void DequeLikeStack()
        {
            Deque<int> deque = new Deque<int>();
            Assert.That(deque.Size(), Is.EqualTo(0));
            
            deque.AddFront(1);
            Assert.That(deque.Size(), Is.EqualTo(1));
            deque.AddFront(2);
            Assert.That(deque.Size(), Is.EqualTo(2));
            Assert.That(deque.RemoveFront(), Is.EqualTo(2));
            deque.AddFront(1);
            deque.AddTail(3);
            deque.AddTail(4);
            Assert.That(deque.RemoveTail(), Is.EqualTo(4));
            Assert.That(deque.Size(), Is.EqualTo(3));
            Assert.That(deque.RemoveFront(), Is.EqualTo(1));
            Assert.That(deque.Size(), Is.EqualTo(2));
            Assert.That(deque.RemoveFront(), Is.EqualTo(1));
            Assert.That(deque.RemoveFront(), Is.EqualTo(3));
            Assert.That(deque.RemoveTail(), Is.EqualTo(0));
            Assert.That(deque.RemoveFront(), Is.EqualTo(0));
        }

        [Test]
        public void DequeLikeStack2()
        {
            Deque<int> deque = new Deque<int>();
            deque.AddFront(1);
            deque.AddFront(2);
            deque.AddFront(3);
            Assert.That(deque.RemoveFront(), Is.EqualTo(3));
            Assert.That(deque.RemoveFront(), Is.EqualTo(2));
            Assert.That(deque.RemoveFront(), Is.EqualTo(1));
            
            deque.AddTail(1);
            deque.AddTail(2);
            deque.AddTail(3);
            Assert.That(deque.RemoveTail(), Is.EqualTo(3));
            Assert.That(deque.RemoveTail(), Is.EqualTo(2));
            Assert.That(deque.RemoveTail(), Is.EqualTo(1));
        }

        [Test]
        public void DequeLikeQueue()
        {
            Deque<int> deque = new Deque<int>();
            deque.AddFront(1);
            deque.AddFront(2);
            deque.AddFront(3);
            Assert.That(deque.RemoveTail(), Is.EqualTo(1));
            Assert.That(deque.RemoveTail(), Is.EqualTo(2));
            Assert.That(deque.RemoveTail(), Is.EqualTo(3));
            Assert.That(deque.Size(), Is.EqualTo(0));
            
            deque.AddTail(1);
            deque.AddTail(2);
            deque.AddTail(3);
            Assert.That(deque.RemoveFront(), Is.EqualTo(1));
            Assert.That(deque.RemoveFront(), Is.EqualTo(2));
            Assert.That(deque.RemoveFront(), Is.EqualTo(3));
        }

        [Test]
        [TestCase("121", true)]
        [TestCase("1221", true)]
        [TestCase("Alula", true)]
        [TestCase("Civic", true)]
        [TestCase("Madam", true)]
        [TestCase("Noon", true)]
        [TestCase("My gym", true)]
        [TestCase("Don’t nod", true)]
        [TestCase("Was it a cat I saw?", true)]
        [TestCase("Step on no pets.", true)]
        [TestCase("Do geese see God", true)]
        [TestCase("abc", false)]
        [TestCase("ab", false)]
        [TestCase("aa", true)]
        [TestCase("false", false)]
        [TestCase("No, Mel Gibson is a casino’s big lemon", true)]
        [TestCase("Cigar? Toss it in a can. It is so tragic", true)]
        [TestCase("No, sir, prefer prison", true)]
        [TestCase("Naomi, did I moan?", true)]
        public void IsPalindrome(string s, bool expected)
        {
            Assert.That(DequeTasks.IsPalindrome(s), Is.EqualTo(expected)); 
        }
    }
}