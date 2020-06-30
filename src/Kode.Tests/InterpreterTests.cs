using NUnit.Framework;
using System;
using static NUnit.Framework.Assert;

namespace Kode.Tests {
    public class InterpreterTests {
        [TestCase("9", 9)]
        [TestCase("3+4", 7)]
        [TestCase("10-7", 3)]
        [TestCase("    1    +     4    ", 5)]
        [TestCase("2 * 4", 8)]
        [TestCase("8 / 2", 4)]
        [TestCase("8 * 2 / 4", 4)]
        [TestCase("10 + 5 - 3 * 2", 9)]
        [TestCase("4 * 2 + 8 / 4 - 2 * 2", 6)]
        [TestCase("10 % 3", 1)]
        [TestCase("9223372036854775807 + 1", -9223372036854775808)]
        [TestCase("1.2 + 2.3", 3.5)]
        [TestCase("1.2 + 3", 4.2)]
        [TestCase("2.5 / 2", 1.25)]
        [TestCase("2 * (3 + 1)", 8)]
        [TestCase("(2.3 + 1) * 2", 6.6)]
        [TestCase("2 + ((3 * 4))", 14)]
        [TestCase("2 + ((3 * 4) * 2)", 26)]
        [TestCase("10 + (3 + 4)", 17)]
        [TestCase("10 + (3 + (4 * 2))", 21)]
        public void TestCalculations2(string input, object expectedResult) {
            AreEqual(expectedResult, Interpreter.Evaluate(input));
        }

        [Test]
        public void TestIncompleteSumThrows() {
            Throws<UnexpectedTokenException>(() => Interpreter.Evaluate("3 +"));
        }
        
        [Test]
        public void TestInterpreterThrowsOnInvalidToken() {
            Throws<InvalidTokenException>(() => Interpreter.Evaluate("#"));
        }
        
        [TestCase("1 + ((2 + 2)", typeof(UnexpectedTokenException))]
        [TestCase("1 + (2 + 2", typeof(UnexpectedTokenException))]
        [TestCase("1 + 2 + 2)", typeof(UnexpectedTokenException))]
        [TestCase("1 + 2 + 2))", typeof(UnexpectedTokenException))]
        public void TestThrowsOnInvalidBracket(string input, Type expectedExceptionType) {
            Throws(expectedExceptionType, () => Interpreter.Evaluate(input));
        }
    }
}