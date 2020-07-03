using NUnit.Framework;
using System;
using static NUnit.Framework.Assert;

namespace Kode.Tests {
    public class TokenHelperTests {
        [TestCase("+", typeof(PositiveToken))]
        [TestCase("-", typeof(NegativeToken))]
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
        public void TestTokens(string input, Type expectedTokenType) {
            TokenHelper.Find(input.AsMemory(), out var token);
            IsInstanceOf(expectedTokenType, token);
        }
    }
}