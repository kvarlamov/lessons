using System;
using Level1Space;
using Recursion;

namespace Level1SolvedSpace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            foreach (var file in FileFinder.Get(@"C:\1"))
            {
                Console.WriteLine(file.Name);
            }
            Console.ReadKey();
        }
    }
}