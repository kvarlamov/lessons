using System.Globalization;
using System.Threading;
using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class PatternUnlockTests
    {
        [Test]
        public void PatternUnlock_changedCulture_141421()
        {
            // Arrange
            var hits = new int[] {4, 2};
            int n = 2;
            CultureInfo ci = new CultureInfo("ru-Ru");
            Thread.CurrentThread.CurrentCulture = ci;
            
            // Act
            var result = Level1Solved.PatternUnlock(n, hits);
            
            // Assert
            Assert.AreEqual("141421", result);
        }
        
        [Test]
        public void PatternUnlock_receive_141421()
        {
            // Arrange
            CultureInfo ci = new CultureInfo("ru-Ru");
            Thread.CurrentThread.CurrentCulture = ci;
            var hits = new int[] {4, 2};
            int n = 2;
            
            // Act
            var result = Level1Solved.PatternUnlock(n, hits);
            
            // Assert
            Assert.AreEqual("141421", result);
        }
        
        [Test]
        public void PatternUnlock_receive_982843()
        {
            // Arrange
            CultureInfo ci = new CultureInfo("ru-Ru");
            Thread.CurrentThread.CurrentCulture = ci;
            var hits = new int[] {1,2,3,4,5,6,2,7,8,9};
            int n = 10;
            
            // Act
            var result = Level1Solved.PatternUnlock(n, hits);
            
            // Assert
            Assert.AreEqual("982843", result);
        }
        
        [Test]
        public void PatternUnlock_receive_2()
        {
            // Arrange
            CultureInfo ci = new CultureInfo("ru-Ru");
            Thread.CurrentThread.CurrentCulture = ci;
            var hits = new int[] {9, 8, 7};
            int n = 3;
            
            // Act
            var result = Level1Solved.PatternUnlock(n, hits);
            
            // Assert
            Assert.AreEqual("2", result);
        }
        
        [Test]
        public void PatternUnlock_receive_1()
        {
            // Arrange
            CultureInfo ci = new CultureInfo("ru-Ru");
            Thread.CurrentThread.CurrentCulture = ci;
            var hits = new int[] {1,9,8,7,3,2,5,6,1,2,8};
            int n = 11;
            
            // Act
            var result = Level1Solved.PatternUnlock(n, hits);
            
            // Assert
            Assert.AreEqual("1", result);
        }
    }
}
