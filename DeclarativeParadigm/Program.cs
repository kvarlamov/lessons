using System;

namespace DeclarativeParadigm
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = FactorialControlAbstractionImpl.Calculate();
            Console.WriteLine(string.Join(",", result));
            
            GetFunc.UsageExample();
        }
    }
}