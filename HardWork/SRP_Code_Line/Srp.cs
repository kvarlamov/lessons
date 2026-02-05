namespace HardWork.SRP_Code_Line;

// 1)
// было: в методе происходило обновление структуры, и также изменение основного объекта, и все в одной строке
mutableCreatedAtMovement.ChangeCreatedAt(MovementOperationEnrichedData.UpdateProcessingDate(movementOperationTask.CreateAt));
// стало: вынес получение структуры для обновления в отдельную переменную
var updatedData = MovementOperationEnrichedData.UpdateProcessingDate(movementOperationTask.CreateAt);
utableCreatedAtMovement.ChangeCreatedAt(updatedData);


//2)
//было: в методе Linq Select происходила передача ф-ии маппинга коллекции в новую, а затем, ещё и фильтрация дублей 
MovementFlowEventInternal[] movementEvents = incomeMovements
    .Select(ToFlowEvent)
    .DistinctBy(x => new { x.Client, x.OperationId })
    .ToArray();

// стало: явно разделил получение маппинга движений и избавление от дублей
var flowEventMovements = incomeMovements.Select(ToFlowEvent)
var ToFlowEventsDistinct = flowEventMovements.DistinctBy(x => new { x.Client, x.OperationId }).ToArray
    // в данном случае выигрыша кажется нет, что в очередной раз показывает, что каждый случай может быть исключением
  
//3)
// было: Получение стрима из БД, преобразование в доменные модели + в коллекцию из стрима в одной строке
await _shardedClickHouseQueryExecutor
    .QueryIteratorAsync(query, cancellationToken)
    .Select(db => db.ToDomain())
    .ToListAsync(cancellationToken);

// стало: разделил код по ответственностям - получение стрима, маппинг в доменнуб модель
IAsyncEnumerable<MovementDb> iterator= _shardedClickHouseQueryExecutor
    .QueryIteratorAsync(query, cancellationToken);
await foreach (var item in iterator)
{
    yield return item.ToDomain();
}

      
//4) 
// было: в итерировании используется метод, получающий асинхронный стрим
await foreach (InvC15d5ShipExemplarInfo c15d5ShipExemplarInfo in ProcessExemplarsBuffer(exemplarDbs, cancellationToken))
{
    ...
}

// стало: вынес в переменную получение асинхронного стрима
IAsyncEnumerable<InvC15d5ShipExemplarInfo> iterator = ProcessExemplarsBuffer(exemplarDbs, cancellationToken);
await foreach (InvC15d5ShipExemplarInfo c15d5ShipExemplarInfo in iterator)
{
    ...
}


//5)
// было: аналог прошлого примера (4), но в ещё более "страшном" виде =):
// получение стрима, с передачей в метод ф-ии, которая отвечает за преобразование объекта
await foreach (InvC15d5ShipExemplarInfo c15d5ShipExemplarInfo in HandleBuffer<InvC15d5ShipExemplarDb, InvC15d5ShipExemplarInfo>(
                   buffer,
                   (db, postingInfo) => postingInfo != null ? db.ToDomain(postingInfo) : db.ToDomain(),
                   ct))
// стало: также явно выделил переменную-итератор, а также убрал логику, определяющую как маппить в доменную модель, в метод Handle
IAsyncEnumerable<InvC15d5ShipExemplarInfo> iterator = HandleBuffer<InvC15d5ShipExemplarDb, InvC15d5ShipExemplarInfo>(buffer, ct)
await foreach (InvC15d5ShipExemplarInfo c15d5ShipExemplarInfo in iterator)
{
    ...
}


//6)
// было: отправка команды медиатора и создание токена
await _mediator.Send(new ExecuteReconCalculationJobCommand(), CancellationTokenSource.CreateLinkedTokenSource(stoppingToken, cts.Token).Token);

//стало: вынес создание токена и команды медиатора в отдельные переменные, медиатор только отправляет Request
var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken, cts.Token);
var command = new ExecuteReconCalculationJobCommand();
await _mediator.Send(command, tokenSource.Token);

//7)
// было: Обогащение параметров прямо в параметрах метода -- слишком длинная строка
result.EnrichWithTicketsAndDate(calculationJob.ReconDate, previousLaunchInfo.JiraTicket.WithUrl(JiraUrl), lastLaunchInfo.JiraTicket.WithUrl(JiraUrl));

// стало: вынес обогащзение джира тикетов в отдельные переменные
var previosTicketEnriched = previousLaunchInfo.JiraTicket.WithUrl(JiraUrl);
var currentTicketEnriched = lastLaunchInfo.JiraTicket.WithUrl(JiraUrl);
result.EnrichWithTicketsAndDate(calculationJob.ReconDate, previosTicketEnriched, currentTicketEnriched);
