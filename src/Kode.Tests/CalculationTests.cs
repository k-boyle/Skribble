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
        public void TestWhiteSpacesIgnored() {
            int result = Kode.Execute("    1    +     4    ");
            AreEqual(5, result);
        }

        [Test]
        public void TestMultiplication() {
            int result = Kode.Execute("2 * 4");
            AreEqual(8, result);
        }

        [Test]
        public void TestDivision() {
            int result = Kode.Execute("8 / 2");
            AreEqual(4, result);
        }
        
        [Test]
        public void TestMixedOperatorSum() {
            int result = Kode.Execute("10 + 5 - 3 * 2");
            AreEqual(24, result);
        }
    }
}