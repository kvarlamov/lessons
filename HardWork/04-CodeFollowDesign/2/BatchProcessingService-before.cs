using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HardWork._04_CodeFollowDesign._2;

internal sealed class MovementEventsBatchProcessingService : IMovementEventsBatchProcessingService
{
    private readonly IProcessorFactory _processorFactory;
    private readonly ILogger<MovementEventsBatchProcessingService> _logger;
    private readonly IMovementEventsValidationService _movementEventsValidationService;
    private readonly IFailedBatchValidationHandlingService _failedBatchValidationHandlingService;
    private readonly IMovementEventsSendingService _movementEventsSendingService;
    private readonly IFailedMovementEventRepository _failedMovementEventRepository;

    private bool _skipMovementsValidation;

    public MovementEventsBatchProcessingService(
        IProcessorFactory processorFactory,
        ILogger<MovementEventsBatchProcessingService> logger,
        IMovementEventsValidationService movementEventsValidationService,
        IFailedBatchValidationHandlingService failedBatchValidationHandlingService,
        IMovementEventsSendingService movementEventsSendingService,
        IOptionsSnapshot<PublisherRetryConfiguration> configurationAccessor,
        IFailedMovementEventRepository failedMovementEventRepository)
    {
        _processorFactory = processorFactory;
        _logger = logger;
        _movementEventsValidationService = movementEventsValidationService;
        _failedBatchValidationHandlingService = failedBatchValidationHandlingService;
        _movementEventsSendingService = movementEventsSendingService;
        _failedMovementEventRepository = failedMovementEventRepository;

        _skipMovementsValidation = configurationAccessor.Value.SkipMovementsValidation;
    }

    public async Task ProcessBatch(
        MovementsRetryTask retryTask,
        MovementsRetrySubTask subTask,
        IReadOnlyCollection<MovementEvent> movementEvents,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<MovementEvent> batchToProcess = movementEvents;

        if (!_skipMovementsValidation)
        {
            ValidationHandlingResult result = await GetBatchValidationResult(
                retryTask,
                subTask,
                batchToProcess,
                cancellationToken);

            if (result.ShouldStopProcessing)
            {
                return;
            }

            batchToProcess = result.ValidMovementEvents;
        }

        await _movementEventsSendingService.SendMovementEvents(
            retryTask,
            subTask,
            batchToProcess,
            cancellationToken);

        bool isLastBatch = movementEvents.Any(movementEvent => movementEvent.IsLastMovement);

        if (isLastBatch)
        {
            subTask.SetSuccess();
        }
    }

    private async Task<ValidationHandlingResult> GetBatchValidationResult(
        MovementsRetryTask retryTask,
        MovementsRetrySubTask subTask,
        IReadOnlyCollection<MovementEvent> batchToProcess,
        CancellationToken cancellationToken)
    {
        bool isNewPublisherProcessing = retryTask.Settings.TaskType == MovementSource.NewPublisher;

        if (!isNewPublisherProcessing)
        {
            IMovementProcessor processor = _processorFactory.Get(retryTask);
            batchToProcess = await processor.PreValidationStep(
                retryTask,
                batchToProcess,
                cancellationToken);
        }

        _logger.LogInformation(
            "TaskId: {Task}. SubTask Validating movements: {subTask} - {counter}",
            retryTask.TaskId.Value,
            subTask.Id.Value,
            batchToProcess.Count);

        bool isBatchValid = await _movementEventsValidationService.IsBatchValid(
            retryTask,
            subTask.Id.Value,
            batchToProcess,
            cancellationToken);

        if (isBatchValid)
        {
            return new ValidationHandlingResult(ShouldStopProcessing: false, batchToProcess);
        }

        MovementEvent[] failedMovementEvents = batchToProcess
            .Where(movementEvent => movementEvent.Status == MovementEventStatus.Error)
            .ToArray();

        await _failedMovementEventRepository.AddMovements(
            subTask.Id,
            failedMovementEvents,
            cancellationToken);

        bool shouldStopProcessing = await _failedBatchValidationHandlingService.IsShouldStopProcessing(
            retryTask,
            subTask,
            batchToProcess,
            isNewPublisherProcessing,
            cancellationToken);

        if (shouldStopProcessing)
        {
            subTask.SetError(ErrorMessages.TaskValidationError);
            retryTask.SetError(ErrorMessages.TaskValidationError);

            return new ValidationHandlingResult(shouldStopProcessing, batchToProcess);
        }

        batchToProcess = batchToProcess
            .Where(mov =>
                mov.Status != MovementEventStatus.Error)
            .ToArray();

        return new ValidationHandlingResult(shouldStopProcessing, batchToProcess);
    }
}