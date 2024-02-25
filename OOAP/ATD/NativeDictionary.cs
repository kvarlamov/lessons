namespace OOAP;

public abstract class NativeDictionary<T>
{
    public const int PUT_OK = 1; // в словарь добавлено значение по ключу
    public const int PUT_ERR = 2; // в словаре не найден пустой слот

    public const int GET_OK = 1; // команда выполнена успешно
    public const int GET_ERR = 2; // ключ не найден

    public abstract int GET_PUT_STATUS();
    public abstract int GET_GET_STATUS();
    
    #region commands

    /// <summary>
    /// Добавление в словарь значения по ключу
    /// </summary>
    /// предусловие: в словаре есть пустой слот
    /// постусловие: значение по указанному ключу добавлено
    public abstract void Put(string key, T value);

    #endregion
    

    #region queries

    /// <summary>
    /// Получает значение по ключу
    /// </summary>
    /// предусловие: ключ существует
    public abstract T Get(string key);

    public abstract int Size();

    #endregion

}

public class NativeDictionaryImpl<T> : NativeDictionary<T>
{
    private int put_status;
    private int get_status;
    private const int step = 3;
    private int size;
    private string [] slots;
    private T [] values;

    public NativeDictionaryImpl(int sz)
    {
        size = sz;
        slots = new string[size];
        values = new T[size];
    }

    public override void Put(string key, T value)
    {
        int index = HashFun(key);
            
        index = IsKey(key) ? Seek(index, key) : Seek(index);

        if (index == -1)
        {
            put_status = PUT_ERR;
            return;
        }
            
        slots[index] = key;
        values[index] = value;
        put_status = PUT_OK;
    }

    public override T Get(string key)
    {
        int index = HashFun(key);

        int finalIndex = Seek(index, key);

        if (finalIndex == -1)
        {
            get_status = GET_ERR;
            return default;
        }

        get_status = GET_OK;
        return values[finalIndex];
    }

    public override int Size() => size;

    public override int GET_PUT_STATUS() => put_status;
    
    public override int GET_GET_STATUS() => get_status;
    
    private int HashFun(string key)
    {
        byte[] data = System.Text.Encoding.UTF8.GetBytes(key);
        int sum = 0;
        for (int i = 0; i < data.Length; i++)
        {
            sum += data[i];
        }
            
        var res = sum % size;
            
        return res;
    }

    private bool IsKey(string key)
    {
        int index = HashFun(key);

        return Seek(index, key) != -1;
    }
    
    private int Seek(int index, string value = null)
    {
        int maxCircles = size;
        int currStep = step;
        for (int circle = 0; circle <= maxCircles; circle++)
        {
            if (slots[index] == value)
                return index;

            int i = index + step;
                
            for (; i < size; i += currStep)
            {
                if (slots[i] == value)
                    return i;
            }

            index = index + 1 > size - 1 ? 0 : index + 1;
            currStep = currStep * step >= size ? step : currStep * step ;
        }

        return -1;
    }
}