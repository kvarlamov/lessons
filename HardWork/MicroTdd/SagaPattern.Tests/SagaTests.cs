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

    [Fact]
    public void Run_StepsExecuted()
    {
        bool executed = false;
        var saga = new Saga();
        var step = new SagaStep("test", () => { executed = true; });
        saga.AddStep(step);
        
        saga.Run();
        executed.Should().BeTrue();
    }

    [Fact]
    public void Run_ExecutionOrder()
    {
        var stepNames =  new List<string>();
        var saga = new Saga();
        var step1 = new SagaStep("1", () => { stepNames.Add("1"); });
        var step2 = new SagaStep("2", () => { stepNames.Add("2"); });
        saga.AddStep(step1);
        saga.AddStep(step2);
        
        saga.Run();
        
        stepNames.Should().HaveCount(2);
        stepNames.Should().BeEquivalentTo("1", "2");
    }

    [Fact]
    public void Run_WithException_StopOnError()
    {
        var stepNames =  new List<string>();
        var saga = new Saga();
        var step1 = new SagaStep("1", () => { stepNames.Add("1"); });
        var step2 = new SagaStep("2", () => throw new InvalidOperationException());
        var step3 = new SagaStep("3", () => { stepNames.Add("3"); });
        saga.AddStep(step1);
        saga.AddStep(step2);
        saga.AddStep(step3);
        
        saga.Run();
        
        stepNames.Should().BeEquivalentTo("1");
    }

    [Fact]
    public void Run_WithCompensation_Compensated()
    {
        var compensations = new List<string>();
        var saga = new Saga();
        var step1 = new SagaStep("1", () => { }, () => { compensations.Add("Compensate 1"); });
        var step2 = new SagaStep("2", () => throw new InvalidOperationException(), () => { compensations.Add("Compensate 2"); });
        var step3 = new SagaStep("3", () => { }, () => { compensations.Add("Compensate 3"); });
        saga.AddStep(step1);
        saga.AddStep(step2);
        saga.AddStep(step3);
        
        saga.Run();
        
        compensations.Should().BeEquivalentTo("Compensate 1");
    }
    
    [Fact]
    public void Run_WithCompensation_CompensatedInOrder()
    {
        var compensations = new List<string>();
        var saga = new Saga();
        var step1 = new SagaStep("1", () => { }, () => { compensations.Add("Compensate 1"); });
        var step2 = new SagaStep("2", () => { }, () => { compensations.Add("Compensate 2"); });
        var step3 = new SagaStep("3", () => throw new InvalidOperationException(), () => { compensations.Add("Compensate 3"); });
        saga.AddStep(step1);
        saga.AddStep(step2);
        saga.AddStep(step3);
        
        saga.Run();
        
        compensations.Should().BeEquivalentTo("Compensate 2", "Compensate 1");
    }
}