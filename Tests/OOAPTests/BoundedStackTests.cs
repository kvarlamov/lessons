using NUnit.Framework;
using OOAP;

namespace OOAPTests;

public class BoundedStackTests
{
    [Test]
    public void BoundedStack_StackIsEmptyAndTryPop_StateIsError()
    {
        // Arrange
        var stack = new BoundedStackImpl<int>(2);
        
        // Act
        stack.Pop();

        // Assert
        Assert.That(stack.GetPopStatus(), Is.EqualTo(BoundedStack<int>.POP_ERR));
    }
    
    [Test]
    public void BoundedStack_StackIsEmptyAndTryPeek_StateIsError()
    {
        // Arrange
        var stack = new BoundedStackImpl<int>(2);
        
        // Act
        stack.Peek();

        // Assert
        Assert.That(stack.GetPeekStatus(), Is.EqualTo(BoundedStack<int>.PEEK_ERR));
    }
    
    [Test]
    public void BoundedStack_StackNotEmpty_PeekReturnLastAndStatusOk()
    {
        // Arrange
        var stack = new BoundedStackImpl<int>(2);
        var elem = 1;
        
        // Act
        stack.Push(elem);
        var res = stack.Peek();

        // Assert
        Assert.That(stack.GetPeekStatus(), Is.EqualTo(BoundedStack<int>.PEEK_OK));
        Assert.That(res, Is.EqualTo(elem));
    }
    
    [Test]
    public void BoundedStack_StackNotEmpty_PopRemoveElem()
    {
        // Arrange
        var stack = new BoundedStackImpl<int>(2);
        var elem = 1;
        var second = 2;
        
        // Act
        stack.Push(second);
        stack.Push(elem);
        stack.Pop();
        var res = stack.Peek();

        // Assert
        Assert.That(stack.GetPeekStatus(), Is.EqualTo(BoundedStack<int>.PEEK_OK));
        Assert.That(res, Is.EqualTo(second));
    }

    [Test]
    public void BoundedStack_StackBecomeEmpty_Err()
    {
        // Arrange
        var stack = new BoundedStackImpl<int>(2);
        var elem = 1;
        
        // Act
        stack.Push(elem);
        stack.Pop();

        // Assert
        Assert.That(stack.GetPopStatus(), Is.EqualTo(BoundedStack<int>.POP_OK));
        Assert.That(stack.Size(), Is.EqualTo(0));
        stack.Pop();
        Assert.That(stack.GetPopStatus(), Is.EqualTo(BoundedStack<int>.POP_ERR));
        stack.Clear();
        Assert.That(stack.GetPopStatus(), Is.EqualTo(BoundedStack<int>.POP_NIL));
    }

    [Test]
    [TestCase(2)]
    [TestCase(0)]
    public void BoundedStack_MaxSize_Err(int size)
    {
        // Arrange
        var stack = size != 0
            ? new BoundedStackImpl<int>(2)
            : new BoundedStackImpl<int>();

        if (size == 0)
            size = 32;

        // Act Assert
        Assert.That(stack.GetPushStatus(), Is.EqualTo(BoundedStack<int>.PUSH_NIL));

        for (int i = 0; i < size; i++)
        {
            stack.Push(i);
        }
        
        Assert.That(stack.GetPushStatus(), Is.EqualTo(BoundedStack<int>.PUSH_OK));
        
        stack.Push(1);
        Assert.That(stack.GetPushStatus(), Is.EqualTo(BoundedStack<int>.PUSH_ERR));
        stack.Push(1);
        Assert.That(stack.GetPushStatus(), Is.EqualTo(BoundedStack<int>.PUSH_ERR));
        Assert.That(stack.Size(), Is.EqualTo(size));
        
        stack.Pop();
        Assert.That(stack.GetPopStatus(), Is.EqualTo(BoundedStack<int>.POP_OK));
        stack.Push(1);
        Assert.That(stack.GetPushStatus(), Is.EqualTo(BoundedStack<int>.PUSH_OK));
    }
}