using System;
using System.Linq;
using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class NativeDictionaryTests
    {
        [Test]
        public void GetNotExists()
        {
            var dictionary = new NativeDictionary<string>(2);
            dictionary.Put("one","1");
            dictionary.Put("two","2");
            
            Assert.IsNull(dictionary.Get("three"));
        }

        [Test]
        public void IsKeyNotExist()
        {
            var dictionary = new NativeDictionary<string>(2);
            dictionary.Put("one","1");
            dictionary.Put("two","2");
            
            Assert.IsFalse(dictionary.IsKey("three"));
        }
        
        
        [Test]
        public void PutNew()
        {
            var dictionary = new NativeDictionary<int>(4);
            dictionary.Put("one",1);
            dictionary.Put("two",2);
            dictionary.Put("three",3);
            dictionary.Put("four",4);
            
            Assert.That(dictionary.Get("one"), Is.EqualTo(1));
            Assert.That(dictionary.Get("two"), Is.EqualTo(2));
            Assert.That(dictionary.Get("three"), Is.EqualTo(3));
            Assert.That(dictionary.Get("four"), Is.EqualTo(4));
            Assert.IsTrue(dictionary.slots.All(x=> !string.IsNullOrEmpty(x)));
            Assert.IsTrue(dictionary.values.All(x=> x != 0));
        }

        [Test]
        public void PutIfExist()
        {
            var dictionary = new NativeDictionary<int>(4);
            dictionary.Put("one",1);
            dictionary.Put("two",2);
            dictionary.Put("three",3);
            dictionary.Put("four",4);
            
            dictionary.Put("one", 11);
            dictionary.Put("two", 22);
            dictionary.Put("three", 33);
            dictionary.Put("four", 44);
            
            Assert.That(dictionary.Get("one"), Is.EqualTo(11));
            Assert.That(dictionary.Get("two"), Is.EqualTo(22));
            Assert.That(dictionary.Get("three"), Is.EqualTo(33));
            Assert.That(dictionary.Get("four"), Is.EqualTo(44));
            Assert.IsTrue(dictionary.slots.All(x=> !string.IsNullOrEmpty(x)));
            Assert.IsTrue(dictionary.values.All(x=> x != 0));
            
            Assert.Throws<IndexOutOfRangeException>(() => dictionary.Put("test", 00));
        }
        
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

            
            var keys = dictionary.slots;
            var values = dictionary.values;
            
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

            
            var keys = dictionary.slots;
            var values = dictionary.values;
            
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