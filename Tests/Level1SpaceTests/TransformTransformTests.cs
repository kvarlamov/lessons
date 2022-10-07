using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class TransformTransformTests
    {
        [Test]
        public void Test1()
        {
            int[] arr = {1,2,1,7,2,4,3,1,5,1,2,1,6,1,2};

            var result = Level1Solved.TransformTransform(arr, arr.Length);
            
            Assert.IsFalse(result);
        }
    }
}