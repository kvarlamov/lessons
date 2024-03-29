﻿using System.Collections.Generic;
using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class BastShoeTests
    {
        
        [Test]
        public void First()
        {
            Queue<string> commands = new Queue<string>();
            commands.Enqueue("1 Привет");
            commands.Enqueue("1 , Мир!");
            commands.Enqueue("1 ++");
            commands.Enqueue("2 2");
            commands.Enqueue("4");
            commands.Enqueue("4");
            commands.Enqueue("1 *");
            commands.Enqueue("4");
            commands.Enqueue("4");
            commands.Enqueue("4");
            commands.Enqueue("3 6");
            commands.Enqueue("2 100");
            
            Queue<string> expected = new Queue<string>();
            expected.Enqueue("Привет");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет, Мир!++");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет, Мир!++");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет, Мир!*");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue(",");
            expected.Enqueue(string.Empty);

            while (commands.Count > 0)
            {
                var res = Level1Solved.BastShoe(commands.Dequeue());
                Assert.AreEqual(expected.Dequeue(), res);
            }
        }
        
        [Test]
        public void Second()
        {
            Queue<string> commands = new Queue<string>();
            commands.Enqueue("1 Привет");
            commands.Enqueue("1 , Мир!");
            commands.Enqueue("1 ++");
            commands.Enqueue("4");
            commands.Enqueue("4");
            commands.Enqueue("5");
            commands.Enqueue("4");
            commands.Enqueue("5");
            commands.Enqueue("5");
            commands.Enqueue("5");
            commands.Enqueue("5");
            commands.Enqueue("4");
            commands.Enqueue("4");
            commands.Enqueue("2 2");
            commands.Enqueue("4");
            commands.Enqueue("5");
            commands.Enqueue("5");
            commands.Enqueue("5");
            
            Queue<string> expected = new Queue<string>();
            expected.Enqueue("Привет");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет, Мир!++");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет, Мир!++");
            expected.Enqueue("Привет, Мир!++");
            expected.Enqueue("Привет, Мир!++");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет");
            expected.Enqueue("Прив");
            expected.Enqueue("Привет");
            expected.Enqueue("Прив");
            expected.Enqueue("Прив");
            expected.Enqueue("Прив");

            while (commands.Count > 0)
            {
                var res = Level1Solved.BastShoe(commands.Dequeue());
                Assert.AreEqual(expected.Dequeue(), res);
            }
        }

        [Test]
        public void Four()
        {
            Queue<string> commands = new Queue<string>();
            commands.Enqueue("1 Привет");
            commands.Enqueue("1 , Мир!");
            commands.Enqueue("1 ++");
            commands.Enqueue("4");
            commands.Enqueue("4");
            commands.Enqueue("4");
            commands.Enqueue("4");
            commands.Enqueue("4");
            commands.Enqueue("4");
            commands.Enqueue("4");
            commands.Enqueue("4");
            
            Queue<string> expected = new Queue<string>();
            expected.Enqueue("Привет");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет, Мир!++");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет");
            expected.Enqueue("");
            expected.Enqueue("");
            expected.Enqueue("");
            expected.Enqueue("");
            expected.Enqueue("");
            expected.Enqueue("");
            expected.Enqueue("");
            expected.Enqueue("");

            while (commands.Count > 0)
            {
                var res = Level1Solved.BastShoe(commands.Dequeue());
                Assert.AreEqual(expected.Dequeue(), res);
            }
        }

        [Test]
        public void TestNew()
        {
            Queue<string> commands = new Queue<string>();
            commands.Enqueue("1 Привет");
            commands.Enqueue("1 , Мир!");
            commands.Enqueue("1 ++");
            commands.Enqueue("3 1");
            commands.Enqueue("4");
            
            Queue<string> expected = new Queue<string>();
            expected.Enqueue("Привет");
            expected.Enqueue("Привет, Мир!");
            expected.Enqueue("Привет, Мир!++");
            expected.Enqueue("р");
            expected.Enqueue("Привет, Мир!");
            
            while (commands.Count > 0)
            {
                var res = Level1Solved.BastShoe(commands.Dequeue());
                Assert.AreEqual(expected.Dequeue(), res);
            }
        }

        [Test]
        public void TestStrange()
        {
            List<KeyValuePair<string, string>> dic = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>( "4", ""),
                new KeyValuePair<string, string>( "2 2", ""),
                new KeyValuePair<string, string>("5", ""),
                new KeyValuePair<string, string>("1 Привет", "Привет"),
                new KeyValuePair<string, string>("1 ++", "Привет++"),
                new KeyValuePair<string, string>("4", "Привет"),
                new KeyValuePair<string, string>("4", ""),
                new KeyValuePair<string, string>("4", ""),
                new KeyValuePair<string, string>("4", ""),
                new KeyValuePair<string, string>("4", ""),
                new KeyValuePair<string, string>("5", ""),
                new KeyValuePair<string, string>("5", "Привет"),
                new KeyValuePair<string, string>("5", "Привет++"),
                new KeyValuePair<string, string>( "2 2", "Привет")
            };

            foreach (var i in dic)
            {
                var res = Level1Solved.BastShoe(i.Key);
                Assert.AreEqual(i.Value, res);
            }
        }
    }
}