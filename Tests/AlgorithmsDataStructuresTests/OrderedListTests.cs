using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AlgorithmsDataStructures;
using NUnit.Framework;

namespace AlgorithmsDataStructuresTests
{
    public class OrderedListTests
    {
        #region Add_Asc

        [Test]
        public void StringAdd_Test1()
        {
            var list = new OrderedList<string>(true);
            list.Add("124");
            list.Add("123");
            list.Add("3");
            list.Add("099999");

            var expected = new List<string>()
            {
                "099999", "123", "124", "3"
            };
            var result = list.GetAll();
            
            CollectionAssert.AreEqual(expected, result.Select(x => x.value).ToList());
            Assert.That(list.head.value, Is.EqualTo("099999"));
            Assert.That(list.tail.value, Is.EqualTo("3"));
        }
        
        [Test]
        public void Add_Head_Empty()
        {
            var list = new OrderedList<int>(true);
            
            list.Add(1);
            
            Assert.That(list.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Add_Head()
        {
            var list = new OrderedList<int>(true);
            list.Add(2);
            list.Add(1);
            list.Add(3);

            var listFromOrdered = list.GetAll();
            Assert.That(listFromOrdered[0].value, Is.EqualTo(1));
            Assert.That(list.Count(), Is.EqualTo(3));
            Assert.That(listFromOrdered[2].value, Is.EqualTo(3));
            Assert.That(list.head.value, Is.EqualTo(1));
            Assert.That(list.tail.value, Is.EqualTo(3));
        }

        [Test]
        public void Add_Middle_Test()
        {
            var list = new OrderedList<int>(true);
            list.Add(3);
            list.Add(2);
            list.Add(1);
            list.Add(4);
            list.Add(6);
            list.Add(5);

            var result = list.GetAll();
            var values = result.Select(x => x.value).ToList();

            var expected = new List<int>()
            {
                1, 2, 3, 4, 5, 6
            };
            
            CollectionAssert.AreEqual(expected, values);
            Assert.That(list.head.value, Is.EqualTo(1));
            Assert.That(list.Count(), Is.EqualTo(6));
            Assert.That(list.tail.value, Is.EqualTo(6));
            Assert.That(result.Last().value, Is.EqualTo(6));
            Assert.That(result.First().value, Is.EqualTo(1));
        }

        [Test]
        public void Add_Tail()
        {
            var list = new OrderedList<int>(true);
            list.Add(2);
            list.Add(5);

            var result = list.GetAll();
            Assert.That(result[0].value, Is.EqualTo(2));
            Assert.That(result[1].value, Is.EqualTo(5));
            Assert.That(list.tail.value, Is.EqualTo(5));
            Assert.That(list.head.value, Is.EqualTo(2));
        }

        [Test]
        public void Add_Middle_Equal1()
        {
            // 1 1 2 3 4, insert 1
            var list = new OrderedList<int>(true);
            list.Add(2);
            list.Add(3);
            list.Add(1);
            list.Add(1);
            list.Add(1);

            var result = list.GetAll();
            var values = result.Select(x => x.value).ToList();

            var expected = new List<int>()
            {
                1, 1, 1, 2, 3
            };
            
            CollectionAssert.AreEqual(expected, values);
            Assert.That(list.head.value, Is.EqualTo(1));
            Assert.That(list.Count(), Is.EqualTo(5));
            Assert.That(list.tail.value, Is.EqualTo(3));
            Assert.That(result.Last().value, Is.EqualTo(3));
            Assert.That(result.First().value, Is.EqualTo(1));
        }

        #endregion

        #region Add_Desc

        [Test]
        public void AddDesc_Head_Empty()
        {
            var list = new OrderedList<int>(false);
            
            list.Add(1);
            
            Assert.That(list.Count(), Is.EqualTo(1));
        }

        [Test]
        public void AddDesc_Head()
        {
            var list = new OrderedList<int>(false);
            list.Add(2);
            list.Add(1);
            list.Add(3);

            var listFromOrdered = list.GetAll();
            Assert.That(listFromOrdered[0].value, Is.EqualTo(3));
            Assert.That(list.Count(), Is.EqualTo(3));
            Assert.That(listFromOrdered[2].value, Is.EqualTo(1));
            Assert.That(list.head.value, Is.EqualTo(3));
            Assert.That(list.tail.value, Is.EqualTo(1));
        }

        [Test]
        public void AddDesc_Middle_Test()
        {
            var list = new OrderedList<int>(false);
            list.Add(3);
            list.Add(2);
            list.Add(1);
            list.Add(4);
            list.Add(6);
            list.Add(5);

            var result = list.GetAll();
            var values = result.Select(x => x.value).ToList();

            var expected = new List<int>()
            {
                6,5,4,3,2,1
            };
            
            CollectionAssert.AreEqual(expected, values);
            Assert.That(list.head.value, Is.EqualTo(6));
            Assert.That(list.Count(), Is.EqualTo(6));
            Assert.That(list.tail.value, Is.EqualTo(1));
            Assert.That(result.Last().value, Is.EqualTo(1));
            Assert.That(result.First().value, Is.EqualTo(6));
        }

        [Test]
        public void AddDesc_Tail()
        {
            var list = new OrderedList<int>(false);
            list.Add(2);
            list.Add(5);

            var result = list.GetAll();
            Assert.That(result[0].value, Is.EqualTo(5));
            Assert.That(result[1].value, Is.EqualTo(2));
            Assert.That(list.tail.value, Is.EqualTo(2));
            Assert.That(list.head.value, Is.EqualTo(5));
        }
        
        [Test]
        public void Add_Desc_Middle_Equal1()
        {
            var list = new OrderedList<int>(false);
            list.Add(2);
            list.Add(3);
            list.Add(1);
            list.Add(1);
            list.Add(1);

            var result = list.GetAll();
            var values = result.Select(x => x.value).ToList();

            var expected = new List<int>()
            {
                3,2, 1, 1, 1
            };
            
            CollectionAssert.AreEqual(expected, values);
            Assert.That(list.head.value, Is.EqualTo(3));
            Assert.That(list.Count(), Is.EqualTo(5));
            Assert.That(list.tail.value, Is.EqualTo(1));
            Assert.That(result.Last().value, Is.EqualTo(1));
            Assert.That(result.First().value, Is.EqualTo(3));
        }

        #endregion
        
        #region Delete

        [Test]
        public void Delete_EmptyList()
        {
            var list = new OrderedList<int>(true);

            list.Delete(2);
            var result = list.GetAll();
            
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(null, list.head);
            Assert.AreEqual(null, list.tail);
        }
        
        [Test]
        public void Delete_OneElementList_Deleted()
        {
            var list = new OrderedList<int>(false);
            list.Add(2);

            list.Delete(2);
            var result = list.GetAll();
            
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(null, list.head);
            Assert.AreEqual(null, list.tail);
        }
        
        [Test]
        public void Delete_TwoElementListRemoveFirst_Deleted()
        {
            var list = new OrderedList<int>(true);
            list.Add(3);
            list.Add(2);

            list.Delete(2);
            var result = list.GetAll();
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.head.next);
            Assert.AreEqual(null, list.tail.next);
            Assert.IsNull(list.head.prev);
            Assert.IsNull(list.tail.prev);
        }
        
        [Test]
        public void DeleteDesc_TwoElementListRemoveFirst_Deleted()
        {
            var list = new OrderedList<int>(false);
            list.Add(2);
            list.Add(3);

            list.Delete(2);
            var result = list.GetAll();
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.head.next);
            Assert.AreEqual(null, list.tail.next);
            Assert.IsNull(list.head.prev);
            Assert.IsNull(list.tail.prev);
        }

        [Test]
        public void Delete_ValueInHead_Deleted()
        {
            var list = new OrderedList<int>(true);
            list.Add(2);
            list.Add(3);
            list.Add(2);

            list.Delete(2);
            var result = list.GetAll();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.IsNull(list.head.prev);
        }
        
        [Test]
        public void DeleteDesc_ValueInHead_Deleted()
        {
            var list = new OrderedList<int>(false);
            list.Add(2);
            list.Add(3);
            list.Add(2);

            list.Delete(2);
            var result = list.GetAll();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(3, list.head.value);
            Assert.AreEqual(2, list.tail.value);
            Assert.IsNull(list.head.prev);
        }
        
        [Test]
        public void Delete_ValueInTail_Deleted()
        {
            var list = new OrderedList<int>(true);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.Delete(4);
            var result = list.GetAll();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(3, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
        }
        
        [Test]
        public void DeleteDesc_ValueInTail_Deleted()
        {
            var list = new OrderedList<int>(false);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.Delete(4);
            var result = list.GetAll();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(3, list.head.value);
            Assert.AreEqual(2, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
        }
        
        [Test]
        public void Delete_Middle_Deleted()
        {
            var list = new OrderedList<int>(true);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.Delete(3);
            var result = list.GetAll();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, list.head.value);
            Assert.AreEqual(4, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
            Assert.AreEqual(2, list.tail.prev.value);
        }
        
        [Test]
        public void DeleteDesc_Middle_Deleted()
        {
            var list = new OrderedList<int>(false);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.Delete(3);
            var result = list.GetAll();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(4, list.head.value);
            Assert.AreEqual(2, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
            Assert.AreEqual(4, list.tail.prev.value);
        }
        
        [Test]
        public void Delete_Middle2_Deleted()
        {
            var list = new OrderedList<int>(true);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(2);

            list.Delete(4);
            var result = list.GetAll();

            var expected = new List<int>
            {
                2, 3, 5
            };
            
            CollectionAssert.AreEqual(expected, result.Select(x => x.value).ToList());
            Assert.AreEqual(5, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
            Assert.AreEqual(3, list.tail.prev.value);
            Assert.That(list.head.value, Is.EqualTo(2));
        }
        
        [Test]
        public void DeleteDesc_Middle2_Deleted()
        {
            var list = new OrderedList<int>(false);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(2);

            list.Delete(4);
            var result = list.GetAll();

            var expected = new List<int>
            {
                5,3,2
            };
            
            CollectionAssert.AreEqual(expected, result.Select(x => x.value).ToList());
            Assert.AreEqual(2, list.tail.value);
            Assert.AreEqual(null, list.tail.next);
            Assert.AreEqual(3, list.tail.prev.value);
            Assert.That(list.head.value, Is.EqualTo(5));
        }

        #endregion

        #region Find

        [Test]
        public void Find_Empty()
        {
            var list = new OrderedList<int>(true);

            var res = list.Find(5);
            
            Assert.IsNull(res);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void FindSingle(bool asc)
        {
            var list = new OrderedList<int>(asc);
            list.Add(1);

            var res = list.Find(1);
            Assert.That(res.value, Is.EqualTo(1));
            
            Assert.IsNull(list.Find(5));
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Find(bool asc)
        {
            var list = new OrderedList<int>(asc);
            list.Add(5);
            list.Add(2);
            list.Add(3);
            list.Add(1);
            list.Add(4);

            var res = list.Find(2);
            
            Assert.That(res.value, Is.EqualTo(2));
            Assert.IsNull(list.Find(6));
        }
        
        [Test]
        [TestCase(true, 5, 5)]
        [TestCase(false, 5, 5)]
        [TestCase(true, 2, 2)]
        [TestCase(true, 3, 3)]
        [TestCase(true, 7, 7)]
        [TestCase(true, 9, 9)]
        [TestCase(true, 11, 11)]
        [TestCase(true, 12, 12)]
        [TestCase(true, 15, 15)]
        [TestCase(true, 1, 1)]
        [TestCase(false, 2, 2)]
        [TestCase(false, 3, 3)]
        [TestCase(false, 7, 7)]
        [TestCase(false, 9, 9)]
        [TestCase(false, 11, 11)]
        [TestCase(false, 12, 12)]
        [TestCase(false, 15, 15)]
        [TestCase(false, 1, 1)]
        [TestCase(false, -1, null)]
        [TestCase(true, -1, null)]
        [TestCase(true, 20, null)]
        [TestCase(false, 20, null)]
        [TestCase(false, 8, null)]
        [TestCase(true, 8, null)]
        public void Find2(bool asc, int val, int? expected)
        {
            var list = new OrderedList<int>(asc);
            list.Add(5);
            list.Add(2);
            list.Add(3);
            list.Add(1);
            list.Add(4);
            list.Add(15);
            list.Add(7);
            list.Add(14);
            list.Add(9);
            list.Add(12);
            list.Add(13);
            list.Add(10);
            list.Add(11);

            var res = list.Find(val);

            if (res == null)
            {
                Assert.IsNull(expected);
            }
            else
            {
                Assert.That(res.value, Is.EqualTo(expected.Value));
            }
        }
        
        #endregion
    }
}