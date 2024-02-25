namespace OOAP;

public abstract class ParentQueue<T>
{
    protected const int REMOVE_FRONT_OK = 1; // Элемент удален
    protected const int REMOVE_FRONT_ERR = 2; // Очередь пустая
    
    protected const int HEAD_OK = 1; // Значение головы успешно получено
    protected const int HEAD_ERR = 2; // Очередь пустая

    public abstract int GET_RemoveFrontStatus();
    public abstract int GET_HeadStatus();


    #region commands

    /// <summary>
    /// Удаляет значение из головы очереди
    /// </summary>
    /// предусловие: очередь не пустая
    /// постусловие: элемент удален из головы очереди
    public abstract void RemoveFront();

    /// <summary>
    /// Добавить элемент в хвост очереди <br/>
    /// </summary>
    /// постусловие: Элемент добавлен в хвост очереди
    public abstract void AddTail(T value);

    #endregion

    #region queries
    
    /// <summary>
    /// Получить значение из головы очереди
    /// </summary>
    /// предусловие: очередь не пустая
    public abstract T Head();
    
    public abstract int Size();

    #endregion
}

public abstract class SimpleQueue<T> : ParentQueue<T>
{ }

public abstract class Deque<T> : ParentQueue<T>
{
    protected const int REMOVE_TAIL_OK = 1; // значение из хвоста удалено
    protected const int REMOVE_TAIL_ERR = 2; // очередь пустая
    
    protected const int TAil_OK = 1; // Значение хвоста успешно получено
    protected const int TAil_ERR = 2; // Очередь пустая
    
    public abstract int GET_RemoveTailStatus();
    public abstract int TailStatus();
    
    #region commands

    /// <summary>
    /// Добавить элемент в голову очереди <br/>
    /// </summary>
    /// постусловие: Элемент добавлен в голову очереди
    public abstract void AddFront(T value);

    /// <summary>
    /// Удаляет значение из хвоста очереди
    /// </summary>
    /// предусловие: очередь не пустая
    /// постусловие: значение из хвоста удалено
    public abstract void RemoveTail();

    #endregion

    #region queries

    /// <summary>
    /// Получить значение из хвоста очереди
    /// </summary>
    /// предусловие: очередь не пустая
    public abstract T Tail();

    #endregion
}

public class ParentQueueImpl<T> : Deque<T>
{
    protected List<T> _queue;

    private int add_tail_status;
    private int remove_front_status;
    private int head_status;
    
    public ParentQueueImpl()
    {
        _queue = new List<T>();
    }

    public override void RemoveFront()
    {
        if (Size() == 0)
        {
            remove_front_status = REMOVE_FRONT_ERR;
            return;
        }
        
        remove_front_status = REMOVE_FRONT_OK;
        _queue.RemoveAt(_queue.Count - 1);
    }

    public override void AddTail(T value)
    {
        _queue.Insert(0, value);
    }

    public override T Head()
    {
        if (Size() == 0)
        {
            head_status = HEAD_ERR;
            return default;
        }

        head_status = HEAD_OK;
        return _queue[^1];
    }

    public override int Size() => _queue.Count;
    public override int GET_HeadStatus() => head_status;
    public override int GET_RemoveFrontStatus() => remove_front_status;
    
    // not implement for Parent!

    public override int GET_RemoveTailStatus()
    {
        throw new NotImplementedException();
    }

    public override int TailStatus()
    {
        throw new NotImplementedException();
    }

    public override void AddFront(T value)
    {
        throw new NotImplementedException();
    }

    public override void RemoveTail()
    {
        throw new NotImplementedException();
    }

    public override T Tail()
    {
        throw new NotImplementedException();
    }
}

public class DequeuImpl<T> : ParentQueueImpl<T>
{
    private int add_front_status;
    private int remove_tail_status;
    private int tail_status;
    
    public DequeuImpl()
    {
        _queue = new List<T>();
    }


    public override void AddFront(T value)
    {
        _queue.Insert(_queue.Count - 1, value);
    }

    public override void RemoveTail()
    {
        if (Size() == 0)
        {
            remove_tail_status = REMOVE_TAIL_ERR;
            return;
        }

        remove_tail_status = REMOVE_TAIL_OK;
        _queue.RemoveAt(0);
    }

    public override T Tail()
    {
        if (Size() == 0)
        {
            tail_status = TAil_ERR;
            return default;
        }

        tail_status = TAil_OK;
        return _queue[0];
    }

    public override int GET_RemoveTailStatus() => remove_tail_status;
    public override int TailStatus() => tail_status;

}