using NUnit.Framework;
using System.Collections.Generic;
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
                                PlusToken.Instance,
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
                                    PlusToken.Instance,
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
                            PlusToken.Instance, 
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
                                PlusToken.Instance,
                                new DoubleNode(3)),
                            LeftBitshiftToken.Instance,
                            new DoubleNode(2)))
                },
                new object[] {
                    "2 + sin 10",
                    new RootNode(
                        new BinaryOperaterNode(
                            new DoubleNode(2),
                            PlusToken.Instance,
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
                                PlusToken.Instance, 
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
                            PlusToken.Instance, 
                            new DoubleNode(2))) 
                },
                new object[] {
                    "fn test a\nret",
                    new RootNode(
                        new FunctionNode(
                            new VarCharToken("test"),
                            new List<VarCharToken> {
                                new VarCharToken("a")
                            },
                            new ISyntaxTreeNode[] { })) 
                },
                new object[] {
                    "fn test a\nret\n10 + 2",
                    new RootNode(
                        new FunctionNode(
                            new VarCharToken("test"),
                            new List<VarCharToken> {
                                new VarCharToken("a")
                            },
                            new ISyntaxTreeNode[] { }),
                        new BinaryOperaterNode(
                            new DoubleNode(10),
                            PlusToken.Instance,
                            new DoubleNode(2))) 
                },
                new object[] {
                    "fn test a\nb = a + 10\nret\n10 + 2",
                    new RootNode(
                        new FunctionNode(
                            new VarCharToken("test"),
                            new List<VarCharToken> {
                                new VarCharToken("a")
                            },
                            new ISyntaxTreeNode[] {
                                new AssignmentNode(
                                    new VarCharToken("b"),
                                    new BinaryOperaterNode(
                                        new VariableNode(
                                            new VarCharToken("a")),
                                        PlusToken.Instance,
                                        new DoubleNode(10)))
                            }),
                        new BinaryOperaterNode(
                            new DoubleNode(10),
                            PlusToken.Instance,
                            new DoubleNode(2))) 
                },
                new object[] {
                    "fn test\nret",
                    new RootNode(
                        new FunctionNode(
                            new VarCharToken("test"),
                            new List<VarCharToken>(),
                            new ISyntaxTreeNode[] { }))
                },
                new object[] {
                    "fn test\nfn test2 a b c\nd = a + b + c\nret\nret",
                    new RootNode(
                        new FunctionNode(
                            new VarCharToken("test"),
                            new List<VarCharToken>(),
                            new ISyntaxTreeNode[] {
                                new FunctionNode(
                                    new VarCharToken("test2"),
                                    new List<VarCharToken> {
                                        new VarCharToken("a"),
                                        new VarCharToken("b"),
                                        new VarCharToken("c")
                                    }, 
                                    new ISyntaxTreeNode[] {
                                        new AssignmentNode(
                                            new VarCharToken("d"),
                                            new BinaryOperaterNode(
                                                new BinaryOperaterNode(
                                                    new VariableNode(
                                                        new VarCharToken("a")),
                                                    PlusToken.Instance, 
                                                    new VariableNode(
                                                        new VarCharToken("b"))),
                                                PlusToken.Instance, 
                                                new VariableNode(
                                                    new VarCharToken("c"))))
                                    }), 
                            }))
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