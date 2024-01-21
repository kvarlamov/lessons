namespace OOAP;

public abstract class Queue<T>
{
    protected const int DEQUEUE_NIL = 0; // Enqueue еще не вызывалась
    protected const int DEQUEUE_OK = 1; // Элемент удален
    protected const int DEQUEUE_ERR = 2; // Очередь пустая
    
    protected const int HEAD_NIL = 0; // Enqueue еще не вызывалась
    protected const int HEAD_OK = 1; // Значение головы успешно получено
    protected const int HEAD_ERR = 2; // Очередь пустая
    
    protected const int TAil_NIL = 0; // Enqueue еще не вызывалась
    protected const int TAil_OK = 1; // Значение хвоста успешно получено
    protected const int TAil_ERR = 2; // Очередь пустая
    
    #region Commands

    /// <summary>
    /// Добавить элемент в хвост очереди <br/>
    /// </summary>
    /// постусловие: Элемент добавлен в хвост очереди
    public abstract void Enqueue(T value);
    
    /// <summary>
    /// Очищает очередь
    /// </summary>
    /// постусловие: очередь очищена
    public abstract void Clear();

    /// <summary>
    /// Удаляет значение из головы очереди
    /// </summary>
    /// предусловие: очередь не пустая
    public abstract void Dequeue();

    #endregion

    #region Queries

    /// <summary>
    /// Получить значение из головы очереди
    /// </summary>
    /// предусловие: очередь не пустая
    public abstract T Head();

    /// <summary>
    /// Получить значение из хвоста очереди
    /// </summary>
    /// предусловие: очередь не пустая
    public abstract T Tail();
    
    public abstract int Size();

    #endregion
}

public class QueueImpl<T> : Queue<T>
{
    private List<T> _queue;

    private int enqueue_status;
    private int dequeue_status;
    private int head_status;
    private int tail_status;

    public QueueImpl()
    {
        _queue = new List<T>();
        head_status = HEAD_NIL;
        tail_status = TAil_NIL;
        dequeue_status = DEQUEUE_NIL;
        enqueue_status = 0;
    }
    
    public override void Enqueue(T value)
    {
        _queue.Insert(0, value);
        enqueue_status = 1;
    }

    public override void Clear()
    {
        _queue.Clear();
        head_status = HEAD_NIL;
        tail_status = TAil_NIL;
        dequeue_status = DEQUEUE_NIL;
        enqueue_status = 0;
    }

    public override void Dequeue()
    {
        if (enqueue_status == 0)
        {
            dequeue_status = DEQUEUE_NIL;
            return;
        }
        
        if (Size() == 0)
        {
            dequeue_status = DEQUEUE_ERR;
            return;
        }
        
        _queue.RemoveAt(_queue.Count - 1);
    }

    public override T Head()
    {
        if (enqueue_status == 0)
        {
            dequeue_status = HEAD_NIL;
            return default;
        }
        
        if (Size() == 0)
        {
            head_status = HEAD_ERR;
            return default;
        }

        head_status = HEAD_OK;
        return _queue[^1];
    }

    public override T Tail()
    {
        if (enqueue_status == 0)
        {
            tail_status = TAil_NIL;
            return default;
        }
        
        if (Size() == 0)
        {
            tail_status = TAil_ERR;
            return default;
        }

        tail_status = TAil_OK;
        return _queue[0];
    }

    public override int Size() => _queue.Count;

    public int Get_Dequeue_Status => dequeue_status;
    public int Get_Head_Status => head_status;
    public int Get_Tail_Status => tail_status;
}