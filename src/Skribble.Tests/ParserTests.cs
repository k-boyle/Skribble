using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Skribble.Tests {
    internal class ParserTests {
        private static object[] SyntaxTrees() {
            return new object[] {
                new object[] {
                    "3 * 2",
                    new RootNode(
                        new BinaryOperaterNode(
                            new DoubleNode(3),
                            MultiplicationToken.Instance,
                            new DoubleNode(2)))
                },
                new object[] {
                    "2 * (1 + 1)",
                    new RootNode( 
                        new BinaryOperaterNode(
                            new DoubleNode(2),
                            MultiplicationToken.Instance,
                            new BinaryOperaterNode(
                                new DoubleNode(1),
                                PositiveToken.Instance,
                                new DoubleNode(1)))) 
                },
                new object[] {
                    "(2 * (5 + 1)) / (8 % (1 * 2))",
                    new RootNode(
                        new BinaryOperaterNode(
                            new BinaryOperaterNode(
                                new DoubleNode(2),
                                MultiplicationToken.Instance,
                                new BinaryOperaterNode(
                                    new DoubleNode(5),
                                    PositiveToken.Instance,
                                    new DoubleNode(1))),
                            DivisionToken.Instance,
                            new BinaryOperaterNode(
                                new DoubleNode(8),
                                ModulusToken.Instance,
                                new BinaryOperaterNode(
                                    new DoubleNode(1),
                                    MultiplicationToken.Instance,
                                    new DoubleNode(2)))))
                },
                new object[] {
                    "2 ** 3",
                    new RootNode(
                        new BinaryOperaterNode(
                            new DoubleNode(2),
                            PowerToken.Instance,
                            new DoubleNode(3)))
                },
                new object[] {
                    "2 pow 3",
                    new RootNode(
                        new BinaryOperaterNode(
                            new DoubleNode(2),
                            PowerToken.Instance,
                            new DoubleNode(3)))
                },
                new object[] {
                    "3 + 2 ** 2",
                    new RootNode(
                        new BinaryOperaterNode(
                            new DoubleNode(3),
                            PositiveToken.Instance, 
                            new BinaryOperaterNode(
                                new DoubleNode(2),
                                PowerToken.Instance,
                                new DoubleNode(2))))
                },
                new object[] {
                    "2 + 3 << 2",
                    new RootNode(
                        new BinaryOperaterNode(
                            new BinaryOperaterNode(
                                new DoubleNode(2),
                                PositiveToken.Instance,
                                new DoubleNode(3)),
                            LeftBitshiftToken.Instance,
                            new DoubleNode(2)))
                },
                new object[] {
                    "2 + sin 10",
                    new RootNode(
                        new BinaryOperaterNode(
                            new DoubleNode(2),
                            PositiveToken.Instance,
                            new UnaryOperatorNode(
                                SineToken.Instance,
                                new DoubleNode(10))))
                },
                new object[] {
                    "a = 10",
                    new RootNode(
                        new AssignmentNode(
                            new VarCharToken("a"),
                            new DoubleNode(10)))
                },
                new object[] {
                    "a = 10\nb = 20\nc = a + b",
                    new RootNode(
                        new AssignmentNode(
                            new VarCharToken("a"),
                            new DoubleNode(10)),
                        new AssignmentNode(
                            new VarCharToken("b"),
                            new DoubleNode(20)),
                        new AssignmentNode(
                            new VarCharToken("c"),
                            new BinaryOperaterNode(
                                new VariableNode(
                                    new VarCharToken("a")),
                                PositiveToken.Instance, 
                                new VariableNode(
                                    new VarCharToken("b"))))) 
                },
                new object[] {
                    "a = 10\nb = 20\n8 + 2",
                    new RootNode(
                        new AssignmentNode(
                            new VarCharToken("a"),
                            new DoubleNode(10)),
                        new AssignmentNode(
                            new VarCharToken("b"),
                            new DoubleNode(20)),
                        new BinaryOperaterNode(
                            new DoubleNode(8), 
                            PositiveToken.Instance, 
                            new DoubleNode(2))) 
                }
            };
        }

        [TestCaseSource(nameof(SyntaxTrees))]
        public void TestSyntaxTrees(string input, RootNode expectedTree) {
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            var parsed = parser.Parse();
            AreEqual(expectedTree, parsed);
        }
        
        [TestCase("3 +")]
        [TestCase("3 *** 2")]
        public void TestIncompleteSumThrows(string input) {
            Throws<UnexpectedTokenException>(() => {
                var tree = new Parser(new Lexer(input)).Parse();
                foreach (var node in tree.ChildNodes) {
                }
            });
        }
        
        [TestCase("1 + ((2 + 2)")]
        [TestCase("1 + (2 + 2")]
        public void TestThrowsOnInvalidBracket(string input) {
            Throws<UnexpectedTokenException>(() => {
                var tree = new Parser(new Lexer(input)).Parse();
                foreach (var node in tree.ChildNodes) {
                }
            });
        }
    }
}