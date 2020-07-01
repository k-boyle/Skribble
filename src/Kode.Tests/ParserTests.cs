using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Kode.Tests {
    public class ParserTests {
        private static object[] SyntaxTrees() {
            return new object[] {
                new object[] {
                    "3 * 2",
                    new OperaterNode(
                        new NumberNode(3),
                        MultiplicationToken.Instance,
                        new NumberNode(2))
                },
                new object[] {
                    "2 * (1 + 1)",
                    new OperaterNode(
                        new NumberNode(2),
                        MultiplicationToken.Instance,
                        new OperaterNode(
                            new NumberNode(1),
                            AdditionToken.Instance,
                            new NumberNode(1))), 
                },
                new object[] {
                    "(2 * (5 + 1)) / (8 % (1 * 2))",
                    new OperaterNode(
                        new OperaterNode(
                            new NumberNode(2),
                            MultiplicationToken.Instance,
                            new OperaterNode(
                                new NumberNode(5),
                                AdditionToken.Instance,
                                new NumberNode(1))),
                        DivisionToken.Instance,
                        new OperaterNode(
                            new NumberNode(8),
                            ModulusToken.Instance,
                            new OperaterNode(
                                new NumberNode(1),
                                MultiplicationToken.Instance,
                                new NumberNode(2)))), 
                }
            };
        }

        [TestCaseSource(nameof(SyntaxTrees))]
        public void TestSyntaxTrees(string input, object expectedTree) {
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            var parsed = parser.Parse();
            AreEqual(expectedTree, parsed);
        }
        
        [Test]
        public void TestIncompleteSumThrows() {
            Throws<UnexpectedTokenException>(() => new Parser(new Lexer("3 +")).Parse());
        }
        
        [TestCase("1 + ((2 + 2)")]
        [TestCase("1 + (2 + 2")]
        public void TestThrowsOnInvalidBracket(string input) {
            Throws<UnexpectedTokenException>(() => new Parser(new Lexer(input)).Parse());
        }
    }
}