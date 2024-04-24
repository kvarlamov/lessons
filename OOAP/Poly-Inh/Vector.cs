namespace OOAP.Poly_Inh;

internal sealed class Vector<T> : AnySum<T>
{
    private T[] _items;
    private int _length;

    public Vector(T[] items)
    {
        _items = items;
        _length = items.Length;
    }

    public Vector(int length)
    {
        _items = new T[length];
        this._length = length;
    }

    public void Add(Vector<T>? other)
    {
        if (other != null && _items.Length != other._items.Length)
        {
            return;
        }

        for (int i = 0; i < _length; i++)
        {
            if (other!._items[i]?.GetType() == typeof(Vector<T>))
            {
                (_items[i] as Vector<T>)?.Add((other._items[i] as Vector<T>));
            }
            else
            {
                _items[i] = Sum(_items[i], other._items[i]);
            }
        }
    }
}