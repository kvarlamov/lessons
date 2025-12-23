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

public sealed class Saga
{
    private readonly List<SagaStep>  _steps = new();

    public void AddStep(SagaStep step)
    {
        _steps.Add(step);
    }

    public void Run()
    {
        foreach (var step in _steps)
        {
            step.Execute();
        }
    }
}