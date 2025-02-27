namespace TheoryFunc1;

internal static class Lesson11
{
    public static void Example()
    {
        // присвоить ф-ию переменной
        Func<string, string> greet = (string name) =>
        {
            return $"hello, {name}";
        };
        
        //передать ф-ию в другую в качестве аргумента
        var callFunc = (Func<string, string> fn, string param) => fn(param);

        Console.WriteLine(callFunc(greet, "Bob"));
    }
}

internal static class Lesson12
{
    public static void Example()
    {
        var doubler = MakeMultiplier(2);
        var tripler = MakeMultiplier(3);

        Console.WriteLine(doubler(3));
        Console.WriteLine(tripler(3));

        static Func<int, int> MakeMultiplier(int factor)
        {
            return x => x * factor;
        }
    }
}

internal static class Lesson13
{
    public static void Example()
    {
        var squareAfterInc = Compose(outer: Square(), inner: Increment());
        Console.WriteLine(squareAfterInc(4));
        
        static Func<int, int> Compose(Func<int, int> outer, Func<int, int> inner)
        {
            return x => outer(inner(x));
        }

        static Func<int, int> Increment()
        {
            return x => x + 1;
        }

        static Func<int, int> Square()
        {
            return x => x * x;
        }
    }
}

internal static class Lesson14
{
    public static void Example()
    {
        var add5 = Add(5);
        Console.WriteLine(add5(4));
        
        static Func<int, int> Add(int x)
        {
            return y => x + y;
        }
    }
}