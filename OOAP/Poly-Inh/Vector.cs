namespace OOAP.Poly_Inh;

internal sealed class Vector<T> : AnySum<T>
{
    private T[] items;
    private int length;

    public Vector(T[] items)
    {
        items = items;
        length = items.Length;
    }

    public Vector(int length)
    {
        items = new T[length];
        this.length = length;
    }

    public void Add(Vector<T> other)
    {
        if (items.Length != other.items.Length)
        {
            return;
        }

        for (int i = 0; i < length; i++)
        {
            items[i] = Sum(items[i], other.items[i]);
        }
    }
}