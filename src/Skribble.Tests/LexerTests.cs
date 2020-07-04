using NUnit.Framework;
using System;
using static NUnit.Framework.Assert;

namespace Skribble.Tests {
    public class LexerTests {
        [TestCase("123", typeof(DoubleToken))]
        [TestCase("1.2", typeof(DoubleToken))]
        [TestCase("", typeof(EOFToken))]
        [TestCase("abc", typeof(VarCharToken))]
        [TestCase("abc1", typeof(VarCharToken))]
        [TestCase("+", typeof(PlusToken))]
        [TestCase("-", typeof(MinusToken))]
        [TestCase("*", typeof(MultiplicationToken))]
        [TestCase("/", typeof(DivisionToken))]
        [TestCase("(", typeof(OpenParenthesesToken))]
        [TestCase(")", typeof(CloseParenthesesToken))]
        [TestCase("%", typeof(ModulusToken))]
        [TestCase("**", typeof(PowerToken))]
        [TestCase("<<", typeof(LeftBitshiftToken))]
        [TestCase(">>", typeof(RightBitshiftToken))]
        [TestCase("pow", typeof(PowerToken))]
        [TestCase("sin", typeof(SineToken))]
        [TestCase("cos", typeof(CosineToken))]
        [TestCase("tan", typeof(TangentToken))]
        [TestCase("=", typeof(AssignmentToken))]
        [TestCase("\n", typeof(EOLToken))]
        [TestCase("\r\n", typeof(EOLToken))]
        public void TestTokens(string input, Type expectedTokenType) {
            var lexer = new Lexer(input);
            IsInstanceOf(expectedTokenType, lexer.GetNextToken());
        }
        
        [Test]
        public void TestTokensInAdditionSum() {
            var lexer = new Lexer("3 + 5");
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<PlusToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestTokensInSubtractionSum() {
            var lexer = new Lexer("3 - 5");
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<MinusToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestTokensInMultiplicationSum() {
            var lexer = new Lexer("3 * 5");
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<MultiplicationToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }

        [Test]
        public void TestTokensInDivisionSum() {
            var lexer = new Lexer("3 / 5");
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<DivisionToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }

        [Test]
        public void TestTokensInBracketedSum() {
            var lexer = new Lexer("3 + (4 + 4)");
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<PlusToken>(lexer.GetNextToken());
            IsInstanceOf<OpenParenthesesToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<PlusToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<CloseParenthesesToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        [Test]
        public void TestTokensInNestedBracketedSum() {
            var lexer = new Lexer("3 + (4 + (4 * 2))");
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<PlusToken>(lexer.GetNextToken());
            IsInstanceOf<OpenParenthesesToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<PlusToken>(lexer.GetNextToken());
            IsInstanceOf<OpenParenthesesToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<MultiplicationToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<CloseParenthesesToken>(lexer.GetNextToken());
            IsInstanceOf<CloseParenthesesToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }

        [Test]
        public void TestTokensInModulusSum() {
            var lexer = new Lexer("10 % 2");
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<ModulusToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestDoubleSum() {
            var lexer = new Lexer("1.2 + 3.1");
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<PlusToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestMultiLengthOperator() {
            var lexer = new Lexer("2 ** 3");
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<PowerToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestVariableAssignment() {
            var lexer = new Lexer("a = 10");
            IsInstanceOf<VarCharToken>(lexer.GetNextToken());
            IsInstanceOf<AssignmentToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
       
        [Test]
        public void TestMultipleVariablesAndAssignments() {
            var lexer = new Lexer("a = 10\nb = 20\nc = a + b");
            IsInstanceOf<VarCharToken>(lexer.GetNextToken());
            IsInstanceOf<AssignmentToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOLToken>(lexer.GetNextToken());
            IsInstanceOf<VarCharToken>(lexer.GetNextToken());
            IsInstanceOf<AssignmentToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOLToken>(lexer.GetNextToken());
            IsInstanceOf<VarCharToken>(lexer.GetNextToken());
            IsInstanceOf<AssignmentToken>(lexer.GetNextToken());
            IsInstanceOf<VarCharToken>(lexer.GetNextToken());
            IsInstanceOf<PlusToken>(lexer.GetNextToken());
            IsInstanceOf<VarCharToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestMultipleNewLines() {
            var lexer = new Lexer("a = 10\n\r\nb = 20");
            IsInstanceOf<VarCharToken>(lexer.GetNextToken());
            IsInstanceOf<AssignmentToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOLToken>(lexer.GetNextToken());
            IsInstanceOf<EOLToken>(lexer.GetNextToken());
            IsInstanceOf<VarCharToken>(lexer.GetNextToken());
            IsInstanceOf<AssignmentToken>(lexer.GetNextToken());
            IsInstanceOf<DoubleToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [TestCase("#", typeof(InvalidTokenException))]
        [TestCase("1..2", typeof(InvalidTokenException))]
        [TestCase("1.2.3", typeof(NumberParseFailedException))]
        public void TestThrowsOnInvalidToken(string input, Type expectedExceptionType) {
            var lexer = new Lexer(input);
            Throws(expectedExceptionType, () => {
                while (!(lexer.GetNextToken() is EOFToken)) ;
            });
        }
    }
}