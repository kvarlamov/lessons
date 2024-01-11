namespace OOAP;

public abstract class BoundedStack<T>
{
    protected const int DefaultMaxSize = 32;
    
    public const int POP_NIL = 0; // push() ещё не вызывалась
    public const int POP_OK = 1;  // последняя pop() отработала нормально
    public const int POP_ERR = 2; // стек пуст

    public const int PEEK_NIL = 0;  // push() ещё не вызывалась
    public const int PEEK_OK = 1;   // последняя peek() отработала нормально
    public const int PEEK_ERR = 2; // стек пуст

    public const int PUSH_NIL = 0; // push() ещё не вызывалась
    public const int PUSH_OK = 1;  // последняя push() отработала нормально
    public const int PUSH_ERR = 2; // стек полон

    #region commands

    /// предусловие: в стеке есть свободное место (размер меньше максимального)
    /// постусловие: в стек добавлено новое значение
    public abstract void Push(T elem);

    /// предусловие: стэк не пустой
    /// постусловие: верхний элемент удален из стека
    public abstract void Pop();
    
    public abstract void Clear();

    #endregion

    #region queries

    /// предусловие: стэк не пустой
    public abstract T Peek();

    public abstract int Size();

    #endregion
}

public class BoundedStackImpl<T> : BoundedStack<T>
{
    private List<T> _stack;
    private int _maxSize;
    private int _peek_status;
    private int _pop_status;
    private int _pushStatus;

    public BoundedStackImpl()
    {
        _stack = new List<T>(DefaultMaxSize);
        _maxSize = DefaultMaxSize;
        _peek_status = PEEK_NIL;
        _pop_status = POP_NIL;
        _pushStatus = PUSH_NIL;
    }
    
    public BoundedStackImpl(int size)
    {
        if (size != 0)
        {
            _stack = new List<T>(size);
            _maxSize = size;
        }
        else
        {
            _stack = new List<T>(DefaultMaxSize);
            _maxSize = DefaultMaxSize;
        }

        _peek_status = PEEK_NIL;
        _pop_status = POP_NIL;
        _pushStatus = PUSH_NIL;
    }

    public override void Push(T elem)
    {
        if (_stack.Count < _maxSize)
        {
            _stack.Add(elem);
            _pushStatus = PUSH_OK;
        }
        else
        {
            _pushStatus = PUSH_ERR;
        }
            
    }

    public override void Pop()
    {
        if (_stack.Count > 0)
        {
            _stack.RemoveAt(_stack.Count - 1);
            _pop_status = POP_OK;
        }
        else
        {
            _pop_status = POP_ERR;
        }
    }

    public override T Peek()
    {
        T result;
        if (_stack.Count > 0)
        {
            result = _stack[^1];
            _peek_status = PEEK_OK;
        }
        else
        {
            result = default;
            _peek_status = PEEK_ERR;
        }

        return result;
    }

    public override int Size() => _stack.Count;

    public override void Clear()
    {
        _stack = new List<T>();
        _pop_status = POP_NIL;
        _peek_status = PEEK_NIL;
        _pushStatus = PUSH_NIL;
    }

    public int GetPopStatus() => _pop_status;
    public int GetPeekStatus() => _peek_status;

    public int GetPushStatus() => _pushStatus;
}