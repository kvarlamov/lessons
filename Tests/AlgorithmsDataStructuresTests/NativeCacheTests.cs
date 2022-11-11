using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class NativeCacheTests
    {
        [Test]
        public void GetOrSet_New_EmptySlotExists_OK()
        {
            var cache = new NativeCache<string>(5);
            cache.AddOrGetExisting("1", "one");
            cache.AddOrGetExisting("2", "two");
            cache.AddOrGetExisting("3", "three");
            cache.AddOrGetExisting("4", "four");
            cache.AddOrGetExisting("5", "five");

            var keys = new List<string>
            {
                "1", "2", "3", "4", "5"
            };
            
            var values = new List<string>
            {
                "one", "two", "three", "four", "five"
            };

            foreach (var key in keys)
            {
                Assert.IsNotNull(cache.slots.FirstOrDefault(x => x.Equals(key)));
            }
            
            foreach (var val in values)
            {
                Assert.IsNotNull(cache.values.FirstOrDefault(x => x.Equals(val)));
            }

            foreach (var hit in cache.hits)
            {
                Assert.That(hit, Is.EqualTo(1));
            }
        }
        
        [Test]
        public void GetOrSet_New_EmptySlotNotExists_ChangeMinimum()
        {
            var cache = new NativeCache<string>(5);
            cache.AddOrGetExisting("1", "one");
            cache.AddOrGetExisting("2", "two");
            cache.AddOrGetExisting("3", "three");
            cache.AddOrGetExisting("4", "four");
            cache.AddOrGetExisting("5", "five");
            cache.AddOrGetExisting("1", "one");
            cache.AddOrGetExisting("2", "two");
            cache.AddOrGetExisting("3", "three");
            cache.AddOrGetExisting("4", "four");
            var res = cache.AddOrGetExisting("6", "six");

            var keys = new List<string>
            {
                "1", "2", "3", "4", "6"
            };
            
            var values = new List<string>
            {
                "one", "two", "three", "four", "six"
            };

            foreach (var key in keys)
            {
                Assert.That(cache.slots.FirstOrDefault(x => x.Equals(key)), Is.EqualTo(key));
            }
            
            foreach (var val in values)
            {
                Assert.IsNotNull(cache.values.FirstOrDefault(x => x.Equals(val)));
            }
        }
        
        [Test]
        public void GetOrSet_New_HadAKey_ReturnValyue()
        {
            var cache = new NativeCache<string>(5);
            var res = cache.AddOrGetExisting("1", "one");

            Assert.That(res, Is.EqualTo("one"));
        }

        [Test]
        public void Hits()
        {
            var cache = new NativeCache<string>(5);
            cache.AddOrGetExisting("1", "one");
            cache.AddOrGetExisting("2", "two");
            cache.AddOrGetExisting("2", "two");
            cache.AddOrGetExisting("3", "three");
            cache.AddOrGetExisting("3", "three");
            cache.AddOrGetExisting("3", "three");
            cache.AddOrGetExisting("4", "four");
            cache.AddOrGetExisting("4", "four");
            cache.AddOrGetExisting("4", "four");
            cache.AddOrGetExisting("4", "four");
            cache.AddOrGetExisting("5", "five");
            cache.AddOrGetExisting("5", "five");
            cache.AddOrGetExisting("5", "five");
            cache.AddOrGetExisting("5", "five");
            cache.AddOrGetExisting("5", "five");

            var indx = Array.IndexOf(cache.hits, 1);
            Assert.That(cache.slots[indx], Is.EqualTo("1"));
            Assert.That(cache.values[indx], Is.EqualTo("one"));
            
            indx = Array.IndexOf(cache.hits, 2);
            Assert.That(cache.slots[indx], Is.EqualTo("2"));
            Assert.That(cache.values[indx], Is.EqualTo("two"));
            
            indx = Array.IndexOf(cache.hits, 3);
            Assert.That(cache.slots[indx], Is.EqualTo("3"));
            Assert.That(cache.values[indx], Is.EqualTo("three"));
            
            indx = Array.IndexOf(cache.hits, 4);
            Assert.That(cache.slots[indx], Is.EqualTo("4"));
            Assert.That(cache.values[indx], Is.EqualTo("four"));
            
            indx = Array.IndexOf(cache.hits, 5);
            Assert.That(cache.slots[indx], Is.EqualTo("5"));
            Assert.That(cache.values[indx], Is.EqualTo("five"));

            cache.AddOrGetExisting("6", "six");
            cache.AddOrGetExisting("6", "six");
            cache.AddOrGetExisting("6", "six");
            cache.AddOrGetExisting("6", "six");
            cache.AddOrGetExisting("6", "six");
            cache.AddOrGetExisting("6", "six");
            
            cache.AddOrGetExisting("7", "seven");
            cache.AddOrGetExisting("7", "seven");
            cache.AddOrGetExisting("7", "seven");
            cache.AddOrGetExisting("7", "seven");
            cache.AddOrGetExisting("7", "seven");
            cache.AddOrGetExisting("7", "seven");
            cache.AddOrGetExisting("7", "seven");
            

            cache.AddOrGetExisting("8", "eight");
            cache.AddOrGetExisting("8", "eight");
            cache.AddOrGetExisting("8", "eight");
            cache.AddOrGetExisting("8", "eight");
            cache.AddOrGetExisting("8", "eight");
            cache.AddOrGetExisting("8", "eight");
            cache.AddOrGetExisting("8", "eight");
            cache.AddOrGetExisting("8", "eight");
            
            cache.AddOrGetExisting("9", "nine");
            cache.AddOrGetExisting("9", "nine");
            cache.AddOrGetExisting("9", "nine");
            cache.AddOrGetExisting("9", "nine");
            cache.AddOrGetExisting("9", "nine");
            cache.AddOrGetExisting("9", "nine");
            cache.AddOrGetExisting("9", "nine");
            cache.AddOrGetExisting("9", "nine");
            cache.AddOrGetExisting("9", "nine");
            
            cache.AddOrGetExisting("10", "ten");
            cache.AddOrGetExisting("10", "ten");
            cache.AddOrGetExisting("10", "ten");
            cache.AddOrGetExisting("10", "ten");
            cache.AddOrGetExisting("10", "ten");
            cache.AddOrGetExisting("10", "ten");
            cache.AddOrGetExisting("10", "ten");
            cache.AddOrGetExisting("10", "ten");
            cache.AddOrGetExisting("10", "ten");
            cache.AddOrGetExisting("10", "ten");
            
            indx = Array.IndexOf(cache.hits, 6);
            Assert.That(cache.slots[indx], Is.EqualTo("6"));
            Assert.That(cache.values[indx], Is.EqualTo("six"));
            
            indx = Array.IndexOf(cache.hits, 7);
            Assert.That(cache.slots[indx], Is.EqualTo("7"));
            Assert.That(cache.values[indx], Is.EqualTo("seven"));
            
            indx = Array.IndexOf(cache.hits, 8);
            Assert.That(cache.slots[indx], Is.EqualTo("8"));
            Assert.That(cache.values[indx], Is.EqualTo("eight"));
            
            indx = Array.IndexOf(cache.hits, 9);
            Assert.That(cache.slots[indx], Is.EqualTo("9"));
            Assert.That(cache.values[indx], Is.EqualTo("nine"));
            
            indx = Array.IndexOf(cache.hits, 10);
            Assert.That(cache.slots[indx], Is.EqualTo("10"));
            Assert.That(cache.values[indx], Is.EqualTo("ten"));
        }
    }
}