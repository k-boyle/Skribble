using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Kode.Tests {
    public class InterpreterTests {
        [TestCase("9", 9)]
        [TestCase("3+4", 7)]
        [TestCase("10-7", 3)]
        [TestCase("    1    +     4    ", 5)]
        [TestCase("2 * 4", 8)]
        [TestCase("8 / 2", 4)]
        [TestCase("10 + 5 - 3 * 2", 24)]
        [TestCase("10 + (3 + 4)", 17)]
        [TestCase("10 + (3 + (4 * 2))", 21)]
        [TestCase("10 % 3", 1)]
        public void TestCalculations(string input, int expectedResult) {
            AreEqual(expectedResult, Interpreter.Evaluate(input));
        }
        
        [Test]
        public void TestIncompleteSumThrows() {
            try {
                Interpreter.Evaluate("3 +");
            } catch (UnexpectedTokenException) {
                Pass();
            }
            
            Fail();
        }
        
        [Test]
        public void TestInterpreterThrowsOnInvalidToken() {
            try {
                Interpreter.Evaluate("#");
            } catch (InvalidTokenException) {
                Pass();
            }
            
            Fail();
        }
    }
}