namespace OOAP;

public sealed class Node<T>
{
    public T value;
    public Node<T> right, left;
        
    public Node(T val) 
    { 
        value = val; 
        right = null;
        left = null;
    }
}

public class ParentListImpl<T> : ParentList<T>
{
    private int _head_status; // успешно; список пуст
    private int _tail_status; // успешно; список пуст
    private int _right_status; // успешно; правее нету элемента
    private int _put_right_status; // успешно; список пуст
    private int _put_left_status; // успешно; список пуст
    private int _remove_status; // успешно; список пуст
    private int _replace_status; // успешно; список пуст
    private int _find_status; // следующий найден; // следующий не найден; список пуст
    private int _get_status; // успешно; список пуст
    
    protected Node<T> _head;
    protected Node<T> _tail;
    protected Node<T> _cursor;
    
    public ParentListImpl()
    {
        _head = null;
        _tail = null;
        _cursor = null;
    }
    
    public override void Head()
    {
        if (_head == null)
        {
            _head_status = HEAD_ERR;
        }
        else
        {
            _cursor = _head;
            _head_status = HEAD_OK;
        }
    }

    public override void Tail()
    {
        if (_tail is null)
        {
            _tail_status = TAIL_ERR;
        }
        else
        {
            _cursor = _tail;
            _tail_status = TAIL_OK;
        }
    }

    public override void Right()
    {
        if (_cursor is null)
        {
            _right_status = RIGHT_ERR_EMPTY;
            return;
        }
        
        if (_cursor.right is null)
        {
            _right_status = RIGHT_ERR_NORIGHT;
        }
        else
        {
            _cursor = _cursor.right;
            _right_status = RIGHT_OK;
        }
    }

    public override void PutRight(T value)
    {
        if (_cursor is null || (_head == null && _tail == null))
        {
            _put_right_status = PUT_RIGHT_ERR;
            return;
        }
        
        // справа от курсора добавляется новый элемент
        var newNode = new Node<T>(value);
        var currentRightToCursor = _cursor.right;
        _cursor.right = newNode;
        newNode.left = _cursor;
        if (currentRightToCursor != null)
        {
            currentRightToCursor.left = newNode;
            newNode.right = currentRightToCursor;
        }
        _put_right_status = PUT_RIGHT_OK;
    }

    public override void PutLeft(T value)
    {
        if (_cursor is null || (_head == null && _tail == null))
        {
            _put_right_status = PUT_LEFT_ERR;
            return;
        }
        
        // слева от курсора добавляется новый элемент
        var newNode = new Node<T>(value);
        var currentLeftToCursor = _cursor.left;
        _cursor.left = newNode;
        newNode.right = _cursor;
        if (currentLeftToCursor != null)
        {
            currentLeftToCursor.right = newNode;
            newNode.left = currentLeftToCursor;
        }
        _put_right_status = PUT_RIGHT_OK;
    }

    public override void Remove()
    {
        if (_cursor is null || (_head == null && _tail == null))
        {
            _remove_status = REMOVE_ERR;
            return;
        }
        
        if (_cursor.right != null)
        {
            var leftToCursor = _cursor.left;
            _cursor = _cursor.right;
            _cursor.left = leftToCursor;
            if (leftToCursor != null)
            {
                leftToCursor.right = _cursor;
            }

            _remove_status = REMOVE_OK;
            return;
        }

        if (_cursor.left != null)
        {
            var rightToCursor = _cursor.right;
            _cursor = _cursor.left;
            _cursor.right = rightToCursor;
            if (rightToCursor != null)
            {
                rightToCursor.left = _cursor;
            }
            
            _remove_status = REMOVE_OK;
        }
    }

    public override void Clear()
    {
        _head = null;
        _tail = null;
        _cursor = null;
    }

    public override void AddTail(T value)
    {
        var newNode = new Node<T>(value);
        if (_head == null) 
        {
            _head = newNode;
            _head.right = null;
            _head.left = null;
        } 
        else 
        {
            _tail.right = newNode;
            newNode.left = _tail;
        }
        _tail = newNode;
    }

    public override void Replace(T value)
    {
        if (_cursor is null || (_head == null && _tail == null))
        {
            _replace_status = REPLACE_ERR;
            return;
        }

        _cursor.value = value;
        _replace_status = REPLACE_OK;
    }

    public override void Find(T value)
    {
        if (_cursor is null || (_head == null && _tail == null))
        {
            _find_status = FIND_ERR_EMPTY;
            return;
        }

        var current = _cursor.right;
        while (current != null)
        {
            if ((value == null && current.value == null) || current.value != null && current.value.Equals(value))
            {
                _cursor = current;
                _find_status = FIND_OK;
                return;
            }

            current = current.right;
        }

        _find_status = FIND_ERR_NOTFOUND;
    }

    public override void RemoveAll(T value)
    {
        while (_find_status != FIND_ERR_NOTFOUND || _find_status != FIND_ERR_EMPTY)
        {
            Find(value);
            Remove();
        }
    }

    public override T Get()
    {
        if (_cursor is null || (_head == null && _tail == null))
        {
            _get_status = GET_ERR;
            return default;
        }

        _get_status = GET_OK;
        return _cursor.value;
    }

    public override int Size()
    {
        int count = 0;
        Node<T> current = _head;
        while (current != null)
        {
            count++;
            current = current.right;
        }

        return count;
    }

    public override bool IsHead() => _cursor == _head;

    public override bool IsTail() => _cursor == _tail;

    public override bool IsValue() => _cursor != null;
}