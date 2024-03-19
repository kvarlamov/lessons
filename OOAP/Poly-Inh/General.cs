using System.Text.Json;
using DeepCopy;

namespace OOAP.Poly_Inh;

public class General : Object
{
    // https://code-maze.com/csharp-deep-copy-of-object/
    // копирует содержимое одного объекта
    public void DeepCopyTo<T>(T objToCopy)
    {
        objToCopy = GetCopy<T>();
        // var type = this.GetType();
        // var props = type.GetProperties();
        //
        // foreach (var property in props)
        // {
        //     if (!property.CanWrite) 
        //         continue;
        //     
        //     object value = property.GetValue(this);
        //     if (value != null && value.GetType().IsClass && !value.GetType().FullName.StartsWith("System."))
        //     {
        //         property.SetValue(objToCopy, DeepCopyTo(value));
        //     }
        //     else
        //     {
        //         property.SetValue(objToCopy, value);
        //     }
        // }
        //
        // return default;
    }

    private T GetCopy<T>()
    {
        // todo - check speed by using
        var current = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize<T>(current);
    }

    public T? Clone<T>()
    {
        var current = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize<T>(current);
    }

    public General FastClone<T>()
    {
        return DeepCopier.Copy(this);
    }

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
    public virtual string ToString()
    {
        return base.ToString();
    }
}

public class Any : General
{
    
}