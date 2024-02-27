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
// т.е. все доктора - люди, но не все люди - доктора
public abstract class Doctor : Human
{
    public int WorkExperience { get; set; }

    public abstract void Heal();
}

// специализация класса родителя - определяем специальность через переопределение базового метода
// более частный случай доктора - все хирурги - доктора, но не все доктора - хирурги
public class Surgeon : Doctor
{
    public override void Heal()
    {
        // operation
    }
}