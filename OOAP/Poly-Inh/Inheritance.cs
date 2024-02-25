namespace OOAP.Poly_Inh;
/*
 * Пример наследования - класс BaseHuman - от него наследуются Patient и Doctor.
 * При этом в C# отсутствует множественное наследование от нескольких классов, но можно реализовывать несколько интерфейсов.
 * Пример полиморфизма - Patient - в методе HealMe принимает сущность Doctor,
 * при этом уже в завизимости от того, кем доктор является, будет выполняться лечение текущего пациента.
 * Пример композиции - Surgeon - в конструкторе принимает объект - Tool, инструмент для работы и записывает его в
 * соответствующее поле. Время жизни объекта Tool определяется временем жизни объекта Surgeon
 */


/// <summary>
/// Базовый класс человека
/// </summary>
public class BaseHuman
{
    public int Age { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}

public class Patient : BaseHuman
{
    public void HealMe(Doctor doctor)
    {
        doctor.Heal();
    }
}

public abstract class Doctor : BaseHuman
{
    public abstract void Heal();
}

public class Psychiatrist : Doctor
{
    public override void Heal()
    {
        Console.WriteLine("психиатр лечит");
    }
}

public class Surgeon : Doctor
{
    private readonly Tool _tool;

    public Surgeon(Tool tool)
    {
        _tool = tool;
    }
    
    public override void Heal()
    {
        Console.WriteLine($"хирург лечит, использует {_tool.Name}");
    }
}

/// <summary>
/// Инструмент для работы
/// </summary>
public class Tool
{
    public string Name { get; set; }
}