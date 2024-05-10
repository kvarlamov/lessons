namespace OOAP.Poly_Inh;

// Наследование вариаций

public class BaseProduct
{
    //..some common attributes
}

public class SpecificProduct1 : BaseProduct
{
    // some specific to product attributes
}

public class SpecificProduct2 : BaseProduct
{
    // some specific to product attributes
}

public class BaseTaxCalculator
{
    protected decimal _tax;
    
    // Наследование с вариацией типа (type variation inheritance)
    public void Calculate(BaseProduct product)
    {
        // .. calculate
        _tax = decimal.Zero;
    }

    // Наследование с функциональной вариацией (functional variation inheritance)
    public virtual decimal FinalCalcAndGetTax()
    {
        return _tax;
    }
}

public class SpecificCalculator : BaseTaxCalculator
{
    // Наследование с вариацией типа (type variation inheritance)
    public void Calculate(SpecificProduct1 product)
    {
        // some specific calculation;
        _tax = decimal.Zero;
    }

    // Наследование с вариацией типа (type variation inheritance)
    public void Calculate(SpecificProduct2 product)
    {
        // some specific calculation;
        _tax = decimal.Zero;
    }

    // Наследование с функциональной вариацией (functional variation inheritance)
    public override decimal FinalCalcAndGetTax()
    {
        // модификация логики финального подсчета
        return _tax;
    }
}

// Наследование с конкретизацией (reification inheritance)
public abstract class BaseCarConstructor
{
    public abstract Car Construct();
}

public class SpecCarConstructor : BaseCarConstructor
{
    // Реализуем логику конструирования конкретной машины,
    // для разных машин конструируем по разному
    public override Car Construct()
    {
        //...
        return new Car();
    }
}

public class Car
{ }

//Структурное наследование (structure inheritance)
// также в качестве примера в C# могут выступить стандартные библиотечные интерфейсы
// ICloneable, IEnumerable, IComparable, ISerializable
public interface IConvertable<TDto, TDomain>
{
    // добавление любой сущности в проекте возможности конвертации в доменную
    public TDomain ToDomain(TDto source);

    // добавление возможности конвертации сущности в дто
    public TDto ToDto(TDomain source);
}