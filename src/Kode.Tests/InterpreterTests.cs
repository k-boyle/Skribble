using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Kode.Tests {
    public class InterpreterTests {
        [Test]
        public void TestTokensInAdditionSum() {
            var interpreter = new Interpreter("3 + 5");
            IsInstanceOf<IntegerToken>(interpreter.GetNextToken());
            IsInstanceOf<AdditionToken>(interpreter.GetNextToken());
            IsInstanceOf<IntegerToken>(interpreter.GetNextToken());
            IsInstanceOf<EOFToken>(interpreter.GetNextToken());
        }
        
        [Test]
        public void TestTokensInSubtractionSum() {
            var interpreter = new Interpreter("3 - 5");
            IsInstanceOf<IntegerToken>(interpreter.GetNextToken());
            IsInstanceOf<MinusToken>(interpreter.GetNextToken());
            IsInstanceOf<IntegerToken>(interpreter.GetNextToken());
            IsInstanceOf<EOFToken>(interpreter.GetNextToken());
        }
        
        [Test]
        public void TestThrowsOnUnknownToken() {
            var interpreter = new Interpreter("#");
            //interpreter is ref struct so can't use Throws
            try {
                interpreter.GetNextToken();
            } catch (InvalidTokenException) {
                Pass();
            }
            
            Fail();
        }
    }
}