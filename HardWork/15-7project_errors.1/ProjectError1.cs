Примеры упростил, оставив главное:

1. Команда удаления задачи
Было:

var task = await _taskRepo.GetById(command.TaskId);
if (task == null)
    throw new Exception("Task not found");
await _taskRepo.Delete(command.TaskId);

Что не так:
пользователь (человек вызывающий через ui/swagger) мог неверно ввести guid задачи, 
её не находили и кидали эксепшн.

Стало:
Хотя здесь проверка кажется работала корректно, можно было забыть её сделать. 
Поэтому переделал контракт репозитория на новый функциона Option<T> (фишка 10 дотнета, пока в проде не применял):
public async Task<Option<Job>> GetByIdAsync(Guid id);
Обработчик:
public async Task<Result> Handle(DeleteTaskCommand command, CancellationToken ct)
{
    var taskOption = await _taskRepo.GetByIdAsync(command.TaskId);
    
    return taskOption.Match(
        some: async task => 
        {
            await _taskRepo.DeleteAsync(task);
            return Result.Success();
        },
        none: () => Result.Failure("Task with id {command.TaskId} not found")
    );
}

2. Хэндлер создания задачи с catch всех подряд Exception
Было:

try 
{
	...
    await _recopistory.Create(job, token);
	...
} catch(Exception ex) 
{
	...
    _logger.LogError(e, "ошибка при добавлении задачи");
	...
}

Что здесь не так:
вызывается команда по создание некоторой задачи. При это ловятся все исключения подряд, и фактически мы не знаем:
- задача не создана по инфраструктурной причине (проблемы с коннектом к БД, закончилась память и т.д.)
- задача не создана из-за дубликата ключа

Стало:
try
{
	bool isExist = await _repository.CheckNotExist(job.Id, token);
	if (isExist)
	{
		throw new DomainException(ErrorCode.JobDublicate, "задача с {job.Id} уже существует");
	}
}
catch(DatabaseException ex)
{
	// обрабатываем ошибки базы и только
	_logger.LogError(...);
	throw; // пробрасываем выше
}

3. JiraCommentBuilder: проверка пустой коллекции в билдере джира комментария
Было:

private readonly AddTableComment<T>(IReadonlyCollection<T> data)
{
    if (data.Count == 0) 
		return this;
    // построение комментария
}

Что здесь не так:
Перестраховка - просто по интуиции добавил валидацию входных параметров.
Однако при работе над заданием нашёл, что код, 
который как раз вызывает JiraCommentBuilder уже проверяет коллекцию на пустоту перед вызовом.

Стало:

// Убрал проверку как избыточную - на этом уровне сюда никогда не придёт пустая коллекция

4. DateRange: проверка даты в конструкторе
Было:

...
public DateRange(DateTime fromDate, DateTime toDate)
{
    if (fromDate > toDate) throw ...;
    if (toDate > DateTime.UtcNow) toDate = DateTime.UtcNow;
    ...
}

при этом:
public sealed class TaskSettings
{
	...
	public DateRange DateRange {get;}
	
	public TaskSettings(DateRange dateRange)
	{
		if (dateRange.FromDate > dateRange.ToDate)
		{
			throw new DomainException(ErrorCodes.InvalidDateRange,
                $"FromDate {dateRange.FromDate} greater than toDate: {dateRange.ToDate}");
		}
	}
}

Что не так:
- Конструктор DateRange проверяет fromDate > toDate. При этом в классе где используется DateRange уже есть эта проверка.
- if (toDate > DateTime.UtcNow) toDate = DateTime.UtcNow; - это изменение входных данных может быть совсем неочевидно
для пользователя

Что сделано:
- в конструкторе убрал избыточную проверку fromDate > toDate
- убрал также изменение невалидных данных if (toDate > DateTime.UtcNow) toDate = DateTime.UtcNow;
, явно заменив ошибкой

public sealed class TaskSettings
{
	...
	public DateRange DateRange {get;}
	
	public TaskSettings(DateRange dateRange)
	{
		if (dateRange.FromDate > dateRange.ToDate)
		{
			throw new DomainException(ErrorCodes.InvalidDateRange,
                $"FromDate {dateRange.FromDate} greater than toDate: {dateRange.ToDate}");
		}
		
		if (dateRange.ToDate > DateTime.UtcNow)
		{
			throw new DomainException(ErrorCodes.InvalidToDate,
                $"toDate: {dateRange.ToDate} > than today. Fix");
		}
		...
	}
}


5. GetRequestCommand: цепочка проверок null и статуса
Было:

public async Task Handle(GetRequestCommand request, CancellationToken ct)
{
	var retrySubTask = await _subTaskRepo.GetSubTaskToProcess(...);
	if (retrySubTask is null)
		return;

	var retryTask = await _taskRepo.GetById(retrySubTask.TaskId);
	if (retryTask is null) 
		throw new Exception("no task for subtask");

	if (retryTask.Status == RetryTaskStatus.New)
		await _taskRepo.Update(...);
}

Что нарушено:

Подзадача создаётся отдельно от задачи, хотя они должны быть связаны транзакционно.
БД допускает состояние, когда есть подзадача без родительской задачи.

Стало:

class RetryContext
{
	public RetryTask RetryTask {get;}
	public RetrySubTask SubTask {get;}
}

public async Task Handle(GetRequestCommand request, CancellationToken ct)
{
    var context = await _repo.GetNextRetryContextInProgress(ct);
    if (context == null) 
		return;
    
    
    await HandleRetry(context);
}

Сразу получаю задачу и подзадачу в правильном статусе транзакционно в новом классе обёртке.