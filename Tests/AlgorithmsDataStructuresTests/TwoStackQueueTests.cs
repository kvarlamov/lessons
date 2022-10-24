using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class TwoStackQueueTests
    {
        [Test]
        public void Size_Empty()
        {
            TwoStackQueue<int> queue = new TwoStackQueue<int>();
            
            Assert.That(queue.Size(), Is.EqualTo(0));
        }

        [Test]
        public void Size_NotEmpty()
        {
            TwoStackQueue<int> queue = new TwoStackQueue<int>();
            queue.Enqueue(1);
            
            Assert.That(queue.Size(), Is.EqualTo(1));
            queue.Enqueue(2);
            Assert.That(queue.Size(), Is.EqualTo(2));
        }

        [Test]
        public void Enqueue_Dequue()
        {
            TwoStackQueue<int> queue = new TwoStackQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            
            Assert.That(queue.Dequeue(), Is.EqualTo(1));
            Assert.That(queue.Size(), Is.EqualTo(1));
            Assert.That(queue.Dequeue(), Is.EqualTo(2));
            Assert.That(queue.Size(), Is.EqualTo(0));
            Assert.That(queue.Dequeue(), Is.EqualTo(0));
        }

        [Test]
        public void SpinTwoStackQueue()
        {
            TwoStackQueue<int> queue = new TwoStackQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);

            var res = queue.SpinQueue(1);
            Assert.That(res[0], Is.EqualTo(1));

            res = queue.SpinQueue(4);
            Assert.That(res[0], Is.EqualTo(5));
            Assert.That(res[4], Is.EqualTo(1));
        }
    }
}