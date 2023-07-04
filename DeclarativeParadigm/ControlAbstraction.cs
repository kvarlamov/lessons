using System;

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
}