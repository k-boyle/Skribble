using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Kode.Tests {
    public class CalculationTests {
        [Test]
        public void TestAddition() {
            int result = Kode.Execute("3+4");
            AreEqual(7, result);
        }
        
        [Test]
        public void TestSubtraction() {
            int result = Kode.Execute("10-7");
            AreEqual(3, result);
        }
        
        [Test]
        public void TestSpacesIgnored() {
            int result = Kode.Execute("    1    +     4    ");
            AreEqual(5, result);
        }
    }
}