using Level1Space;
using NUnit.Framework;

namespace Level1SpaceTests
{
    public class TheRabbitsFootTests
    {
        [Test]
        public void Encode1()
        {
            string s = "отдай мою кроличью лапку";

            var res = Level1Solved.TheRabbitsFoot(s, true);
            
            Assert.AreEqual("омоюу толл дюиа акчп йрьк", res);
        }
        
        [Test]
        public void Encode2()
        {
            string s = "какая-то тестовая строка для кодирования символов";

            var res = Level1Solved.TheRabbitsFoot(s, true);
            
            Assert.AreEqual("кторяосо аовоквив ктакоам аеяаднв яссдиио -ттлрял", res);
        }
        
        [Test]
        public void Decode2()
        {
            string s = "кторяосо аовоквив ктакоам аеяаднв яссдиио -ттлрял";

            var res = Level1Solved.TheRabbitsFoot(s, false);
            
            Assert.AreEqual("какая-тотестоваястрокадлякодированиясимволов", res);
        }
        
        [Test]
        public void Decode1()
        {
            string s = "омоюу толл дюиа акчп йрьк";

            var res = Level1Solved.TheRabbitsFoot(s, false);
            
            Assert.AreEqual("отдаймоюкроличьюлапку", res);
        }
    }
}