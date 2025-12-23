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

    [Fact]
    public void AddStepToSaga_NotThrowException()
    {
        var saga = new Saga();
        var step = new SagaStep("test", () => { });
        
        //Assert that not throw
        saga.AddStep(step);
    }
}