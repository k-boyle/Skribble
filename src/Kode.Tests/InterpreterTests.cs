﻿using NUnit.Framework;
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
        [TestCase("1.2 + 2.3", 3.5)]
        [TestCase("1.2 + 3", 4.2)]
        [TestCase("2.5 / 2", 1.25)]
        [TestCase("2 * (3 + 1)", 8)]
        [TestCase("(2.3 + 1) * 2", 6.6)]
        [TestCase("2 + ((3 * 4))", 14)]
        [TestCase("2 + ((3 * 4) * 2)", 26)]
        [TestCase("10 + (3 + 4)", 17)]
        [TestCase("10 + (3 + (4 * 2))", 21)]
        [TestCase("7 / 2", 3.5)]
        [TestCase("7.0 / 2", 3.5)]
        [TestCase("-3", -3)]
        [TestCase("--3", 3)]
        [TestCase("5 - -2", 7)]
        [TestCase("5 * -2", -10)]
        public void TestCalculations(string input, object expectedResult) {
            AreEqual(expectedResult, Interpreter.Evaluate(input));
        }
        
        [TestCase("1 + 2 + 2)")]
        [TestCase("1 + 2 + 2))")]
        public void TestThrowsOnInvalidBracket(string input) {
            Throws<UnexpectedTokenException>(() => Interpreter.Evaluate(input));
        }
    }
}