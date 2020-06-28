using NUnit.Framework;
using System;
using static NUnit.Framework.Assert;

namespace Kode.Tests {
    public class LexerTests {
        [TestCase("123", typeof(IntegerToken))]
        [TestCase("+", typeof(AdditionToken))]
        [TestCase("-", typeof(MinusToken))]
        [TestCase("*", typeof(MultiplicationToken))]
        [TestCase("/", typeof(DivisionToken))]
        [TestCase("", typeof(EOFToken))]
        public void TestTokens(string input, Type expectedTokenType) {
            var lexer = new Lexer(input);
            IsInstanceOf(expectedTokenType, lexer.GetNextToken());
        }
        
        [Test]
        public void TestTokensInAdditionSum() {
            var lexer = new Lexer("3 + 5");
            IsInstanceOf<IntegerToken>(lexer.GetNextToken());
            IsInstanceOf<AdditionToken>(lexer.GetNextToken());
            IsInstanceOf<IntegerToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestTokensInSubtractionSum() {
            var lexer = new Lexer("3 - 5");
            IsInstanceOf<IntegerToken>(lexer.GetNextToken());
            IsInstanceOf<MinusToken>(lexer.GetNextToken());
            IsInstanceOf<IntegerToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestTokensInMultiplicationSum() {
            var lexer = new Lexer("3 * 5");
            IsInstanceOf<IntegerToken>(lexer.GetNextToken());
            IsInstanceOf<MultiplicationToken>(lexer.GetNextToken());
            IsInstanceOf<IntegerToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestTokensInDivisionSum() {
            var lexer = new Lexer("3 / 5");
            IsInstanceOf<IntegerToken>(lexer.GetNextToken());
            IsInstanceOf<DivisionToken>(lexer.GetNextToken());
            IsInstanceOf<IntegerToken>(lexer.GetNextToken());
            IsInstanceOf<EOFToken>(lexer.GetNextToken());
        }
        
        [Test]
        public void TestThrowsOnUnknownToken() {
            var lexer = new Lexer("#");
            //lexer is ref struct so can't use Throws
            try {
                lexer.GetNextToken();
            } catch (InvalidTokenException) {
                Pass();
            }
            
            Fail();
        }
    }
}