using System;
using System.Collections.Generic;
using System.Linq;

namespace DeclarativeParadigm
{
    public interface IControlAbstraction<T>
    {
        T Iterate(T s, Func<T, bool> isDone, Func<T, T> transform);
    }

    public class ControlAbstraction<T> : IControlAbstraction<T>
    {
        public T Iterate(T s, Func<T, bool> isDone, Func<T, T> transform)
        {
            if (isDone(s))
                return s;

            s = transform(s);
            
            return Iterate(s, isDone, transform);
        }
    }

    public static class FactorialControlAbstractionImpl
    {
        public static int[] Calculate()
        {
            return new ControlAbstraction<int[]>().Iterate(new[] {1,5}, IsDone, Transform);
        }
        
        private static bool IsDone(int[] array) => array[^1] == 1;

        private static int[] Transform(int[] a)
        {
            var newArray = new[] {a[0] * a[1], a[1] - 1};

            return newArray;
        }
    }

    public static class Fact
    {
        private static int Factor(int n, int a)
        {
            if (n == 0)
                return a;
            
            return Factor(n - 1, a * n);
        }

        public static int IterateFactor(int n)
        {
            if (n < 0)
                throw new ArgumentException("cant be less than zero");
            
            return Factor(n, 1);
        }
    }

    public static class ListLen
    {
        public static int GetLength(this List<int> list) => IterLen(0, list);

        private static int IterLen(int i, List<int> list)
        {
            if (list.Count == 0)
                return i;

            return IterLen(i + 1, list.GetRange(1, list.Count - 1));
        }
    }

    public static class CycleAbstractions
    {
        public static void UsageExample()
        {
            // Generic func that assept different func and firrerent edge case
            //for plus
            int[] arr = new[] {1, 2, 3, 4, 5};
            var sumRes = FoldR(arr, Sum, 0);
            var multyRes = FoldR(arr, Multy, 1);
            var accForAll = ForAllAcc(arr, Sum, 0);
            Console.WriteLine(sumRes);
            Console.WriteLine(multyRes);
        }

        public static int ForAllAcc(int[] Ls, Func<int, int, int> F, int Acc)
        {
            if (Ls.Any() is false)
                return Acc;

            Acc = F(Ls[0], Acc);
            var tail = Ls[1..];

            return ForAllAcc(tail, F, Acc);
        }
        
        public static int FoldR(int[] list, Func<int, int, int> f, int res)
        {
            if (!list.Any())
                return res;

            var head = list[0];
            var tail = list[1..];

            return f(head, FoldR(tail, f, res));
        }

        private static int Sum(int x, int y)
        {
            return x + y;
        }

        private static int Multy(int x, int y)
        {
            return x * y;
        }
    }
}