namespace OOAP.Poly_Inh;

/// <summary>
/// Базовый класс человека
/// </summary>
public class Human
{
    public int Age { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}

// расширение класса-родителя - Doctor расширяет функционал базового класса Human, добавляя новые доступные свойства и поведение
public abstract class Doctor : Human
{
    public int WorkExperience { get; set; }

    public abstract void Heal();
}

// специализация класса родителя - определяем специальность через переопределение базового метода 
public class Surgeon : Doctor
{
    public override void Heal()
    {
        // operation
    }
}