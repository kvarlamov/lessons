namespace HardWork._04_CodeFollowDesign._3;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Ozon.Inventory.Asgard.MovementEventsStorage.Domain.MovementBackupTask;
using Ozon.Inventory.Asgard.MovementEventsStorage.Domain.MovementOperationLogs;
using Ozon.Inventory.Asgard.MovementEventsStorage.Domain.ValueObjects;
using Ozon.Inventory.Asgard.MovementEventsStorage.DomainServices.MovementsBackupTasks.Configurations;
using Ozon.Inventory.Asgard.MovementEventsStorage.Repository.Metrics;
using Ozon.Inventory.Asgard.MovementEventsStorage.Repository.Postgres;

namespace Ozon.Inventory.Asgard.MovementEventsStorage.DomainServices.MovementsBackupTasks.Services;

internal sealed class MovementsBackupTaskProcessingService : IMovementsBackupTaskProcessingService
{
    private readonly IMovementsRepository _movementsRepository;
    private readonly IMovementsBackupTasksRepository _movementsBackupTasksRepository;
    private readonly IMovementsMigrationBatchProcessingService _migrationBatchProcessingService;
    private readonly ILogger<MovementsBackupTaskProcessingService> _logger;
    private readonly IFailedS3MigrationTasksCounterMeter _failedS3MigrationTasksCounterMeter;

    private int _movementsBatchSize;
    private int _s3FileSize;

    public MovementsBackupTaskProcessingService(
        IMovementsRepository movementsRepository,
        IOptionsSnapshot<MovementsS3MigrationConfig> movementsParallelProcessingConfig,
        IMovementsBackupTasksRepository movementsBackupTasksRepository,
        IMovementsMigrationBatchProcessingService migrationBatchProcessingService,
        ILogger<MovementsBackupTaskProcessingService> logger,
        IFailedS3MigrationTasksCounterMeter failedS3MigrationTasksCounterMeter)
    {
        _movementsRepository = movementsRepository;
        _movementsBackupTasksRepository = movementsBackupTasksRepository;
        _migrationBatchProcessingService = migrationBatchProcessingService;
        _logger = logger;
        _failedS3MigrationTasksCounterMeter = failedS3MigrationTasksCounterMeter;

        ApplyMovementsBatchSize(movementsParallelProcessingConfig.Value.MovementsBatchSizePerBucket);
        ApplyS3FileSize(movementsParallelProcessingConfig.Value.S3FileSize);
    }

    public async Task ProcessTask(
        MovementsBackupTask movementsBackupTask,
        CancellationToken cancellationToken)
    {
        try
        {
            var offset = 0;
            var movementsToS3 = new List<Movement>(_s3FileSize);

            IReadOnlyCollection<Movement> movements = await GetNotMigratedMovementsBatch(
                movementsBackupTask,
                offset,
                cancellationToken);

            while (movements.Count > 0)
            {
                var index = 0;

                while (index < movements.Count)
                {
                    IEnumerable<Movement> movementsToMigrate = movements
                        .Skip(index)
                        .Take(_s3FileSize)
                        .ToArray();

                    movementsToS3.AddRange(movementsToMigrate);

                    if (movementsToS3.Count >= _s3FileSize)
                    {
                        await _migrationBatchProcessingService.ProcessBatch(
                            movementsBackupTask,
                            movementsToS3,
                            cancellationToken);

                        movementsToS3.Clear();
                    }

                    index += movementsToMigrate.Count();
                }

                offset += _movementsBatchSize;
                movements = await GetNotMigratedMovementsBatch(
                    movementsBackupTask,
                    offset,
                    cancellationToken);
            }

            if (movementsToS3.Any())
            {
                await _migrationBatchProcessingService.ProcessBatch(
                    movementsBackupTask,
                    movementsToS3,
                    cancellationToken);
            }

            movementsBackupTask.SetStatus(MovementsBackupTaskStatus.Finish);
        }
        catch (Exception exception)
        {
            movementsBackupTask.SetStatus(MovementsBackupTaskStatus.Error);

            _failedS3MigrationTasksCounterMeter.Handle(movementsBackupTask.Type);
            _logger.LogError(
                exception,
                $"Task {movementsBackupTask.Type}/{movementsBackupTask.Date} failed. {exception.Message}");
        }
        finally
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1));
            await _movementsBackupTasksRepository.UpdateTask(
                movementsBackupTask,
                cts.Token);
        }
    }

    private Task<IReadOnlyCollection<Movement>> GetNotMigratedMovementsBatch(
        MovementsBackupTask movementsBackupTask,
        int offset,
        CancellationToken cancellationToken)
    {
        return _movementsRepository.GetBatchByDateRangeAndOperationTypeForMigration(
            movementsBackupTask.Date,
            movementsBackupTask.Date.AddDays(1),
            movementsBackupTask.Type,
            _movementsBatchSize,
            offset,
            cancellationToken);
    }

    private void ApplyMovementsBatchSize(int movementsBatchSize)
    {
        _movementsBatchSize = movementsBatchSize;
    }

    private void ApplyS3FileSize(int s3FileSize)
    {
        _s3FileSize = s3FileSize;
    }
}