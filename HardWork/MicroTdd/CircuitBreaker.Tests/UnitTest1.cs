using FluentAssertions;

namespace CircuitBreaker.Tests;

public class UnitTest1
{
    private readonly CircuitBreaker _breaker;

    public UnitTest1()
    {
        _breaker = new CircuitBreaker(3, TimeSpan.FromSeconds(5));
    }
    
    [Fact]
    public async Task ResetStateIfTimePassed()
    {
        // Arrange
        _breaker.ManualOpen();
        _breaker.GetState().Should().BeTrue();
        
        await _breaker.ExecuteAsync(() => Task.CompletedTask);
        _breaker.GetState().Should().BeTrue();
        
        //wait to close again
        await Task.Delay(5000);
        await _breaker.ExecuteAsync(() => Task.CompletedTask);
        _breaker.GetState().Should().BeFalse();
    }

    [Fact]
    public async Task StateIsOpen_ThrowException()
    {
        // Arrange
        _breaker.ManualOpen();
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _breaker.ExecuteAsync(() => Task.CompletedTask));
    }

    [Fact]
    public async Task OnSuccess()
    {
        await _breaker.ExecuteAsync(() => Task.CompletedTask);
        
        _breaker.GetState().Should().BeFalse();
        _breaker.GetFailureCount().Should().Be(0);
    }

    [Fact]
    public async Task OnFailure()
    {
        await Assert.ThrowsAnyAsync<Exception>(() => _breaker.ExecuteAsync(() => throw new InvalidOperationException()));
        _breaker.GetFailureCount().Should().Be(1);
    }
}