namespace SagaPattern;

public sealed class SagaStep
{
    public string Name { get; }
    public Action Execute { get; }
    public Action? Compensate { get; }

    public SagaStep(string name, Action execute, Action? compensate = null)
    {
        Name = name;
        Execute = execute;
        Compensate = compensate;
    }
}

public class SagaExecutionException : Exception
{
    public IReadOnlyList<Exception> CompensationErrors { get; }
    
    public SagaExecutionException(
        string message, 
        Exception innerException, 
        List<Exception> compensationErrors)
        : base(message, innerException)
    {
        CompensationErrors = compensationErrors;
    }
}

public enum SagaStatus
{
    NotStarted,
    InProgress,
    Completed,
    Failed,
    Compensated
}

public sealed class Saga
{
    private readonly List<SagaStep>  _steps = new();
    private readonly List<SagaStep> _completedSteps = new();
    private readonly List<Exception> _compensationErrors = new();
    public SagaStatus Status { get; private set; } = SagaStatus.NotStarted;
    
    public IReadOnlyList<Exception> CompensationErrors => _compensationErrors;

    public void AddStep(SagaStep step)
    {
        _steps.Add(step);
    }

    public void Run()
    {
        if (Status != SagaStatus.NotStarted)
        {
            throw new InvalidOperationException("Saga can only be run once");
        }
        
        Status = SagaStatus.InProgress;
        foreach (var step in _steps)
        {
            try
            {
                step.Execute();
                _completedSteps.Add(step);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Compensate();
                Status = _compensationErrors.Count == 0 
                    ? SagaStatus.Compensated 
                    : SagaStatus.Failed;
                
                throw new SagaExecutionException(
                    "Saga failed during execution", 
                    ex, 
                    _compensationErrors);
            }
            
            Status = SagaStatus.Completed;
        }
    }

    private void Compensate()
    {
        foreach (var step in _completedSteps.AsEnumerable().Reverse())
        {
            try
            {
                step.Compensate?.Invoke();
            }
            catch (Exception ex)
            {
                _compensationErrors.Add(ex);
            }
        }
    }
}