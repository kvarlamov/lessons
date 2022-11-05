using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class PowerSetTests
    {
        private Stopwatch _stopWatch;
        
        [TearDown]
        public void Cleanup()
        {
            if (_stopWatch == null)
                return;
            
            _stopWatch.Stop();
            Console.WriteLine("Excution time for {0} - {1} ms",
                TestContext.CurrentContext.Test.Name,
                _stopWatch.ElapsedMilliseconds);
        }
        
        [Test]
        public void Put_EqualElements_SetWithoutDuplicates()
        {
            var set = new PowerSet<string>(1);
            set.Put("1");
            set.Put("1");
            
            Assert.That(set.Size(), Is.EqualTo(1));
            Assert.That(set._entries.First(), Is.EqualTo("1"));
            Assert.IsTrue(set.Get("1"));
            Assert.IsFalse(set.Get(string.Empty));
            Assert.IsFalse(set.Get("asdf"));
        }

        [Test]
        public void PutFew()
        {
            int length = 10;
            var set = new PowerSet<string>(length);
            var list = GetList(length);
            foreach (var str in list)
            {
                set.Put(str);
                set.Put(str);
            }
            
            Assert.That(set.Size(), Is.EqualTo(length));
            foreach (var s in list)
            {
                Assert.IsTrue(set.Get(s));
            }
            
            Assert.IsFalse(set.Get(string.Empty));
            Assert.IsFalse(set.Get("asdf"));
        }
        
        [Test]
        public void PutBig()
        {
            _stopWatch = Stopwatch.StartNew();
            int length = 20000;
            var set = new PowerSet<string>(length);
            var list = GetList(length);
            foreach (var str in list)
            {
                set.Put(str);
                set.Put(str);
            }
            
            Assert.That(set.Size(), Is.EqualTo(length));
            foreach (var s in list)
            {
                Assert.IsTrue(set.Get(s));
            }
            
            Assert.IsFalse(set.Get(string.Empty));
            Assert.IsFalse(set.Get("asdf"));
        }

        [Test]
        public void Put_OnlyDistinctBig()
        {
            int length = 20000;
            var set = new PowerSet<string>();
            foreach (var str in GetList(length))
            {
                set.Put(str);
                set.Put(str);
            }
            
            Assert.That(set.Size(), Is.EqualTo(length));
        }
        
        [Test]
        public void Remove_One()
        {
            int length = 10;
            var set = new PowerSet<string>(length);
            var strs = GetList(length);
            foreach (var str in strs)
            {
                set.Put(str);
                set.Put(str);
            }

            _stopWatch = Stopwatch.StartNew();
            Assert.IsTrue(set.Remove(strs[3]));
            Assert.IsFalse(set._entries.Contains(strs[3]));
            Assert.That(set.Size(), Is.EqualTo(length - 1));
        }

        [Test]
        public void Remove_All()
        {
            int length = 10;
            var set = new PowerSet<string>(length);
            var strs = GetList(length);
            foreach (var str in strs)
            {
                set.Put(str);
                set.Put(str);
            }

            _stopWatch = Stopwatch.StartNew();
            for (var i = 0; i < strs.Count; i++)
            {
                Assert.IsTrue(set.Remove(strs[i]));
                Assert.IsFalse(set.Remove(strs[i]));
            }

            //here set should be empty
            Assert.IsFalse(set.Remove(string.Empty));
            Assert.IsFalse(set.Remove(string.Empty));
            Assert.IsFalse(set.Remove(string.Empty));
            Assert.That(set.Size(), Is.EqualTo(0));
        }
        
        [Test]
        public void Remove_OneBig()
        {
            int length = 20000;
            var set = new PowerSet<string>(length);
            var strs = GetList(length);
            foreach (var str in strs)
            {
                set.Put(str);
                set.Put(str);
            }

            _stopWatch = Stopwatch.StartNew();
            Assert.That(set._entries.Distinct().ToArray().Length, Is.EqualTo(length));

            var rnd = new Random();

            int index = rnd.Next(0, length - 1);
            Assert.IsTrue(set.Remove(strs[index]));
            Assert.IsFalse(set.Remove(strs[index]));

            //here set should be empty
            Assert.That(set.Size(), Is.EqualTo(length - 1));
        }
        
        [Test]
        public void Remove_AllBig()
        {
            int length = 20000;
            var set = new PowerSet<string>(length);
            var strs = GetList(length);
            foreach (var str in strs)
            {
                set.Put(str);
                set.Put(str);
            }

            _stopWatch = Stopwatch.StartNew();
            Assert.That(set._entries.Distinct().ToArray().Length, Is.EqualTo(length));
            
            for (var i = 0; i < strs.Count; i++)
            {
                Assert.IsTrue(set.Remove(strs[i]));
                Assert.IsFalse(set.Remove(strs[i]));
            }

            //here set should be empty
            Assert.IsFalse(set.Remove(string.Empty));
            Assert.IsFalse(set.Remove(string.Empty));
            Assert.IsFalse(set.Remove(string.Empty));
            Assert.That(set.Size(), Is.EqualTo(0));
        }

        [Test]
        public void Intersect_Empty()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(5);
            
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");
            set2.Put("6");
            set2.Put("7");
            set2.Put("8");
            set2.Put("9");
            set2.Put("10");

            _stopWatch = Stopwatch.StartNew();
            var res = set1.Intersection(set2);
            
            Assert.That(res.Size(), Is.EqualTo(0));
            Assert.IsTrue(res.GetEntries().ToArray().All(i => string.IsNullOrEmpty(i)));
        }
        
        [Test]
        public void Intersect_Empty3()
        {
            var set1 = new PowerSet<string>();
            var set2 = new PowerSet<string>(5);
            
            set2.Put("6");
            set2.Put("7");
            set2.Put("8");
            set2.Put("9");
            set2.Put("10");

            _stopWatch = Stopwatch.StartNew();
            var res = set1.Intersection(set2);
            
            Assert.That(res.Size(), Is.EqualTo(0));
            Assert.IsTrue(res.GetEntries().ToArray().All(i => string.IsNullOrEmpty(i)));
        }
        
        [Test]
        public void Intersect_Empty2()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>();
            
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");

            _stopWatch = Stopwatch.StartNew();
            var res = set1.Intersection(set2);
            
            Assert.That(res.Size(), Is.EqualTo(0));
            Assert.IsTrue(res.GetEntries().ToArray().All(i => string.IsNullOrEmpty(i)));
        }
        
        [Test]
        public void Intersect_NotEmpty()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(5);
            
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");
            set2.Put("1");
            set2.Put("7");
            set2.Put("3");
            set2.Put("9");
            set2.Put("5");

            _stopWatch = Stopwatch.StartNew();
            var res = set1.Intersection(set2);
            
            Assert.That(res.Size(), Is.EqualTo(3));
            Assert.IsTrue(res.Get("1"));
            Assert.IsTrue(res.Get("3"));
            Assert.IsTrue(res.Get("5"));
        }
        
        [Test]
        public void Intersect_NotEmpty2()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(5);
            
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");
            set2.Put("1");
            set2.Put("2");
            set2.Put("3");
            set2.Put("4");
            set2.Put("5");

            _stopWatch = Stopwatch.StartNew();
            var res = set1.Intersection(set2);
            
            Assert.That(res.Size(), Is.EqualTo(5));
            Assert.IsTrue(res.Get("1"));
            Assert.IsTrue(res.Get("2"));
            Assert.IsTrue(res.Get("3"));
            Assert.IsTrue(res.Get("4"));
            Assert.IsTrue(res.Get("5"));
        }
        
        [Test]
        public void Intersect_NotEmpty_Big()
        {
            var set1 = new PowerSet<string>();
            var set2 = new PowerSet<string>();
            var length = 20000;
            var list = GetList(length);
            foreach (var i in list)
            {
                set1.Put(i);
                set2.Put(i);
            }
            
            _stopWatch = Stopwatch.StartNew();
            var res = set1.Intersection(set2);
            
            Assert.That(res.Size(), Is.EqualTo(length));
            foreach (var i in list)
            {
                Assert.IsTrue(res.Get(i));
            }
        }

        [Test]
        public void Union_OneEmpty1()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(5);
            
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");

            var res = set1.Union(set2);
            
            Assert.That(res.Size(), Is.EqualTo(5));
            Assert.IsTrue(res.Get("1"));
            Assert.IsTrue(res.Get("2"));
            Assert.IsTrue(res.Get("3"));
            Assert.IsTrue(res.Get("4"));
            Assert.IsTrue(res.Get("5"));
        }
        
        [Test]
        public void Union_OneEmpty2()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(5);
            
            set2.Put("1");
            set2.Put("2");
            set2.Put("3");
            set2.Put("4");
            set2.Put("5");
            
            var res = set1.Union(set2);
            
            Assert.That(res.Size(), Is.EqualTo(5));
            Assert.IsTrue(res.Get("1"));
            Assert.IsTrue(res.Get("2"));
            Assert.IsTrue(res.Get("3"));
            Assert.IsTrue(res.Get("4"));
            Assert.IsTrue(res.Get("5"));
        }
        
        [Test]
        public void Union_NotEmpty1()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(6);
            
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");
            set2.Put("1");
            set2.Put("2");
            set2.Put("3");
            set2.Put("4");
            set2.Put("5");
            set2.Put("6");
            
            var res = set1.Union(set2);
            
            Assert.That(res.Size(), Is.EqualTo(6));
            Assert.IsTrue(res.Get("1"));
            Assert.IsTrue(res.Get("2"));
            Assert.IsTrue(res.Get("3"));
            Assert.IsTrue(res.Get("4"));
            Assert.IsTrue(res.Get("5"));
            Assert.IsTrue(res.Get("6"));
        }
        
        [Test]
        public void Union_NotEmpty2()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(6);
            
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");
            set2.Put("6");
            set2.Put("7");
            set2.Put("8");
            set2.Put("9");
            set2.Put("10");
            set2.Put("11");
            
            var res = set1.Union(set2);
            
            Assert.That(res.Size(), Is.EqualTo(11));
            Assert.IsTrue(res.Get("1"));
            Assert.IsTrue(res.Get("2"));
            Assert.IsTrue(res.Get("3"));
            Assert.IsTrue(res.Get("4"));
            Assert.IsTrue(res.Get("5"));
            Assert.IsTrue(res.Get("6"));
        }
        
        [Test]
        public void Union_NotEmpty3()
        {
            var set1 = new PowerSet<string>(1);
            var set2 = new PowerSet<string>(2);
            
            set1.Put("1");
            set2.Put("1");
            set2.Put("2");
            
            var res = set1.Union(set2);
            
            Assert.That(res.Size(), Is.EqualTo(2));
            Assert.IsTrue(res.Get("1"));
            Assert.IsTrue(res.Get("2"));
        }

        [Test]
        public void UnionBig()
        {
            var set1 = new PowerSet<string>();
            var set2 = new PowerSet<string>();

            var length = 20000;
            var list1 = GetList(length);
            foreach (var i in list1)
            {
                set1.Put(i);
            }
            var list2 = GetList(length);
            foreach (var i in list2)
            {
                set2.Put(i);
            }

            var expected = list1.Union(list2).ToArray();

            _stopWatch = Stopwatch.StartNew();
            var res = set1.Union(set2);
            
            Assert.That(res.Size(), Is.EqualTo(expected.Length));
        }

        [Test]
        public void Difference_Empty1()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(5);
            
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");
            set2.Put("1");
            set2.Put("2");
            set2.Put("3");
            set2.Put("4");
            set2.Put("5");

            var res = set1.Difference(set2);
            
            Assert.That(res.Size(), Is.EqualTo(0));
            Assert.IsTrue(res.GetEntries().ToArray().All(x => string.IsNullOrEmpty(x)));
        }
        
        [Test]
        public void Difference_NotEmpty1()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(5);
            
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");
            set2.Put("1");
            set2.Put("2");
            set2.Put("5");

            var res = set1.Difference(set2);
            
            Assert.That(res.Size(), Is.EqualTo(2));
            Assert.IsTrue(res.Get("3"));
            Assert.IsTrue(res.Get("4"));
        }
        
        [Test]
        public void DifferenceBig()
        {
            var set1 = new PowerSet<string>();
            var set2 = new PowerSet<string>();

            var length = 20000;
            var list1 = GetList(length);
            foreach (var i in list1)
            {
                set1.Put(i);
            }
            var list2 = GetList(length);
            foreach (var i in list2)
            {
                set2.Put(i);
            }

            var expected = list1.Except(list2).ToArray();

            _stopWatch = Stopwatch.StartNew();
            var res = set1.Difference(set2);
            
            Assert.That(res.Size(), Is.EqualTo(expected.Length));
        }

        [Test, Description("все элементы параметра входят в текущее множество")]
        public void IsSubset1()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(5);
            
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");
            set2.Put("1");
            set2.Put("4");
            set2.Put("5");
            
            Assert.IsTrue(set1.IsSubset(set2));
        }
        
        [Test, Description("все элементы параметра входят в текущее множество")]
        public void IsSubset1_2()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(5);
            
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");
            set2.Put("1");
            set2.Put("2");
            set2.Put("3");
            set2.Put("4");
            set2.Put("5");
            
            Assert.IsTrue(set1.IsSubset(set2));
        }

        [Test]
        public void IsSubSet_Empty()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(5);
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");
            
            Assert.IsTrue(set1.IsSubset(set2));
        }
        
        [Test, Description("все элементы текущего множества входят в параметр")]
        public void IsSubset2()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(5);
            
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");
            set2.Put("1");
            set2.Put("2");
            set2.Put("3");
            set2.Put("4");
            set2.Put("5");
            set2.Put("6");
            set2.Put("7");
            
            Assert.IsFalse(set1.IsSubset(set2));
        }
        
        [Test, Description("не все элементы параметра входят в текущее множество")]
        public void IsSubset3()
        {
            var set1 = new PowerSet<string>(5);
            var set2 = new PowerSet<string>(5);
            
            set1.Put("1");
            set1.Put("2");
            set1.Put("3");
            set1.Put("4");
            set1.Put("5");
            set2.Put("2");
            set2.Put("3");
            set2.Put("4");
            set2.Put("6");
            
            Assert.IsFalse(set1.IsSubset(set2));
        }

        private List<string> GetList(int length)
        {
            var list = new HashSet<string>();
            Random rnd = new Random();
            int count = 0;

            while (count != length)
            {
                var str = rnd.Next(0, 1000000).ToString();
                if (list.Add(str))
                    count++;
            }

            return list.ToList();
        }
    }
}