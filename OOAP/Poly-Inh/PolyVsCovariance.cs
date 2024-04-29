namespace OOAP.Poly_Inh;

internal sealed class PolyVsCovariance
{
    public static void Demonstrate()
    {
        Zoo zoo = new Zoo();
        Animal1 cat1 = new Cat1("Timofei");
        Dog1 dog1 = new Dog1("Bobik");
            
        // Пример полиморфного вызова метода
        zoo.AcceptAnimal(cat1);
        zoo.AcceptAnimal(dog1);
            
        var cats = new List<Animal1> {cat1, new Cat1("Boris")};
        // Пример ковариантного вызова метода
        zoo.AcceptMany(cats);
    }
    
}

public class Zoo
{
    public void AcceptAnimal(Animal1 animal)
    {
        Console.WriteLine($"accepted {animal.ToString()}");
    }

    public void AcceptMany<T>(T animals)
    {
        if (typeof(T) == typeof(List<Animal1>))
        {
            foreach (var animal in (IEnumerable<Animal1>)animals)
            {
                AcceptAnimal(animal);
            }
        }
    }
}

public abstract class Animal1
{
    public string Name { get; init; }
    public Animal1(string name)
    {
        Name = name;
    }
}

public class Cat1 : Animal1
{
    public Cat1(string name) : base(name)
    {
    }

    public override string ToString()
    {
        return $"CAT {Name}";
    }
}

public class Dog1 : Animal1
{
    public Dog1(string name) : base(name)
    {
    }
    
    public override string ToString()
    {
        return $"DOG {Name}";
    }
}