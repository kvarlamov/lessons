namespace OOAP.Poly_Inh;

public class Any : General
{
    
    public static void AssignmentAttempt<T>(Any target, T source)
    {
        target = source is Any src 
            ? src 
            : new None();
    }
}

public class AnySum<T> : Any
{
    public T Sum(T one, T two)
    {
        T res = default;
        if (one is int && two is int)
        {
            res = sumInt(one,two);
        }
        else
        {
            throw new NotImplementedException("Операция не реализована");
        }
        
        //todo - implement other types of sum

        return res;
    }

    private T sumInt(T one, T two)
    {
        int sum = (int)(object)one + (int)(object)one;
        return (T)(object)sum;
    }
}