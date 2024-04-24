namespace OOAP.Poly_Inh;

public class Any : General
{
    private object _value;

    public Any(object value)
    {
        _value = value;
    }
    
    public static void AssignmentAttempt<T>(Any target, T source)
    {
        target = source is Any src 
            ? src 
            : new None();
    }

    public Any Add(Any other)
    {
        if (_value is int && other._value is int)
        {
            int sum = (int)_value + (int)other._value;
            return new Any(sum);
        }
        else
        {
            throw new NotImplementedException("Операция не реализована");
        }
    }


    public static Any operator +(Any first, Any second)
    {
        return first.Add(second);
    }
}