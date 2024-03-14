namespace OOAP.Poly_Inh;

public static class Covariance_Ex
{
    public static void CovarianceExample()
    {
        // Пример ковариантности из стандартной библиотеки
        // Можем присвоить интерфейсу с более общим типом интерфейс с более специализированным
        IEnumerable<Cat> cats = new List<Cat>();
        IEnumerable<Animal> animals = cats;

        // Пример контрвариантности из стандартной библиотеки
        // Более специфическому параметру Cat/Dog делегата Action присваиваем метод с более общим параметром Animal
        Action<Cat> processCat = ProcessAnimal;
        processCat(new Cat());
        Action<Dog> processDog = ProcessAnimal;
        processDog(new Dog());

        // Пример ковариантности и ковариантрости
        // Ковариантность - можем для интерфейса более общего типа Beer присвоить более класс, типизированный более спепциализированным типом Ipa
        IBeerFactory<Ipa, Beer> ipaFactory = new IpaBeerFactory(); // в данном случае 2-му generic параметру Beer в типе IBeerFactory<_, Beer> можем присвоить более специализированный Ipa в IpaBeerFactory (IBeerFactory<Beer, Ipa>)
        var ipa = ipaFactory.Produce("ipa #1");
        // Пример контрвариантности 
        IBeerFactory<Ipa, Beer> beerFactory = new BeerFactory(); // - в данной случае 1-му generic параметру в IBeerFactory<Ipa, _> можем присвоить более общий SimpleBeer в BeerFactory (IBeerFactory<SimpleBeer, _>)
    }

    private static void ProcessAnimal(Animal a)
    {
        a.SayWhoAreYou();
    }
}

public abstract class Animal
{
    public string Name { get; set; }
    public abstract void SayWhoAreYou();
}

public class Cat : Animal
{
    public override void SayWhoAreYou()
    {
        Console.WriteLine("I'm a cat");
    }
}

public class BritainCat : Cat
{ }

public class Dog : Animal
{
    public override void SayWhoAreYou()
    {
        Console.WriteLine("I'm a dog");
    }
}

public abstract class Beer
{
    public string Name { get; set; }
}

public class SimpleBeer : Beer
{
    public SimpleBeer(string name)
    {
        Name = name;
    }
}

public class Ipa : SimpleBeer
{
    public Ipa(string name) : base(name)
    { }
}

public interface IBeerFactory<in T, out TBeer>
{
    public TBeer Produce(string name);

    public void SendToStore(T beer);
}

public class BeerFactory : IBeerFactory<SimpleBeer, SimpleBeer>
{
    public SimpleBeer Produce(string name) => new(name);

    public void SendToStore(SimpleBeer beer)
    {
        Console.WriteLine($"Sending sijmple beer '{beer.Name}'");
    }
}

public class IpaBeerFactory : IBeerFactory<SimpleBeer, Ipa>
{
    public Ipa Produce(string name) => new(name);
    public void SendToStore(SimpleBeer beer)
    {
        Console.WriteLine($"Sending ipa '{beer.Name}'");
    }
}

