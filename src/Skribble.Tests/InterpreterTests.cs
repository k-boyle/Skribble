using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using static NUnit.Framework.Assert;

namespace Skribble.Tests {
    internal class InterpreterTests {
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
        [TestCase("2 ** 3", 8)]
        [TestCase("2 pow 3", 8)]
        [TestCase("8 + 2 ** 3", 16)]
        [TestCase("2 + 3 << 2", 20)]
        [TestCase("sin 90", 1)]
        [TestCase("2 * sin 90", 2)]
        public void TestCalculations(string input, double expectedResult) {
            AreEqual(expectedResult, Interpreter.Evaluate(input));
        }
        
        [TestCase("a = 10")]
        [TestCase("a = 10\nb = 20\nc = a + b")]
        public void TestScriptsWithoutReturn(string input) {
            AreEqual(null, Interpreter.Evaluate(input));
        }
        
        [TestCase("1 + 2 + 2)")]
        [TestCase("1 + 2 + 2))")]
        public void TestThrowsOnInvalidBracket(string input) {
            Throws<UnexpectedTokenException>(() => Interpreter.Evaluate(input));
        }
        
        [Test]
        public void TestThrowsOnUnknownVariable() {
            Throws<UnknownVariableException>(() => Interpreter.Evaluate("a = b + 10"));
        }
        
        [TestCaseSource(nameof(SyntaxTrees))]
        public void TestSyntaxTreeEvaluation(string test, RootNode tree, double? expectedOutcome) {
            var parser = new Mock<Parser>();
            parser.Setup(p => p.Parse()).Returns(tree);
            var interpreter = new Interpreter(parser.Object);
            AreEqual(expectedOutcome, interpreter.NavigateSyntaxTree());
            AreEqual(expectedOutcome, Interpreter.Evaluate(test));
            parser.Verify();
        }
        
        private static object[] SyntaxTrees() {
            return new object[] {
                new object[] {
                    "3 * 2",
                    new RootNode(
                        new BinaryOperaterNode(
                            new DoubleNode(3),
                            MultiplicationToken.Instance,
                            new DoubleNode(2))),
                    6d
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
                                new DoubleNode(1)))),
                    4d
                },
                new object[] {
                    "(2 * (5 + 1)) / (8 % (3 * 2))",
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
                                    new DoubleNode(3),
                                    MultiplicationToken.Instance,
                                    new DoubleNode(2))))),
                    6d
                },
                new object[] {
                    "2 ** 3",
                    new RootNode(
                        new BinaryOperaterNode(
                            new DoubleNode(2),
                            PowerToken.Instance,
                            new DoubleNode(3))),
                    8d
                },
                new object[] {
                    "2 pow 3",
                    new RootNode(
                        new BinaryOperaterNode(
                            new DoubleNode(2),
                            PowerToken.Instance,
                            new DoubleNode(3))),
                    8d
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
                                new DoubleNode(2)))),
                    7d
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
                            new DoubleNode(2))),
                    20d
                },
                new object[] {
                    "2 + sin 10",
                    new RootNode(
                        new BinaryOperaterNode(
                            new DoubleNode(2),
                            PlusToken.Instance,
                            new UnaryOperatorNode(
                                SineToken.Instance,
                                new DoubleNode(10)))),
                    2.1736481776669305d
                },
                new object[] {
                    "a = 10",
                    new RootNode(
                        new AssignmentNode(
                            new VarCharToken("a"),
                            new DoubleNode(10))),
                    null
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
                                    new VarCharToken("b"))))) ,
                    null
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
                            new DoubleNode(2))),
                    10d
                },
                new object[] {
                    "fn test a\nret",
                    new RootNode(
                        new FunctionNode(
                            new VarCharToken("test"),
                            new List<VarCharToken> {
                                new VarCharToken("a")
                            },
                            new ISyntaxTreeNode[] { })),
                    null
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
                            new DoubleNode(2))),
                    12d
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
                            new DoubleNode(2))),
                    12d
                },
                new object[] {
                    "fn test\nret",
                    new RootNode(
                        new FunctionNode(
                            new VarCharToken("test"),
                            new List<VarCharToken>(),
                            new ISyntaxTreeNode[] { })),
                    null
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
                            })),
                    null
                }
            };
        }
    }
}