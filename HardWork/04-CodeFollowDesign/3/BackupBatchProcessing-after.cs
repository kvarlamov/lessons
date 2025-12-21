/*
namespace HardWork._04_CodeFollowDesign._3;

// Спецификации
public record ProcessingConfiguration(int MovementsBatchSize, int S3FileSize)
{
    public static ProcessingConfiguration Create(MovementsS3MigrationConfig config)
        => new(config.MovementsBatchSizePerBucket, config.S3FileSize);
}

public record ProcessingContext(
    MovementsBackupTask Task,
    ProcessingConfiguration Config,
    CancellationToken CancellationToken);

public record BatchProcessingResult(
    bool IsCompleted,
    IReadOnlyCollection<Movement> RemainingMovements);

public static class BackupProcessingRules
{
    // Предусловия
    public static ProcessingContext ValidateContext(ProcessingContext context)
    {
        if (context.Config.MovementsBatchSize <= 0)
            throw new ArgumentException("Movements batch size must be positive");
        if (context.Config.S3FileSize <= 0)
            throw new ArgumentException("S3 file size must be positive");
        return context;
    }

    // Правила батчинга
    public static IEnumerable<IReadOnlyCollection<Movement>> CreateS3Batches(
        IReadOnlyCollection<Movement> movements,
        int s3FileSize)
    {
        return movements
            .Select((movement, index) => new { movement, index })
            .GroupBy(x => x.index / s3FileSize)
            .Select(g => g.Select(x => x.movement).ToArray());
    }

    // Проверка завершения
    public static bool IsProcessingComplete(IReadOnlyCollection<Movement> movements)
        => movements.Count == 0;
}

public sealed class MovementsBackupTaskProcessingService
{
    private readonly IMovementsRepository _movementsRepository;
    private readonly IMovementsBackupTasksRepository _movementsBackupTasksRepository;
    private readonly IMovementsMigrationBatchProcessingService _migrationBatchProcessingService;
    private readonly ILogger<MovementsBackupTaskProcessingService> _logger;
    private readonly IFailedS3MigrationTasksCounterMeter _failedS3MigrationTasksCounterMeter;
    private readonly ProcessingConfiguration _config;

    public MovementsBackupTaskProcessingService(...)
    {
        // ... инициализация зависимостей
        _config = ProcessingConfiguration.Create(movementsParallelProcessingConfig.Value);
    }

    public async Task ProcessTask(
        MovementsBackupTask movementsBackupTask,
        CancellationToken cancellationToken)
    {
        var context = new ProcessingContext(movementsBackupTask, _config, cancellationToken);
        
        try
        {
            await ExecuteBackupProcessing(context);
            context.Task.SetStatus(MovementsBackupTaskStatus.Finish);
        }
        catch (Exception exception)
        {
            await HandleProcessingError(context, exception);
        }
        finally
        {
            await FinalizeTask(context);
        }
    }

    private async Task ExecuteBackupProcessing(ProcessingContext context)
    {
        BackupProcessingRules.ValidateContext(context);
        
        await foreach (var s3Batch in CreateS3Batches(context))
        {
            await _migrationBatchProcessingService.ProcessBatch(
                context.Task,
                s3Batch,
                context.CancellationToken);
        }
    }

    private async IAsyncEnumerable<IReadOnlyCollection<Movement>> CreateS3Batches(ProcessingContext context)
    {
        var accumulatedBatch = new List<Movement>(_config.S3FileSize);
        
        await foreach (var dbBatch in FetchMovementBatches(context))
        {
            var s3Batches = BackupProcessingRules.CreateS3Batches(dbBatch, _config.S3FileSize);
            
            foreach (var s3Batch in s3Batches)
            {
                if (accumulatedBatch.Count + s3Batch.Count > _config.S3FileSize && accumulatedBatch.Any())
                {
                    yield return accumulatedBatch.ToArray();
                    accumulatedBatch.Clear();
                }
                
                accumulatedBatch.AddRange(s3Batch);
            }
        }

        if (accumulatedBatch.Any())
        {
            yield return accumulatedBatch;
        }
    }

    private async IAsyncEnumerable<IReadOnlyCollection<Movement>> FetchMovementBatches(ProcessingContext context)
    {
        var offset = 0;
        
        while (true)
        {
            var movements = await _movementsRepository.GetBatchByDateRangeAndOperationTypeForMigration(
                context.Task.Date,
                context.Task.Date.AddDays(1),
                context.Task.Type,
                _config.MovementsBatchSize,
                offset,
                context.CancellationToken);

            if (BackupProcessingRules.IsProcessingComplete(movements))
                yield break;

            yield return movements;
            offset += _config.MovementsBatchSize;
        }
    }

    private async Task HandleProcessingError(ProcessingContext context, Exception exception)
    {
        context.Task.SetStatus(MovementsBackupTaskStatus.Error);
        _failedS3MigrationTasksCounterMeter.Handle(context.Task.Type);
        
        _logger.LogError(
            exception,
            "Task {TaskType}/{TaskDate} failed. {ErrorMessage}",
            context.Task.Type,
            context.Task.Date,
            exception.Message);
    }

    private async Task FinalizeTask(ProcessingContext context)
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1));
        await _movementsBackupTasksRepository.UpdateTask(context.Task, cts.Token);
    }
}
*/