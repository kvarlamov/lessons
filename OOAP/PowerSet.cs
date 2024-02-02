namespace OOAP;



public abstract class PowerSet<T> : HashTable<T>
{
    public const int Put_OK = 1; // значение добавлено
    public const int Put_Exist = 2; // значение уже существует в множестве
    public const int Put_Err = 3; // достигнут предельный размер множества
    
    #region queries

    /// <summary>
    /// Пересечение множеств
    /// </summary>
    public abstract PowerSet<T> Intersection(PowerSet<T> set2);

    /// <summary>
    /// Объединение множеств
    /// </summary>
    public abstract PowerSet<T> Union(PowerSet<T> set2);

    /// <summary>
    /// Разность множеств
    /// </summary>
    public abstract PowerSet<T> Difference(PowerSet<T> set2);

    /// <summary>
    /// Проверяет, является ли множество set2 подмножеством текущего
    /// </summary>
    public abstract bool IsSubset(PowerSet<T> set2);

    public abstract int Size();

    public abstract IEnumerable<T> GetEntries();

    #endregion
}

public class PowerSetImpl<T> : PowerSet<T>
{
    private readonly List<T> _entries;
    private readonly int _size;

    private int put_status;
    private int remove_status;

    public PowerSetImpl(int size)
    {
        _size = size;
        _entries = new List<T>(size);
    }

    public override int GET_PUT_STATUS() => put_status;

    public override int GET_REMOVE_STATUS() => remove_status;

    // предусловие: значения нет в множестве, величина множаства меньше максимальной
    public override void Put(T value)
    {
        if (_entries.Contains(value))
        {
            put_status = Put_Exist;
            return;
        }

        if (_entries.Count == _size)
        {
            put_status = PUT_ERR;
            return;
        }

        put_status = PUT_OK;
        _entries.Add(value);
    }

    public override void Remove(T value) => remove_status = _entries.Remove(value) ? REMOVE_OK : REMOVE_ERR;

    public override bool Contains(T value) => _entries.Contains(value);

    public override PowerSet<T> Intersection(PowerSet<T> set2)
    {
        var newSet = new PowerSetImpl<T>(Size());
        foreach (var entry in _entries)
        {
            if (entry == null)
                continue;
                
            if (set2.Contains(entry))
                newSet.Put(entry);
        }

        return newSet;
    }

    public override PowerSet<T> Union(PowerSet<T> set2)
    {
        var newSet = new PowerSetImpl<T>(Size() + set2.Size());
        foreach (var entry in _entries)
        {
            if (entry == null)
                continue;
                
            newSet.Put(entry);
        }

        foreach (var entry in set2.GetEntries())
        {
            if (entry == null)
                continue;
                
            newSet.Put(entry);
        }
            
        return newSet;
    }

    public override PowerSet<T> Difference(PowerSet<T> set2)
    {
        var newSet = new PowerSetImpl<T>(Size());
        foreach (var entry in _entries)
        {
            if (entry == null)
                continue;
                
            if (!set2.Contains(entry))
                newSet.Put(entry);
        }
            
        return newSet;
    }

    public override bool IsSubset(PowerSet<T> set2)
    {
        throw new NotImplementedException();
    }

    public override int Size() => _entries.Count;
    
    public override IEnumerable<T> GetEntries()
    {
        foreach (var entry in _entries)
        {
            yield return entry;
        }
    }
}