1) В наших проектах весьма часто применяется следующий шаблон:
Какой-то набор данных, приходящий в метод как асинхронный стрим,
укладывается в некоторый буффер (размер определяется через конфигурацию)
и далее это обрабатывается.
Примеры:
Before:
private async Task HandleAnalyticItemsStream(
        AnalyticSubJob analyticSubJob,
        IAsyncEnumerable<TAnalyticItem> analyticItems,
        CancellationToken cancellationToken)
{
	var buffer = new List<TAnalyticItem>(SaveBufferSize);

	await foreach (TAnalyticItem item in analyticItems.WithCancellation(cancellationToken))
	{
		buffer.Add(item);

		if (buffer.Count >= SaveBufferSize)
		{
			await HandleSaveBuffer(analyticSubJob, buffer, cancellationToken);
			buffer.Clear();
		}
	}

	if (buffer.Count > 0)
	{
		await HandleSaveBuffer(analyticSubJob, buffer, cancellationToken);
	}
}

Я выделил универсальную абстракцию, которая обрабатывает данные батчами:
After:

public sealed class BatchProcessor<T> : IAsyncEnumerable<IList<T>>,IAsyncDisposable
{
    private readonly IAsyncEnumerable<T> _source;
    private readonly int _batchSize;

    public BatchProcessor(IAsyncEnumerable<T> source, int batchSize)
    {
        _source = source;
        _batchSize = batchSize;
    }

    public async IAsyncEnumerator<IList<T>> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        var buffer = new List<T>(_batchSize);

        await foreach (T item in _source.WithCancellation(cancellationToken))
        {
            buffer.Add(item);
            if (buffer.Count < _batchSize)
            {
                continue;
            }

            yield return [..buffer];
            buffer.Clear();
        }

        if (buffer.Count > 0)
        {
            yield return buffer;
        }
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}

Использование:
private async Task HandleAnalyticItemsStream(
        AnalyticSubJob analyticSubJob,
        IAsyncEnumerable<TAnalyticItem> analyticItems,
        CancellationToken cancellationToken)
{
	await using var batchProcessor = new BatchProcessor<TAnalyticItem>(analyticItems, SaveBufferSize);
	await foreach (IList<TAnalyticItem> buffer in batchProcessor)
	{
		await HandleSaveBuffer(analyticSubJob, buffer, cancellationToken);
	}
}

При этом есть пакет System.Interactive.Async, который фактически позволяет сделать тоже самое :))
{
	await foreach (IList<TAnalyticItem> buffer in analyticItems.Buffer(SaveBufferSize))
	{
		await HandleSaveBuffer(analyticSubJob, buffer, cancellationToken);
	}
}



2) Второй паттерн, который также очень часто используется у нас - это обработка какой то задачи:
мы пытаемся получить задачу из БД, если она отсутствует - прерываем работу
если задача найдена, происходит её обработка и далее обновление статуса или обработка ошибки

пример:
before:	
internal sealed class ExecuteReconCalculationJobCommandHandler : IRequestHandler<ExecuteReconCalculationJobCommand>
{
    private readonly IReconCalculationJobRepository _reconCalculationJobRepository;
    private readonly Func<ReconCalculationType, IReconCalculationProcessor> _reconCalculationProcessorFactory;
    private readonly ILogger<ExecuteReconCalculationJobCommandHandler> _logger;
    private readonly IFailedCalculateDiscrepancyMeter _failedCalculateDiscrepancyMeter;

    private TimeSpan _allowedTimeError;

    public ExecuteReconCalculationJobCommandHandler(
        IReconCalculationJobRepository reconCalculationJobRepository,
        Func<ReconCalculationType, IReconCalculationProcessor> reconCalculationProcessorFactory,
        IOptionsSnapshot<CalculateReconDiscrepancyConfiguration> configurationAccessor,
        ILogger<ExecuteReconCalculationJobCommandHandler> logger,
        IFailedCalculateDiscrepancyMeter failedCalculateDiscrepancyMeter)
    {
        _reconCalculationJobRepository = reconCalculationJobRepository;
        _reconCalculationProcessorFactory = reconCalculationProcessorFactory;
        _logger = logger;
        _failedCalculateDiscrepancyMeter = failedCalculateDiscrepancyMeter;
        ApplyConfiguration(configurationAccessor.Value);
    }

    public async Task Handle(ExecuteReconCalculationJobCommand request, CancellationToken cancellationToken)
    {
        ReconCalculationJob? calculationJob = null;

        try
        {
            calculationJob = await _reconCalculationJobRepository.TakeToProcess(cancellationToken);

            if (calculationJob == null)
            {
                return;
            }

            IReconCalculationProcessor processor = _reconCalculationProcessorFactory(calculationJob.CalculationType);
            await processor.Process(calculationJob, cancellationToken);
            await _reconCalculationJobRepository.SetStatus(calculationJob, cancellationToken);
        }
        catch (Exception e)
        {
            if (calculationJob is not null)
            {
                _failedCalculateDiscrepancyMeter.IncrementFailedCalculation(calculationJob.ReconciliationCode);
                calculationJob.SetError(e.Message);
                CancellationToken ct = new CancellationTokenSource(_allowedTimeError).Token;
                await _reconCalculationJobRepository.SetStatus(calculationJob, ct);
            }

            throw;
        }
    }

    private void ApplyConfiguration(CalculateReconDiscrepancyConfiguration configuration)
    {
        _allowedTimeError = TimeSpan.FromMinutes(configuration.AllowedErrorTimeMinutes);
    }
} 

Выполнил рефакторинг - выделил статический класс - универсальный обработчик задачи
after:
public static class JobProcessor<TJob>
{
    public static async Task Process(
        Func<CancellationToken, Task<TJob?>> jobFetcher,
        Func<TJob, CancellationToken, Task> jobExecutor,
        Func<TJob, CancellationToken, Task> onSuccess,
        Func<TJob, Exception, Task> onError,
        CancellationToken cancellationToken)
    {
        var calculationJob = default(TJob);

        try
        {
            calculationJob = await jobFetcher(cancellationToken);

            if (calculationJob == null)
            {
                return;
            }

            await jobExecutor(calculationJob, cancellationToken);
            await onSuccess(calculationJob, cancellationToken);
        }
        catch (Exception e)
        {
            if (calculationJob is not null)
            {
                await onError(calculationJob, e);
            }
        }
    }
}

Использование:

public async Task Handle(ExecuteReconCalculationJobCommand request, CancellationToken cancellationToken)
{
    // единственный метод обработки - HOF, принимающая ф-ии обработчики
    await JobProcessor<ReconCalculationJob>.Process(
        jobFetcher: ct => _reconCalculationJobRepository.TakeToProcess(ct),
        jobExecutor: ProcessInternal,
        onSuccess: OnSuccess,
        onError: OnError,
        cancellationToken: cancellationToken
    );

    async Task ProcessInternal(ReconCalculationJob job, CancellationToken ct)
    {
        IReconCalculationProcessor processor = _reconCalculationProcessorFactory(job.CalculationType);
        await processor.Process(job, ct);
    }

    async Task OnSuccess(ReconCalculationJob job, CancellationToken ct)
    {
        await _reconCalculationJobRepository.SetStatus(job, ct);
    }

    async Task OnError(ReconCalculationJob job, Exception e)
    {
        _failedCalculateDiscrepancyMeter.IncrementFailedCalculation(job.ReconciliationCode);
        job.SetError(e.Message);
        CancellationToken ctInternal = new CancellationTokenSource(_allowedTimeError).Token;
        await _reconCalculationJobRepository.SetStatus(job, ctInternal);
    }
}

3) Третий паттерн - метод фоновой задачи, который обрабатывает какой то код, логгирует ошибки, выполняет delay

Пример:
Before:
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        try
        {
            await ProcessAutoReportTask(stoppingToken);
            await SendAutoReportTaskNotification(stoppingToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while processing AutoReportJobBackground: {@e}", e);
        }
        finally
        {
            await Task.Delay(TimeSpan.FromSeconds(_waitingTime), stoppingToken);
        }
    }
}

Выделил статический класс - раннер, который инкапсулирует в себе логику данного шаблона
After:
public static class JobRunner
{
    public static async Task RunAsync(
        Func<CancellationToken, Task> action,
        TimeSpan interval,
        ILogger? logger = null,
        CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await action(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Error in periodic job: {Message}", ex.Message);
            }
            finally
            {
                await Task.Delay(interval, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}

Применение:
await JobRunner.RunAsync(
	async ct =>
	{
		await ProcessAutoReportTask(ct);
		await SendAutoReportTaskNotification(ct);
	},
	TimeSpan.FromSeconds(_waitingTime),
	_logger,
	stoppingToken);