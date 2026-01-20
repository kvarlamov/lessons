namespace MixinsConcept;

/*
 * Поскольку C# не поддерживает множественное наследование, миксины могут быть полезны.
 * Чтобы реализовать миксины в C# необходимо использовать интерфейсы + статические классы
 * И далее в классе реализовать интерфейсы миксинов
 */

// Пример 1: миксин логгера (+ получения времени) - переделал базовый пример из Wikipedia на java https://en.wikipedia.org/wiki/Mixin
// для того чтобы ознакомиться с этим понятием
public interface ILoggerMixin
{
    string Source { get; }
}

public static class LoggerMixin
{
    public static void Log(this ILoggerMixin mixin, string source, string time)
    {
        Console.WriteLine($"[log][{time}]: {source}");
    }
}

public interface ITimeLoggerMixin;

public static class TimeLoggerMixin
{
    public static string GetLoggingTime(this ITimeLoggerMixin mixin)
    {
        return DateTime.UtcNow.ToString("HH:mm:ss.fff");
    }
}

public sealed class MixinExample1 : ILoggerMixin, ITimeLoggerMixin
{
    public string Source => nameof(MixinExample1);

    public void StartSomeWork()
    {
        // ... here some work
        
        // Использование миксина в коде класса
        this.Log(Source, this.GetLoggingTime());
    }
}

// Пример 2: Из рабочего проекта - абстрактный класс отчёта, в котором есть метод для получения строки для csv файла

// Текущая реализация (лишние методы убрал для улучшения читаемости)
public abstract class KeyReport
{
    public abstract IKey Key { get; }
    public abstract ReconciliationCode Code { get; }
    public ReconciliationSide Side { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public int Count { get; init; }
    public KeyReportJob KeyReportingJob { get; }

    protected KeyReport(
        ReconciliationSide side,
        DateTime createdAt,
        DateTime updatedAt,
        KeyReportJob keyReportingJob)
    {
        Side = side;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        KeyReportingJob = keyReportingJob;
    }

    public abstract string GetCsvReportLine();
    public abstract string GetCsvReportHeaderLine();
}

public sealed class C15KeyReport : KeyReport
{
    public override ReconciliationCode Code => ReconciliationCode.C15;

    public override C15Key Key { get; }

    private C15KeyReport(
        ReconciliationSide side,
        DateTime createdAt,
        DateTime updatedAt,
        KeyReportJob keyReportingJob,
        C15Key key) : base(side, createdAt, updatedAt, keyReportingJob)
    {
        Key = key;
    }
    
    //...

    public override string GetCsvReportLine()
    {
        var key = Key.ToString();
        var date = CreatedAt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        var count = Count.ToString();

        return $"{key},{date},{count}";
    }

    public override string GetCsvReportHeaderLine()
    {
        return $"inventory_exemplar_id,posting_number,created_at,count";
    }
}

// Использование:

public class ReportProcessor
{
    public async Task ProcessReport(List<KeyReport> reports, CancellationToken ct)
    {
        //...
        var header = rep[0].GetCsvReportHeaderLine();
        foreach (var rep in reports)
        {
            var line = rep.GetCsvReportLine();
        }

        //...

    }
}
/*
Проблема - в каждом из классов-наследников KeyReport приходится переопределять методы базового класса
GetCsvReportHeaderLine и GetCsvReportLine, что загромождает классы и нарушает SRP
Пока класс-наследник 1 это не так заметно, однако при увеличении классов логика будет дублироваться

Применим миксины:
*/
public interface ICsvKeyReportMixin
{
    public string GetKey();
    public DateTime CreatedAt { get; }
    public int Count { get; }
}

public static class CsvKeyReportMixinExtensions
{
    public static string GetCsvReportLine(this ICsvKeyReportMixin keyReportMixin)
    {
        string key = keyReportMixin.GetKey();
        var date = keyReportMixin.CreatedAt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        var count = keyReportMixin.Count.ToString();

        return $"{key},{date},{count}";
    }
  
  public static string GetCsvHeader(this ICsvKeyReportMixin keyReportMixin)
  {
    return keyReportMixin.GetHeader();
  }
}

public sealed class C15KeyReport : KeyReport, ICsvKeyReportMixin
{
    public override ReconciliationCode Code => ReconciliationCode.C15;

    public override C15Key Key { get; }

    private C15KeyReport(
        ReconciliationSide side,
        DateTime createdAt,
        DateTime updatedAt,
        KeyReportJob keyReportingJob,
        C15Key key) : base(side, createdAt, updatedAt, keyReportingJob)
    {
        Key = key;
    }
  
    //...

    public string GetHeader() => "inventory_exemplar_id,posting_number,created_at,count";

    public string GetKey() => Key.ToString();
}

public abstract class KeyReport
{
    public abstract IKey Key { get; }
    public abstract ReconciliationCode Code { get; }
    public ReconciliationSide Side { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public int Count { get; init; }
    public KeyReportJob KeyReportingJob { get; }

    protected KeyReport(
        ReconciliationSide side,
        DateTime createdAt,
        DateTime updatedAt,
        KeyReportJob keyReportingJob)
    {
        Side = side;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        KeyReportingJob = keyReportingJob;
    }

    public string GetCsvReportLine()
      {
        if (this is ICsvKeyReportMixin mixin)
          return mixin.GetCsvReportLine();
      }
  
    public string GetCsvReportHeaderLine()
      {
        if (this is ICsvKeyReportMixin mixin)
          return mixin.GetHeader();
      }
}

/*
 * Показанный пример показывает, как у некоторой сущности (KeyReport) можно расширить фукционал
 * Однако на текущий момент в языке программирования C# кажется он выглядит лишь усложнением,
 * которое привнесёт в проект лишь ненужную сложность, поскольку требует как наличия интерфейса
 * миксина, так и статического класса с его использованием, несмотря на то, что интерфейсы уже позволяют
 * иметь реализацию по умолчанию. В этом смысле в Java это выглядит более удобоиспользуемым
 *
 * В любом случае удалось ознакомиться с концептом миксинов и возможно удастся применить их в реальных проектах
 */