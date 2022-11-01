using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class HashTableTests
    {
        const int Size = 19;
        private const int Stp = 3;
        
        [Test, TestCaseSource(nameof(TestNumbers))]
        public void HashFunTest2(string val)
        {
            HashTable table = new HashTable(Size, Stp);
            Assert.That(table.HashFun(val), Is.LessThanOrEqualTo(Size - 1));
        }

        [Test, TestCaseSource(nameof(TestNumbersShort))]
        public void SeekSlot(string val)
        {
            HashTable table = new HashTable(Size, Stp);

            Assert.That(table.SeekSlot(val), Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void PutTest()
        {
            HashTable table = new HashTable(Size, Stp);
            var strs = TestNumbersShort();
            var listNotPutted = new List<string>();

            for (int i = 0; i < strs.Length; i++)
            {
                if (table.Put(strs[i])== -1)
                    listNotPutted.Add(strs[i]);
            }

            var res = table.GetAll();
            Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x)));

            //check that not putted not exist in final hashset
            var check = listNotPutted.Except(res).ToArray();
            Assert.IsTrue(check.Length == listNotPutted.Count);
        }
        
        [Test]
        public void PutShortTest()
        {
            for (int j = 0; j < 500; j++)
            {
                HashTable table = new HashTable(Size, Stp);
                var strs = TestNumbersSuperShort();
                var listNotPutted = new List<string>();

                for (int i = 0; i < strs.Length; i++)
                {
                    if (table.Put(strs[i])== -1)
                        listNotPutted.Add(strs[i]);
                }

                var res = table.GetAll();
                Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x)));

                //check that not putted not exist in final hashset
                var check = listNotPutted.Except(res).ToArray();
                Assert.IsTrue(check.Length == listNotPutted.Count);
            }
        }

        [Test]
        public void FindTests()
        {
            HashTable table = new HashTable(Size, Stp);
            var strs = TestNumbersShort();
            var listNotPutted = new List<string>();

            for (int i = 0; i < strs.Length; i++)
            {
                if (table.Put(strs[i])== -1)
                    listNotPutted.Add(strs[i]);
            }

            var res = table.GetAll();
            Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x)));

            foreach (var s in strs.Except(listNotPutted).ToArray())
            {
                Assert.That(table.Find(s), Is.GreaterThanOrEqualTo(0));
            }
            
            foreach (var s in listNotPutted)
            {
                Assert.That(table.Find(s), Is.EqualTo(-1));
            }
        }
        
        static string[] TestNumbers()
        {
            int size = 500;
            Random rnd = new Random();
            var arr = new string[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = rnd.Next(0, 2000000).ToString();
            }

            return arr;
        }
        
        static string[] TestNumbersShort()
        {
            int size = 100;
            Random rnd = new Random();
            var arr = new string[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = rnd.Next(0, 2000000).ToString();
            }

            return arr;
        }
        
        static string[] TestNumbersSuperShort()
        {
            int size = 20;
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