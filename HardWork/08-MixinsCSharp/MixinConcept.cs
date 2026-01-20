namespace HardWork._08_MixinsCSharp;

/*
 * Поскольку C# не поддерживает множественное наследование, миксины могут быть полезны.
 * Чтобы реализовать миксины в C# необходимо использовать интерфейсы + статические классы 
*/


// Пример миксина (переделан базовый пример из Wikipedia)
public interface ILoggerMixin
{
    string source { get; }
}

public static class LoggerMixin
{
    public static void Log(this ILoggerMixin mixin, string source)
    {
        
    }
}

public class MixinConcept
{
    
}