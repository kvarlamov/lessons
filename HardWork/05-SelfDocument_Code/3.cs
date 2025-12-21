//ВО ВСЕХ ПРИМЕРАХ ЧАСТЬ КОДА ЗАМЕНЕНА НА ... для упрощения
/*
Представлен сервис, из названия которого следует, что он выполняет сохранение аналитических записей. 
На этом самодокументация кода закончена.
При этом изначально может показаться что он может подойти для сохранения любых аналитических данных, 
имплементирующих интерфейс, но фактически здесь есть ограничение -- данные могут быть получены только из определенного 
источника (сервиса с результатами сверок), а также могут быть сохранены только в репозиторий базы Clickhouse.
Такие ограничения следуют из того, что сервис используется в сервисе Аналитики, который и определяет процессы 
получения данных, их аналитики и сохранения.
И данный сервис как раз задает базовое поведение сохранения определенных аналитических данных в clickhouse.
При этом, если смотреть на полный флоу аналитики, то аналитика выполняется на основе базовой задачи аналитики,
которая в свою очередь состоит из подзадач аналитики, и данный сервис фактически сохраняет кусочек данных по 
аналитике, а не все необходимые данные целиком.
*/
/*
internal abstract class SaveAnalyticItemsServiceBase<TAnalyticItem> : ISaveAnalyticItemsService<TAnalyticItem>
    where TAnalyticItem : IAnalyticItem
{
    private readonly IAnalyticSubJobsRepository _analyticSubJobsRepository;
    private readonly ILogger _logger;

    protected int SaveMaxDegreeOfParallelism;
    protected int SaveBufferSize;

    private long _handledRows;

    protected SaveAnalyticItemsServiceBase(
        ILogger logger,
        IAnalyticSubJobsRepository analyticSubJobsRepository)
    {
        _logger = logger;
        _analyticSubJobsRepository = analyticSubJobsRepository;
    }

    public async Task SaveAnalytic(
        AnalyticSubJob analyticSubJob,
        IAsyncEnumerable<TAnalyticItem> analyticItems,
        CancellationToken cancellationToken)
    {
        _handledRows = 0;

        await Task.WhenAll(
            Enumerable.Range(start: 0, SaveMaxDegreeOfParallelism).Select(_ => HandleAnalyticItemsStream(analyticSubJob, analyticItems, cancellationToken)));
    }

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

    private async Task HandleSaveBuffer(AnalyticSubJob analyticSubJob, List<TAnalyticItem> buffer, CancellationToken cancellationToken)
    {
        ...
    }

    protected abstract Task SaveAnalyticItems(
        AnalyticSubJob analyticSubJob,
        IReadOnlyCollection<TAnalyticItem> analyticItems,
        CancellationToken cancellationToken);
}
*/