Пример 1.
Для упрощения скрыл лишние поля

Есть класс 
public sealed record ExemplarAttributes(
...
DateTimeOffset? FactIncomeMoment,
...);

И в коде использовалось создание экземпляра класса:
return new ExemplarAttributes(
	...
	Fixer(exemplarAttributesInternal.FactIncomeMoment),
	...);
	
	
Для свойства FactIncomeMoment использовался следующий метод:
private static DateTimeOffset? Fixer(DateTimeOffset? factIncomeMoment)
{
	if (!factIncomeMoment.HasValue)
	{
		return null;
	}

	var customDate = new DateTimeOffset(year: 1901, month: 1, day: 1, hour: 0, minute: 0, second: 0, TimeSpan.Zero);
	if (factIncomeMoment < customDate)
	{
		return customDate;
	}

	return factIncomeMoment;
}

При этом понимания, почему так сделано нет - коммента никакого не оставлено, и гарантий, что данный фиксер останется при следующем рефакторинге
тоже нет.

Плохой рефакторинг (уборка в методе):
- написать комментарий, возможно даже указать проблему, которую это решило
- сделать более говорящее название метода и переменных: IncorretTimeFixer, minAllowedDate...

Возможный правильный рефакторинг:
Меняем свойство класса ExemplarAttributes с 
DateTimeOffset? FactIncomeMoment на
конкретный тип: 
public record struct FactIncomeMomentTime
{
    private enum FactIncomeState
    {
        Nullable,
        ModifiedToMinValue,
        Valid
    }
    private DateTimeOffset? Value { get; init; }
    private FactIncomeState State { get; init; }

    private FactIncomeMomentTime(DateTimeOffset? factIncomeMoment, FactIncomeState momentState)
    {
        Value = factIncomeMoment;
        State = momentState;
    }

    public static FactIncomeMomentTime Create(DateTimeOffset? factIncomeMoment)
    {
        if (!factIncomeMoment.HasValue)
        {
            return new FactIncomeMomentTime(factIncomeMoment: null, FactIncomeState.Nullable);
        }

        var customDate = new DateTimeOffset(year: 1901, month: 1, day: 1, hour: 0, minute: 0, second: 0, TimeSpan.Zero);
        if (factIncomeMoment < customDate)
        {
            return new FactIncomeMomentTime(customDate, FactIncomeState.ModifiedToMinValue);
        }

        return new FactIncomeMomentTime(factIncomeMoment, FactIncomeState.Valid);
    }

    public DateTimeOffset GetTimeNotNull()
    {
        if (State == FactIncomeState.Nullable)
		{
			throw new InvalidOperationException(...);
		}
		
		return Value;
    }
}

Как это влияет:
- мы можем создать income время только через специальный фабричный метод.
На первый взгляд кажется что мы просто перенесли логику фиксера в другое место.
Но изменения здесь глубже:
изменив свойство объекта с обычного типа DateTimeOffset мы ввели тип, определяющий бизнес правила - каким может быть время incomeTime.
В этом типе (и только в нём) мы задаём допустимые границы времени и как они должны быть представлены;
Если завтра нам скажут, что невалидное время нужно не преобразовывать, а выдавать это с ошибкой,
изменив логику в типе мы явно измененим поведение во всей системе, гарантируя правильную обработку.
Введение внутреннего состояния FactIncomeState может помочь по разному обрабатывать возможные состояния объекта.
Они(состояния) явно определены в самом типе, и мы можем исключить всю логику проверки FactIncomeMoment.HasValue?, которая
могла присутствовать в коде со старым Nullable типом DateTimeOffset?, а явно обрабатывать (если это необходимо) разные состояния.

Также закрыв в типе значение времени (private Value) в местах, где по логике мы не должны иметь значение null мы можем пользоваться методом,
которые не отдаёт пустого значения GetTimeNotNull

использование в коде:
return new ExemplarAttributes(
	...
	FactIncomeMomentTime.Create(exemplarAttributesInternal.FactIncomeMoment),
	...);


2 пример

public async Task ProcessBatch(
        MovementsRetryTask retryTask,
        MovementsRetrySubTask subTask,
        IReadOnlyCollection<MovementEvent> movementEvents,
        CancellationToken cancellationToken)
{
	bool isLastBatch = movementEvents.Any(movementEvent => movementEvent.IsLastMovement);

	if (retryTask.TaskType == RetryTaskType.DocumentsByClientOperationId)
	{
		await ProcessDocuments(retryTask, subTask, movementEvents, isLastBatch, cancellationToken);
	}

	if (retryTask.TaskType == RetryTaskType.PostingsByClientOperationId)
	{
		await ProcessPostings(retryTask, subTask, movementEvents, isLastBatch, cancellationToken);
	}
	else
	{
		await ProcessMovementBatchBatch(retryTask, subTask, movementEvents, isLastBatch, cancellationToken);
	}
}

Изначально в методе был только код
await ProcessMovementBatchBatch(retryTask, subTask, movementEvents, isLastBatch, cancellationToken);
который обрабатывал некоторый набор записей.

Далее я, при выполнении задачи, с учётом сжатых сроков добавил в существующий код 1-н if 
if (retryTask.TaskType == RetryTaskType.DocumentsByClientOperationId)
который обрабатывал документы из набора записей.

И недавно разработчик из нашей команды, на основе моих "поделок" усугубил ситуацию и добавил:
if (retryTask.TaskType == RetryTaskType.PostingsByClientOperationId)
{
	await ProcessPostings(retryTask, subTask, movementEvents, isLastBatch, cancellationToken);
}

В итоге метод стал выглядеть неуклюже, а в перспективе может стать гораздо хуже.
Вызывающий данный метод код:
await _movementEventsBatchProcessingService.ProcessBatch(
retryTask,
subTask,
movements,
cancellationToken);

Возможный рефакторинг:

// данный интерфейс не меняем - он как и ранее обрабатывает набор данных типа MovementEvent
public interface IMovementEventsBatchProcessingService
{
    Task ProcessBatch(
        MovementsRetryTask retryTask,
        MovementsRetrySubTask subTask,
        IReadOnlyCollection<MovementEvent> batch,
        CancellationToken cancellationToken);
}

выделяем процессоры для обработки каждого типа (паттерн стратегия)

_processors = new Dictionary<RetryTaskType, IRetryTaskBatchProcessor>
{
	[RetryTaskType.DocumentsByClientOperationId] = _documentsProcessor,
	[RetryTaskType.PostingsByClientOperationId] = _postingsProcessor,
	[RetryTaskType.DateRange] = _defaultProcessor,
	[RetryTaskType.ClientOperationId] = _defaultProcessor
};

и тогда метод обработки будет таким:
public async Task ProcessBatch(
        MovementsRetryTask retryTask,
        MovementsRetrySubTask subTask,
        IReadOnlyCollection<MovementEvent> movementEvents,
        CancellationToken cancellationToken)
{
	bool isLastBatch = movementEvents.Any(movementEvent => movementEvent.IsLastMovement);

	var processor = _processors[retryTask.TaskType];
	await processor.ProcessAsync(retryTask, subTask, movementEvents, isLastBatch, cancellationToken);
}

при этом при добавлении новых типов - нам всего лишь достаточно будет добавить новую стратегию обработки.

Теперь код метода "процессит" входящий набор данных, А как это делается скрыто в деталях каждого процессора.
Задали поведение явно через интерфейс процессора.
При этом он сохранил поведение в системе - фактически делать будет тоже самое, но код стал расширяемым.
И тестировать отдельные процессоры (по use cases) гораздо проще.

Reflection:
При реализации задания понял, что выполнять полноценный рефакторинг достаточно сложно, он не рождается на лету, как рефакторинг уборка.
Нужно хорошо осозновать, что делает система. И постоянно думать на медленном мышлении о том, что мы делаем, а не как.
Поэтому не смог найти 3-й пример в своем коде. Но при этом сегодня удалось применить часть подходов из задания в code review
и "уберечь" код от ненужной проверки (правда это из 1-й ошибки задания). Но в любом случае рассуждения о том, как взаимодействуют
части системы, а не детали реализации методов стараюсь теперь применять в каждом удобном случае и более вдумчиво думать о системе в целом.

Что касается тестирования - лучший сценарий тестирования use cases на мой взгляд - это интеграционные тесты, которые применяю повсеместно.




