using FluentAssertions;

namespace CircuitBreaker.Tests;

public class UnitTest1
{
    private const int ThreshholdFailure = 3;
    private const int OpenTimeSeconds = 5;
    private readonly CircuitBreaker _breaker;

    public UnitTest1()
    {
        _breaker = new CircuitBreaker(ThreshholdFailure, TimeSpan.FromSeconds(OpenTimeSeconds));
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
        await Task.Delay(OpenTimeSeconds);
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
        _breaker.GetState().Should().BeFalse();
    }
    
    [Fact]
    public async Task OnSuccess_ResetFailure()
    {
        await Assert.ThrowsAnyAsync<Exception>(() => _breaker.ExecuteAsync(() => throw new InvalidOperationException()));
        _breaker.GetFailureCount().Should().Be(1);
        
        await _breaker.ExecuteAsync(() => Task.CompletedTask);
        _breaker.GetState().Should().BeFalse();
        _breaker.GetFailureCount().Should().Be(0);
    }

    [Fact]
    public async Task OnFailure_Threshhold_ClosedAfterFailureAndTimePass()
    {
        for (var i = 0; i < ThreshholdFailure; i++)
        {
            await Assert.ThrowsAnyAsync<Exception>(() => _breaker.ExecuteAsync(() => throw new InvalidOperationException()));
            _breaker.GetFailureCount().Should().Be(i+1);
        }
        
        _breaker.GetState().Should().BeTrue();
        await Assert.ThrowsAsync<InvalidOperationException>(() => _breaker.ExecuteAsync(() => Task.CompletedTask));
        await Task.Delay(TimeSpan.FromSeconds(OpenTimeSeconds));
        await _breaker.ExecuteAsync(() => Task.CompletedTask);
        _breaker.GetState().Should().BeFalse();
        _breaker.GetFailureCount().Should().Be(0);
    }
    
    [Fact]
    public async Task OnFailure_Threshhold_OpenState()
    {
        for (var i = 0; i < ThreshholdFailure; i++)
        {
            await Assert.ThrowsAnyAsync<Exception>(() => _breaker.ExecuteAsync(() => throw new InvalidOperationException()));
            _breaker.GetFailureCount().Should().Be(i+1);
        }
        
        _breaker.GetState().Should().BeTrue();
        await Assert.ThrowsAsync<InvalidOperationException>(() => _breaker.ExecuteAsync(() => Task.CompletedTask));
    }
}