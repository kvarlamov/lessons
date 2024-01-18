namespace OOAP;

public abstract class DynArray<T>
{
    protected int _set_status;
    protected int _remove_status;
    protected int _get_status;
    
    public const int SET_OK = 1; // курсор установлен
    public const int SET_ERR = 2; // ошибка - индекс выходит за пределы массива

    public const int REMOVE_OK = 1; // элемент удален
    public const int REMOVE_ERR = 2; // в массиве нет элементов

    public const int GET_OK = 1; // Получен элемент
    public const int GET_ERR = 2; // массив пуст
    
    protected const int Base_Capacity = 16;
    
    // *** commands ***

    public abstract void MakeArray(int newCapacity);

    /// <summary>
    /// Устанавливает курсор на переданный индекс <br/>
    /// </summary>
    /// предусловие: индекс не выходит за пределы массива <br/>
    public abstract void Set_Cursor(int i);
    
    /// <summary>
    /// вставляет элемент по текущему индексу, сдвигая вперёд все последующие элементы <br/>
    /// </summary>
    /// постусловие: новый элемент вставлен в i-ю позицию
    public abstract void Insert(T val);

    /// <summary>
    /// Удаляет элемент по текущему индексу, передвигает последующие элементы на позицию
    /// </summary>
    /// предусловие: массив не пустой <br/>
    public abstract void Remove();

    /// <summary>
    /// Очищает массив
    /// </summary>
    /// постусловие: массив очищен
    public abstract void Clear();

    // *** queries ***
    
    /// <summary>
    /// Получает элемент, на который указывает курсор
    /// </summary>
    /// предусловие: массив не пустой <br/>
    public abstract T Get();

    /// <summary>
    /// Получить текущую величину массива
    /// </summary>
    public abstract int Size();
    
    // *** get statuses ***
    public int Get_SetStatus() => _set_status;
    public int Get_RemoveStatus() => _remove_status;
    public int Get_GetStatus() => _get_status;
}

public class DynArrayImpl<T> : DynArray<T>
{
    private int _capacity;
    private int _count;
    private T [] _array;
    private int _cursor;
    
    public DynArrayImpl() : this(Base_Capacity)
    {
    }

    public DynArrayImpl(int capacity)
    {
        _capacity = capacity;
        _count = 0;
        _cursor = 0;
    }

    public override void MakeArray(int newCapacity)
    {
        Array.Resize(ref _array, newCapacity);
        _capacity = newCapacity;
    }

    public override void Set_Cursor(int i)
    {
        if (IsIndexOutOfRange(i))
        {
            _set_status = SET_ERR;
        }
        else
        {
            _set_status = SET_OK;
        }
    }

    public override void Insert(T val)
    {
        ResizeIfFull();
        for (int i = _cursor; i <= _count; i++)
        {
            (_array[i], val) = (val, _array[i]);
        }

        _count++;
    }

    public override void Remove()
    {
        if (_cursor == 0 || _count == 0)
        {
            _remove_status = REMOVE_ERR;
            return;
        }
        
        for (int i = _cursor; i < _count - 1; i++)
        {
            _array[i] = _array[i + 1];
        }

        _count--;
        ShrinkArr();
        _remove_status = REMOVE_OK;
    }

    public override void Clear()
    {
        _count = 0;
        _cursor = 0;
        MakeArray(Base_Capacity);
    }

    public override T Get()
    {
        if (_cursor == 0 || _count == 0)
        {
            _get_status = GET_ERR;
            return default;
        }

        _get_status = GET_OK;
        return _array[_cursor];
    }

    public override int Size() => _count;

    private void ResizeIfFull()
    {
        if (_count == _capacity)
        {
            MakeArray(_capacity * 2);
        }
    }
    
    private void ShrinkArr()
    {
        double result = (double)_count / _capacity;
        if (result >= 0.5)
            return;

        int newCapacity = (int)(_capacity / 1.5);
        if (newCapacity <= Base_Capacity)
            MakeArray(Base_Capacity);
        else
            MakeArray(newCapacity);
    }

    private bool IsIndexOutOfRange(int index) => index > _count || index < 0;
}