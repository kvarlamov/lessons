namespace OOAP.Poly_Inh;

// Implementation Inheritance
// Наследование реализации представлено в виде АТД Список и его реализации на основе Span
public abstract class AbstractList<T>
{
    public abstract T this[int index] { get; set; }
    public abstract void Add(T item);
    public abstract void Clear();
    public abstract int Count { get; }
}

public class SpanList<T> : AbstractList<T>
{
    private Memory<T> _items;
    private int _size;

    public SpanList(int initialCapacity)
    {
        _items = new Memory<T>(new T[initialCapacity]);
        _size = 0;
    }

    public override T this[int index]
    {
        get => _items.Span[index];
        set => _items.Span[index] = value;
    }

    public override void Add(T item)
    {
        //...
    }

    public override void Clear()
    {
        _items = new Memory<T>(new T[_items.Length]);
        _size = 0;
    }

    public override int Count => _size;
}

// Facility Inheritance 
// Представлено в виде доменного наследника класса Exception
public class DomainException : Exception
{
    public DomainException() : base() {}

    public DomainException(string message) : base(message) {}

    public DomainException(string message, Exception innerException) : 
        base(message, innerException) {}
}