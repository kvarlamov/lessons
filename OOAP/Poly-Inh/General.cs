using System.Text.Json;
using DeepCopy;

namespace OOAP.Poly_Inh;

public class General : Object
{
    // https://code-maze.com/csharp-deep-copy-of-object/
    // копирует содержимое одного объекта
    public void DeepCopyTo<T>(T? copyTarget) => copyTarget = GetCopy<T>();

    public T? Clone<T>() => GetCopy<T>();

    public T FastClone<T>(T input) => DeepCopier.Copy(input);

    public string Serialize() => JsonSerializer.Serialize(this);

    public T? Deserialize<T>(string input) => JsonSerializer.Deserialize<T>(input);

    public static bool DeepEquals<T>(T? x, T? y)
    {
        if (ReferenceEquals(x, y)) return true;

        if (x is null || y is null) return false;
        if (x.GetType() != y.GetType()) return false;

        var isEqual = true;
        foreach (var property in x.GetType().GetProperties())
        {
            object firstVal = property.GetValue(x);
            object secondVal = property.GetValue(y);
            if (firstVal != null && firstVal.GetType().IsClass && !firstVal.GetType().FullName.StartsWith("System."))
            {
                isEqual = DeepEquals(firstVal, secondVal);
                if (!isEqual)
                    return false;
            }
            else
            {
                if (!firstVal.Equals(secondVal)) return false;
            }
        }

        return isEqual;
    }


    // При необходимости потомок может переопределить данный метод
    public virtual string ToString() => base.ToString();

    private T? GetCopy<T>()
    {
        var current = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize<T>(current);
    }
}

public class Any : General
{
    
}