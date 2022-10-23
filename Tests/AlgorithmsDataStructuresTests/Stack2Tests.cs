using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class Stack2Tests
    {
        [Test]
        public void Size_empty_zero()
        {
            Stack2<int> stack = new Stack2<int>();

            var result = stack.Size();
            
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void SizePush_notempty_correct()
        {
            Stack2<int> stack = new Stack2<int>();
            stack.Push(1);stack.Push(1);stack.Push(1);

            Assert.That(stack.Size(), Is.EqualTo(3));
            stack.Pop();
            Assert.That(stack.Size(), Is.EqualTo(2));
        }

        [Test]
        public void Pop_empty_null()
        {
            Stack2<int> stack = new Stack2<int>();
            var result = stack.Pop();
            
            Assert.That(result, Is.EqualTo(0));
        }
        
        [Test]
        public void Pop_notEmpty_null()
        {
            Stack2<int> stack = new Stack2<int>();
            stack.Push(1);
            var result = stack.Pop();
            
            Assert.That(result, Is.EqualTo(1));
            Assert.That(stack.Size(), Is.EqualTo(0));
        }

        [Test]
        public void Peek_Empty_null()
        {
            Stack2<int> stack = new Stack2<int>();
            var result = stack.Peek();
            
            Assert.That(result, Is.EqualTo(0));
        }
        
        [Test]
        public void Peek_NotEmpty_resultAndSizeDontChanged()
        {
            Stack2<int> stack = new Stack2<int>();
            stack.Push(1);
            var result = stack.Peek();
            
            Assert.That(result, Is.EqualTo(1));
            Assert.That(stack.Size(), Is.EqualTo(1));
        }
    }
}