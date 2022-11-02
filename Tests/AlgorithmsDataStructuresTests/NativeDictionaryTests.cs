using System;
using System.Linq;
using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class NativeDictionaryTests
    {
        [Test]
        [TestCase(19)]
        [TestCase(20)]
        [TestCase(50)]
        public void AllMethodTest(int size)
        {
            var dictionary = new NativeDictionary<int>(size);

            var arr = TestNumbersSuperShort(size);
            
            for (var i = 0; i < arr.Length; i++)
            {
                dictionary.Put(arr[i], int.Parse(arr[i]));
            }

            foreach (var n in arr)
            {
                Assert.IsTrue(dictionary.IsKey(n));
                Assert.That(dictionary.Get(n), Is.EqualTo(int.Parse(n)));
            }

            
            var keys = dictionary.GetAllKeys();
            var values = dictionary.GetAllValues();
            
            Assert.IsTrue(keys.All(x=> !string.IsNullOrEmpty(x)));
            Assert.IsTrue(values.All(x=> x != 0));
        }
        
        [Test]
        [TestCase(19)]
        [TestCase(20)]
        [TestCase(50)]
        public void AllMethodTestString(int size)
        {
            var dictionary = new NativeDictionary<string>(size);

            var arr = TestNumbersSuperShort(size);
            
            for (var i = 0; i < arr.Length; i++)
            {
                dictionary.Put(arr[i], arr[i]);
            }

            foreach (var n in arr)
            {
                Assert.IsTrue(dictionary.IsKey(n));
                Assert.That(dictionary.Get(n), Is.EqualTo(n));
            }

            
            var keys = dictionary.GetAllKeys();
            var values = dictionary.GetAllValues();
            
            Assert.IsTrue(keys.All(x=> !string.IsNullOrEmpty(x)));
            Assert.IsTrue(values.All(x=> !string.IsNullOrEmpty(x)));
        }
            
        static string[] TestNumbersSuperShort(int size)
        {
            Random rnd = new Random();
            var arr = new string[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = rnd.Next(0, 2000000).ToString();
            }

            return arr;
        }
    }
}