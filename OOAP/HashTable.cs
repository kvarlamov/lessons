namespace OOAP;

public abstract class HashTable<T>
{
    protected const int PUT_OK = 1; // значение добавлено
    protected const int PUT_ERR = 2; // ошибка добавления 

    public const int REMOVE_OK = 1; // значение удалено
    public const int REMOVE_ERR = 2; // значение не найдено

    public abstract int GET_PUT_STATUS();
    public abstract int GET_REMOVE_STATUS();
    
    #region Commands

    /// <summary>
    /// Добавить значение в таблицу
    /// </summary>
    /// постусловие: значение добавлено
    public abstract void Put(T value);

    /// <summary>
    /// Удалить значение из таблицы
    /// </summary>
    /// постусловие: элемент удален
    public abstract void Remove(T value);

    #endregion

    #region Queries

    /// <summary>
    /// Проверяет, принадлежит ли значение таблице
    /// </summary>
    public abstract bool Contains(T value);

    #endregion
}

public class HashTableImpl<T> : HashTable<T>
{
    private int size;
    private int put_status;
    private int remove_status;
    private int step = 3;
    private T[] slots;

    public HashTableImpl(int sz)
    {
        size = sz;
        slots = new T[size];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = default;
        }
    }

    public override void Put(T value)
    {
        int index = SeekSlot(value);
        if (index == -1)
        {
            put_status = PUT_ERR;
            return;
        }

        slots[index] = value;
        put_status = PUT_OK;
    }

    public override void Remove(T value)
    {
        int index = HashFun(value);
        var finalIndex = Seek(index, value);
        if (finalIndex == -1)
        {
            remove_status = REMOVE_ERR;
            return;
        }

        slots[finalIndex] = default;
        remove_status = REMOVE_OK;
    }

    public override bool Contains(T value)
    {
        int index = HashFun(value);
        return Seek(index, value) != -1;
    }
    
    private int SeekSlot(T value)
    {
        int index = HashFun(value);
        return Seek(index);
    }
    
    private int HashFun(T value)
    {
        var code = value!.GetHashCode();
        var res = code % size;
            
        return res;
    }
    
    private int Seek(int index, T value = default)
    {
        int maxCircles = size;
        int currStep = step;
        for (int circle = 0; circle <= maxCircles; circle++)
        {
            if (slots[index]!.Equals(value))
                return index;

            int i = index + step;
                
            for (; i < size; i += currStep)
            {
                if (slots[i]!.Equals(value))
                    return i;
            }

            index = index + 1 > size - 1 ? 0 : index + 1;
            currStep = currStep * step >= size ? step : currStep * step ;
        }

        return -1;
    }

    public override int GET_PUT_STATUS() => put_status;
    public override int GET_REMOVE_STATUS() => remove_status;
}