namespace CircuitBreaker;

public sealed class CircuitBreaker
{
    // количество попыток до разрыва
    private readonly int _failureThreshold;
    // продолжительность разрыва
    private readonly TimeSpan _openDuration;
    // счётчик текущих попыток
    private int _openFailures;
    private bool _isOpen;
    private DateTime _openTime;

    public CircuitBreaker(int failureThreshold, TimeSpan? openDuration = null)
    {
        _failureThreshold = failureThreshold;
        _openDuration = openDuration ?? TimeSpan.FromSeconds(5);;
    }

    public async Task ExecuteAsync(Func<Task> operation)
    {
        if (_isOpen)
        {
            // если время разрыва закончилось
            if (DateTime.UtcNow > _openTime + _openDuration)
            {
                ResetState();
            }
            else
            {
                throw new InvalidOperationException("The circuit is open. Please wait");
            }
        }

        try
        {
            await operation();
            OnSuccess();
        }
        catch (Exception exception)
        {
            OnFailure();
            throw;
        }
    }

    private void OnSuccess()
    {
        _isOpen = false;
        _openFailures = 0;
    }

    private void OnFailure()
    {
        _openFailures++;
        if (_openFailures >= _failureThreshold)
        {
            _isOpen = true;
            _openTime = DateTime.UtcNow;
        }
    }

    private void ResetState()
    {
        _isOpen = false;
        _openFailures = 0;
    }

    /// <summary>
    /// POST: цепь разорвана (_isOpen=true)
    /// </summary>
    public void ManualOpen()
    {
        _isOpen = true;
        _openTime = DateTime.UtcNow;
    }

    public bool GetState()
    {
        return _isOpen;
    }

    public int GetFailureCount()
    {
        return _openFailures;
    }
}