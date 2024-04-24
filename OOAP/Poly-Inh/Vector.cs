namespace OOAP.Poly_Inh;

internal sealed class Vector<T> : General
    where T : Any
{
    private T[] items;

    public Vector(int length)
    {
        items = new T[length];
    }

    public Any Add(Vector<T> other)
    {
        if (items.Length != other.items.Length)
        {
            return null;
        }
    
        Any result = default;
        foreach (var item in items)
        {
            result += item;
        }

        foreach (var item in other.items)
        {
            result += item;
        }

        return result;
    }
}