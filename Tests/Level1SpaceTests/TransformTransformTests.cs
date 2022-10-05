using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class TransformTransformTests
    {
        [Test]
        public void Test1()
        {
            int[] arr = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            var result = Level1.TransformTransform(arr, arr.Length);
        }
    }
}