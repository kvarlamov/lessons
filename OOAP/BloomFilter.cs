namespace OOAP;

// Возможны ложные срабатывания - найден несуществующий, но существующий найден всегда
public abstract class BloomFilter
{
    /// <summary>
    /// Добавляет строку в фильтр
    /// </summary>
    /// постусловие: строка добавлена в фильтр
    public abstract void Add(string str);

    /// <summary>
    /// Проверяет, присутствует ли строка в фильтре
    /// </summary>
    public abstract bool IsValue(string str);
}

public class BloomFilterImpl : BloomFilter
{
    private readonly int filter_len;
    private int bittest;

    public BloomFilterImpl(int filterLen)
    {
        filter_len = filterLen;
        this.bittest = 0;
    }
    
    public override void Add(string str)
    {
        int hash1 = Hash1(str);
        int hash2 = Hash2(str);

        var bitmask1 = 1 << hash1;
        var bitmask2 = 1 << hash2;
        bittest |= bitmask1;
        bittest |= bitmask2;
    }

    public override bool IsValue(string str)
    {
        int hash1 = Hash1(str);
        int hash2 = Hash2(str);

        return (bittest & (1 << hash1)) != 0 && (bittest & (1 << hash2)) != 0;
    }
    
    public int Hash1(string str1)
    {
        // 17
        int n = 17;
        int res = 0;
        for(int i=0; i<str1.Length; i++)
        {
            int code = (int)str1[i];
            unchecked
            {
                res = Math.Abs((res * n + code) % filter_len);
            }
        }
            
        return res;
    }
    public int Hash2(string str1)
    {
        // 223
        int n = 223;
        int res = 0;
        for(int i=0; i<str1.Length; i++)
        {
            int code = (int)str1[i];
            unchecked
            {
                res = Math.Abs((res * n + code) % filter_len);
            }
                
        }
            
        return res;
    }
}