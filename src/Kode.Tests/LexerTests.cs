using NUnit.Framework;
using System;
using System.Numerics;
using static NUnit.Framework.Assert;

namespace Kode.Tests {
    public class LexerTests {
        [TestCase("123", typeof(LongToken))]
        [TestCase("1.2", typeof(DoubleToken))]
        [TestCase("+", typeof(PositiveToken))]
        [TestCase("-", typeof(NegativeToken))]
        [TestCase("*", typeof(MultiplicationToken))]
        [TestCase("/", typeof(DivisionToken))]
        [TestCase("", typeof(EOFToken))]
        [TestCase("(", typeof(OpenParenthesesToken))]
        [TestCase(")", typeof(CloseParenthesesToken))]
        [TestCase("%", typeof(ModulusToken))]
        public void TestTokens(string input, Type expectedTokenType) {
            var lexer = new Lexer(input);
            IsInstanceOf(expectedTokenType, lexer.GetNextToken());
        }
        
        [Test]
        public void TestTokensInAdditionSum() {
            var lexer = new Lexer("3 + 5");
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<PositiveToken>(lexer.GetNextToken());
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestTokensInSubtractionSum() {
            var lexer = new Lexer("3 - 5");
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<NegativeToken>(lexer.GetNextToken());
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestTokensInMultiplicationSum() {
            var lexer = new Lexer("3 * 5");
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<MultiplicationToken>(lexer.GetNextToken());
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }

        [Test]
        public void TestTokensInDivisionSum() {
            var lexer = new Lexer("3 / 5");
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<DivisionToken>(lexer.GetNextToken());
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }

        [Test]
        public void TestTokensInBracketedSum() {
            var lexer = new Lexer("3 + (4 + 4)");
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<PositiveToken>(lexer.GetNextToken());
            IsInstanceOf<OpenParenthesesToken>(lexer.GetNextToken());
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<PositiveToken>(lexer.GetNextToken());
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<CloseParenthesesToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        [Test]
        public void TestTokensInNestedBracketedSum() {
            var lexer = new Lexer("3 + (4 + (4 * 2))");
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<PositiveToken>(lexer.GetNextToken());
            IsInstanceOf<OpenParenthesesToken>(lexer.GetNextToken());
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<PositiveToken>(lexer.GetNextToken());
            IsInstanceOf<OpenParenthesesToken>(lexer.GetNextToken());
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<MultiplicationToken>(lexer.GetNextToken());
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<CloseParenthesesToken>(lexer.GetNextToken());
            IsInstanceOf<CloseParenthesesToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }

        [Test]
        public void TestTokensInModulusSum() {
            var lexer = new Lexer("10 % 2");
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<ModulusToken>(lexer.GetNextToken());
            IsInstanceOf<LongToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestDoubleSum() {
            var lexer = new Lexer("1.2 + 3.1");
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<PositiveToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [TestCase("#")]
        [TestCase("1..2")]
        [TestCase("1.2.3")]
        public void TestThrowsOnInvalidToken(string input) {
            var lexer = new Lexer(input);
            Throws<InvalidTokenException>(() => {
                while (!(lexer.GetNextToken() is EOFToken)) ;
            });
        }
        
        private static object[] OutOfRangeNumbers() {
            return new object[] {
                (new BigInteger(long.MaxValue) + 1).ToString(),
                (new BigInteger(long.MinValue) - 1).ToString(),
                (new BigInteger(double.MaxValue) + 1).ToString(),
                double.MinValue.ToString($".{new string('#', 324)}") + "1"
            };
        }
        
        [TestCaseSource(nameof(OutOfRangeNumbers))]
        public void TestThrowsOnOutOfRangeNumbers(string input) {
            var lexer = new Lexer(input);
            Throws<NumberParseFailedException>(() => {
                while (!(lexer.GetNextToken() is EOFToken)) ;
            });
        }
    }
}