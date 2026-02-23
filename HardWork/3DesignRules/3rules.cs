1. Запрет ошибочного комбинирования на уровне классов
1.1. Немного упростил пример, убрав всю ненужную информацию:

public abstract class StockTypeDiscrepancyBase : ReconDataBase
{
    public DateTime Date { get; protected set; }
    public long Count { get; }
    public long Diff { get; protected set; }

    public StockTypeDiscrepancyBase(long count)
    {
        Count = count;
    }

    public void SetDiff(long diff)
    {
        Diff = diff;
    }

    public void EnrichWithDate(DateTime date)
    {
        Date = date;
    }
	
	...
}

public sealed class StockTypeDiscrepancy : StockTypeDiscrepancyBase
{
    public ReconStockType StockType { get; }

    public StockTypeDiscrepancy(ReconStockType stockType, long count) : base(count)
    {
        AsgardStockType = stockType;
    }

    public static StockTypeDiscrepancy CreateFromDb(
        DateTime date,
        ReconStockType stockType,
        long count,
        long diff)
    {
        return new StockTypeDiscrepancy(stockType, count)
        {
            Date = date,
            Diff = diff
        };
    }
}

в иерархии классов имеется базовый тип StockTypeDiscrepancyBase, который устанавливает Diff - разницу между прошлым вычисление и текущим
При этом в его наследнике StockTypeDiscrepancy имеется статический метод, который создаёт тип из базы и именно на основе его происходит вычисление
Потенциально здесь несколько ошибок:
- если в какой то момент вызывающий код не вызовет получение типа из БД, не обработает корректным образом и не отправит нужный параметр, то Diff не будет вычислен или вычислится неправильно
- публичный конструктор при наличии публичного фабричного метода CreateFromDb -- если появится новый статический метод например CreateNew (разработчик увидит что уже есть другой статичный метод) то он по сути вступает в конфликт
с конструктором -- и может быть вызван не тот "создатель" класса, который ожидается, или что ещё хуже, в разных
местах будут вызываться разные, вызывая путаницу при отладке

Как можно изменить:
- в метод Count как минимум можно явно передавать тип (ValueType или целиком объект, а не примитив), чтобы
в методе явно проверить нужное поле и корректно покрыть это тестами (а не в разных местах вызывающего кода)

public void SetDiff(StockTypeDiscrepancy previous)
{
	...
	
	Diff = Count - previous.Count;
}
// или вообще рассчитывать Diff на уровне БД

- изменить публичный конструктор на protected, избавившись от двойственности при создании объекта
public abstract class StockTypeDiscrepancyBase : ReconDataBase
{
    public DateTime Date { get; protected set; }
    public long Count { get; }
    public long Diff { get; protected set; }

    protected StockTypeDiscrepancyBase(long count)
    {
        Count = count;
    }
	
	...
}

public sealed class StockTypeDiscrepancy : StockTypeDiscrepancyBase
{
    public ReconStockType StockType { get; }

    protected StockTypeDiscrepancy(ReconStockType stockType, long count) : base(count)
    {
        AsgardStockType = stockType;
    }

    public static StockTypeDiscrepancy Create(
        DateTime date,
        ReconStockType stockType,
        long count,
        long diff)
    {
        return new StockTypeDiscrepancy(stockType, count)
        {
            Date = date,
            Diff = diff
        };
    }
	...
}

1.2 Потенциально неправильная установка статуса:
public sealed class MovementsRetryTask
{
    public RetryTaskId TaskId { get; }
    public RetryTaskStatus Status { get; private set; }
	...

	public void SetStatus(RetryTaskStatus status)
    {
        Status = status;
    }

    public void SetSuccess()
    {
        Status = RetryTaskStatus.Success;
    }

    public void SetInProgress()
    {
        Status = RetryTaskStatus.InProgress;
    }

    public void SetError(string? error)
    {
        Status = RetryTaskStatus.Error;
        Error = error;
    } 
	...
}

Несмотря на то, что в данном примере для статуса используется ValueType вместо примитива, что правильно, есть
ошибка, возникшая при сопровождении кода.
К изначальным методам, явно устанавливающим конкретные статусы, добавился общий метод, принимающий любой статус.
И такая казалось бы мелочь очень легко может привести к неправильному состоянию задачи, которое в свою очередь
приведёт к зависанию, ошибочному статусу и т.д.
А ошибиться в передаче ненужного статуса проще, чем в вызове метода

РЕШЕНИЕ: 
- избавился от метода SetStatus и использовать явные методы.
Это хоть и неидеальное решение, но как минимум избавит от установки Success в методе Catch
- 2-е - избавился от метода InProgress, установить его транзакционно на объекте в БД при получении.
Потому что здесь есть проблема гонок между разными подами сервиса

2. Дефолтные конструкторы
Здесь приведу обобщённый пример (они все однообразны).
В большинстве доменных объектов культура в нашей команде запрещает такое использование, поэтому
я не смог найти примера в коде, однако в объектах, работающих на уровне БД (мы используем Dapper)
чаще всего есть такое:
internal sealed class MovementTaskDb
{
    public Guid Id { get; init; }
    public string Client { get; init; } = null!;
	...
}
Т.е. используется дефолтный конструктор (точнее отсутствие заменяет компилятор)
Часто это оправдано, когда например часть полей имеет возможность принимать null - 
и если мы их не получаем, то избавляет от написания специального конструктора для этого случая, 
поскольку иначе даппер не сможет смаппить объект
Однако конструктор полезен как раз для проверки, что объект будет получен и смапплен в тип БД - и далее
попадёт в соответствующий доменный.
Плюс конструктор защищает при маппинге из доменного слоя в слой БД -- мы явно передадим все необходимые поля

// убрали init, добавили конструктор
internal sealed class MovementTaskDb
{
    public Guid Id { get; ; }
    public string Client { get; ; };
	...
	
	public MovementTaskDb(Guid id, string client) {...}
}

Ну или как минимум, если конструктор по каким-то причином использовать нельзя (ограничение даппер)
добавить ко всем необходимым полям Required - гарантию, что поле не забудем инициализировать:
internal sealed class MovementTaskDb
{
    public required Guid Id { get; init; }
    public required string Client { get; init; } = null!;
	...
}

3. Одержимость примитивами
3.1.
В задачах часто используем идентификатор Guid
Но допустим есть задача с подзадачами

internal sealed class MovementSubTask
{
    public Guid Id { get;  }
	public Guid ParentTaksId {get;}
    public string Client { get;  }
	public string OperationId { get;  }
	...
}
В таком сценарии использование примитива может привести к ошибке программы, когда будет передан неправильный Guid
Поэтому лучше явно использовать ValueType, при этом разные:
internal sealed class MovementSubTask
{
    public SubTaskId Id { get;  }
	public TaskId ParentTaksId {get;}
    public string Client { get;  }
	public string OperationId { get;  }
	...
}
// Тогда уже на этапе компиляции мы получим ошибку и исправим её

3.2.
2-й пример не является ошибочным, поскольку в наших проектах мы уже чаще всего используем ValueType
и найти ещё один подходящий оказалось непросто (точнее можно, но показалось это более удачный вариант)
В одном из расчётов было поле, отвечающее за процент сходимости (показатель того, как "сходятся" 2 источника данных)
Я сразу спроектировал нужный тип, который внутри себя инкапсулирует логику расчёта (которая одинакова для всех)
Это позволило избавиться от поля double в типах, где он используется и вынесло исключило дублирование логики
расчёта в разных местах

public struct ConvergencePercent
{
    public double Value { get; }

    private ConvergencePercent(double value)
    {
        Value = value;
    }

    public static ConvergencePercent Calculate(long totalDiff, long totalCount)
    {
        double convergencePercent = (100 - totalDiff / (totalCount / 100.0))/100.0;

        return new ConvergencePercent(convergencePercent);
    }

    public static ConvergencePercent SetFromDb(double value)
    {
        return new ConvergencePercent(value);
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}




