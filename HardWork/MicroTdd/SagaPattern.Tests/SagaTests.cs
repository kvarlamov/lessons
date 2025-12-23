using FluentAssertions;

namespace SagaPattern.Tests;

public class SagaTests
{
    [Fact]
    public void ContructorTest()
    {
        var saga = new Saga();
        saga.Should().NotBeNull();
    }
}