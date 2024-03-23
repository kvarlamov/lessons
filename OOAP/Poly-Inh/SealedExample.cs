using System.Text.Json;

namespace OOAP.Poly_Inh;

/*
 * В C# можно поставить модификатор sealed, который запрещает создавать потомков класса
 * Также данный модификатор можно применить к переопределенному (override) методу
 * Т.о. поставить на любой метод данный модификатор возможности нет
 *
 * При этом если метод не виртуальный, мы не можем сделать override, однако есть возможность
 * создать метод с тем же именем в потомке (и добавить ключевое слово new) - тогда при создании экземпляра потомка
 * будет вызываться его метод
 *
 * Чтобы запретить переопределение методов потомками базовый метод делается virtual
 * А в переопределенном добавляется ключевое слово sealed
 *
 * В примере ниже метод DeepCopyTo в классе AnySealed "запечатывается" и мы уже
 * не можем переопределить его ниже по иерархии наследования в SubAny - ошибка компиляции
 * При этом Clone не запечатан - и может быть переопределен
 */
public class SealedExample
{
    public virtual void DeepCopyTo<T>(T? copyTarget) => copyTarget = GetCopy<T>();

    public virtual T? Clone<T>() => GetCopy<T>();
    
    private T? GetCopy<T>()
    {
        var current = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize<T>(current);
    }
}

public class AnySealed : SealedExample
{
    public sealed override void DeepCopyTo<T>(T? copyTarget) where T : default
    {
        base.DeepCopyTo(copyTarget);
    }

    public override T? Clone<T>() where T : default
    {
        return base.Clone<T>();
    }
}

public class SubAny : AnySealed
{
    public override void DeepCopyTo<T>(T? copyTarget) where T : default
    {
        base.DeepCopyTo(copyTarget);
    }
    
    public override T? Clone<T>() where T : default
    {
        return base.Clone<T>();
    }
}