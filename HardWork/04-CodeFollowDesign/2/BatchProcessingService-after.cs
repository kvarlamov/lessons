using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HardWork._04_CodeFollowDesign._2;

// Спецификация
public record ProcessingContext(
    MovementsRetryTask RetryTask,
    MovementsRetrySubTask SubTask,
    IReadOnlyCollection<MovementEvent> MovementEvents,
    CancellationToken CancellationToken);

public record ValidationResult(
    bool ShouldStopProcessing,
    IReadOnlyCollection<MovementEvent> ValidMovementEvents,
    string? ErrorMessage = null);

// Правила вынесены в чистые функции
public static class BatchProcessingRules
{
    // Precondition check
    public static ProcessingContext ValidateContext(ProcessingContext context)
    {
        if (context.MovementEvents.Count == 0)
        {
            throw new ArgumentException("No movement events found");
        }

        if (context.RetryTask is null || context.SubTask is null)
        {
            throw new  ArgumentException("No subtask or task provided");
        }
        
        return context;
    }
    
    public static bool ShouldSkipValidation(ProcessingContext context, bool skipMovementsValidation)
        => skipMovementsValidation;

    public static bool RequiresPreValidation(ProcessingContext context)
        => context.RetryTask.Settings.TaskType != MovementSource.NewPublisher;

    public static IReadOnlyCollection<MovementEvent> GetValidEvents(
        IReadOnlyCollection<MovementEvent> events)
        => events.Where(mov => mov.Status != MovementEventStatus.Error).ToArray();

    // Проверка последнего события
    public static bool ContainsLastEvent(IReadOnlyCollection<MovementEvent> events)
        => events.Any(movementEvent => movementEvent.IsLastMovement);
}

internal sealed class MovementEventsBatchProcessingService2 : IMovementEventsBatchProcessingService
{
    private readonly IProcessorFactory _processorFactory;
    private readonly ILogger<MovementEventsBatchProcessingService2> _logger;
    private readonly IMovementEventsValidationService _movementEventsValidationService;
    private readonly IFailedBatchValidationHandlingService _failedBatchValidationHandlingService;
    private readonly IMovementEventsSendingService _movementEventsSendingService;
    private readonly IFailedMovementEventRepository _failedMovementEventRepository;

    private bool _skipMovementsValidation;

    public MovementEventsBatchProcessingService2(
        IProcessorFactory processorFactory,
        ILogger<MovementEventsBatchProcessingService2> logger,
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
        // проверяем предусловия
        var context = BatchProcessingRules.ValidateContext(new ProcessingContext(retryTask, subTask, movementEvents, cancellationToken));
        
        // Валидация
        var validationResult = await Validate(context);
        
        if (!validationResult.ShouldProcess)
            return;

        // Отправка
        await Sending(context with { MovementEvents = validationResult.ValidMovementEvents });
    }
    
    private async Task<ValidationResult> Validate(ProcessingContext context)
    {
        // Если валидация пропущена - обрабатываем все события
        if (BatchProcessingRules.ShouldSkipValidation(context, _skipMovementsValidation))
            return new ValidationResult(true, context.MovementEvents);

        // Получаем результат валидации
        var validationResult = await ValidateBatch(context);
        
        return validationResult;
    }

    private async Task<ValidationResult> ValidateBatch(ProcessingContext context)
    {
        // Предварительная обработка (если требуется)
        var eventsToValidate = await ApplyPreValidation(context);
        
        _logger.LogInformation(
            "TaskId: {Task}. SubTask Validating movements: {subTask} - {counter}",
            context.RetryTask.TaskId.Value,
            context.SubTask.Id.Value,
            eventsToValidate.Count);

        // Валидация
        bool isBatchValid = await _movementEventsValidationService.IsBatchValid(
            context.RetryTask,
            context.SubTask.Id.Value,
            eventsToValidate,
            context.CancellationToken);

        if (isBatchValid)
            return new ValidationResult(false, eventsToValidate);

        return await HandleInvalidBatch(context with { MovementEvents = eventsToValidate });
    }

    private async Task<IReadOnlyCollection<MovementEvent>> ApplyPreValidation(ProcessingContext context)
    {
        if (!BatchProcessingRules.RequiresPreValidation(context))
            return context.MovementEvents;

        IMovementProcessor processor = _processorFactory.Get(context.RetryTask);
        return await processor.PreValidationStep(
            context.RetryTask,
            context.MovementEvents,
            context.CancellationToken);
    }

    private async Task<ValidationResult> HandleInvalidBatch(ProcessingContext context)
    {
        var failedEvents = context.MovementEvents
            .Where(movementEvent => movementEvent.Status == MovementEventStatus.Error)
            .ToArray();

        await _failedMovementEventRepository.AddMovements(
            context.SubTask.Id,
            failedEvents,
            context.CancellationToken);

        bool shouldStopProcessing = await _failedBatchValidationHandlingService.IsShouldStopProcessing(
            context.RetryTask,
            context.SubTask,
            context.MovementEvents,
            !BatchProcessingRules.RequiresPreValidation(context), // isNewPublisherProcessing
            context.CancellationToken);

        if (shouldStopProcessing)
        {
            context.SubTask.SetError(ErrorMessages.TaskValidationError);
            context.RetryTask.SetError(ErrorMessages.TaskValidationError);
            return new ValidationResult(true, context.MovementEvents);
        }

        var validEvents = BatchProcessingRules.GetValidEvents(context.MovementEvents);
        return new ValidationResult(false, validEvents);
    }

    private async Task Sending(ProcessingContext context)
    {
        // Отправка событий
        await _movementEventsSendingService.SendMovementEvents(
            context.RetryTask,
            context.SubTask,
            context.MovementEvents,
            context.CancellationToken);

        // Завершение подзадачи при последнем событии
        if (BatchProcessingRules.ContainsLastEvent(context.MovementEvents))
        {
            context.SubTask.SetSuccess();
        }
    }
}