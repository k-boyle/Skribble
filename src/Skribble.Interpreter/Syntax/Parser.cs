using System;
using System.Collections.Generic;
using System.Linq;

namespace Skribble {
    internal class Parser {
        private static readonly HashSet<Type>[] OperatorPrecendence = {
            new HashSet<Type> {
                typeof(LeftBitshiftToken),
                typeof(RightBitshiftToken)
            },
            new HashSet<Type> {
                typeof(PlusToken),
                typeof(MinusToken)
            },
            new HashSet<Type> {
                typeof(MultiplicationToken),
                typeof(DivisionToken),
                typeof(ModulusToken)
            },
            new HashSet<Type> {
                typeof(PowerToken)
            }
        };

        public IToken CurrentToken => this._currentToken;

        private readonly Lexer _lexer;

        private IToken _currentToken;

        public Parser(Lexer lexer) {
            this._lexer = lexer;
            this._currentToken = this._lexer.GetNextToken();
        }

        public RootNode Parse() {
            return new RootNode(Statements());
        }
        
        private IEnumerable<ISyntaxTreeNode> Statements() {
            yield return Statement();
            while (this._currentToken is EOLToken) {
                this._currentToken = this._lexer.GetNextToken();
                yield return Statement();
            }
        }

        private ISyntaxTreeNode Statement() {
            return this._currentToken switch {
                VarCharToken vchar => Assignment(vchar),
                FunctionToken _    => Function(),
                ReturnToken _      => NopNode.Instance,
                _                  => Expression()
            };
        }
        
        private AssignmentNode Assignment(VarCharToken vchar) {
            this._currentToken = this._lexer.GetNextToken();
            if (this._currentToken is AssignmentToken) {
                this._currentToken = this._lexer.GetNextToken();
                return new AssignmentNode(vchar, Expression());
            }

            throw new UnexpectedTokenException(typeof(AssignmentToken), this._currentToken);
        }

        private FunctionNode Function() {
            this._currentToken = this._lexer.GetNextToken();
            if (!(this._currentToken is VarCharToken name)) {
                throw new UnexpectedTokenException(typeof(VarCharToken), this._currentToken);
            }

            var parameters = new List<VarCharToken>();
            while ((this._currentToken = this._lexer.GetNextToken()) is VarCharToken param) {
                parameters.Add(param);
            }

            if (!(this._currentToken is EOLToken)) {
                throw new UnexpectedTokenException(typeof(EOLToken), this._currentToken);
            }

            this._currentToken = this._lexer.GetNextToken();
            IEnumerable<ISyntaxTreeNode> function = Statements().ToArray(); //todo get rid of this
            if (!(this._currentToken is ReturnToken)) {
                throw new UnexpectedTokenException(typeof(ReturnToken), this._currentToken);
            }

            this._currentToken = this._lexer.GetNextToken();
            return new FunctionNode(name, parameters, function);
        }
        
        private ISyntaxTreeNode Expression(int precedence = 0) {
            ISyntaxTreeNode node;
            
            if (this._currentToken is ReturnToken) {
                this._currentToken = this._lexer.GetNextToken();
                return NopNode.Instance;
            }
            
            if (precedence == OperatorPrecendence.Length) {
                switch (this._currentToken) {
                    case DoubleToken number:
                        this._currentToken = this._lexer.GetNextToken();
                        return new DoubleNode(number);

                    case OpenParenthesesToken _:
                        this._currentToken = this._lexer.GetNextToken();
                        node = Expression();
                        if (this._currentToken is CloseParenthesesToken) {
                            this._currentToken = this._lexer.GetNextToken();
                            return node;
                        }

                        throw new UnexpectedTokenException(typeof(CloseParenthesesToken), this._currentToken);

                    case IUnaryOperatorToken unary:
                        this._currentToken = this._lexer.GetNextToken();
                        return new UnaryOperatorNode(unary, Expression());

                    case VarCharToken vchar:
                        this._currentToken = this._lexer.GetNextToken();
                        return new VariableNode(vchar);

                    default:
                        throw new UnexpectedTokenException(this._currentToken, this._lexer.Position);
                }
            }

            node = Expression(precedence + 1);
            var operators = OperatorPrecendence[precedence];
            while (this._currentToken is IBinaryOperatorToken op && operators.Contains(op.GetType())) {
                this._currentToken = this._lexer.GetNextToken();
                node = new BinaryOperaterNode(node, op, Expression(precedence + 1));
            }

            return node;
        }
    }
}