using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class StackTests
    {
        [Test]
        public void Size_empty_zero()
        {
            Stack<int> stack = new Stack<int>();

            var result = stack.Size();
            
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void SizePush_notempty_correct()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);stack.Push(1);stack.Push(1);

            Assert.That(stack.Size(), Is.EqualTo(3));
            stack.Pop();
            Assert.That(stack.Size(), Is.EqualTo(2));
        }

        [Test]
        public void Pop_empty_null()
        {
            Stack<int> stack = new Stack<int>();
            var result = stack.Pop();
            
            Assert.That(result, Is.EqualTo(0));
        }
        
        [Test]
        public void Pop_notEmpty_null()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            var result = stack.Pop();
            
            Assert.That(result, Is.EqualTo(1));
            Assert.That(stack.Size(), Is.EqualTo(0));
        }

        [Test]
        public void Peek_Empty_null()
        {
            Stack<int> stack = new Stack<int>();
            var result = stack.Peek();
            
            Assert.That(result, Is.EqualTo(0));
        }
        
        [Test]
        public void Peek_NotEmpty_resultAndSizeDontChanged()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            var result = stack.Peek();
            
            Assert.That(result, Is.EqualTo(1));
            Assert.That(stack.Size(), Is.EqualTo(1));
        }

        [Test]
        [TestCase("())(", false)]
        [TestCase("))((", false)]
        [TestCase("((())", false)]
        [TestCase("(((())", false)]
        [TestCase("(()))", false)]
        [TestCase(")", false)]
        [TestCase("(", false)]
        [TestCase("()", true)]
        [TestCase("(())", true)]
        [TestCase("(()())", true)]
        [TestCase("((())((()())())", false)]
        [TestCase("((())((()())()))", true)]
        [TestCase("((()))", true)]
        public void BraceTaskTest(string s, bool expected)
        {
            Assert.That(StackTasks.BraceTask(s), Is.EqualTo(expected));
        }
    }
}