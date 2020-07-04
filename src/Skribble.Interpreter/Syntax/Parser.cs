using System;
using System.Collections.Generic;

namespace Skribble {
    internal class Parser {
        private static readonly HashSet<Type>[] OperatorPrecendence = {
            new HashSet<Type> {
                typeof(LeftBitshiftToken),
                typeof(RightBitshiftToken)
            },
            new HashSet<Type> {
                typeof(PositiveToken),
                typeof(NegativeToken)
            },
            new HashSet<Type> {
                typeof(MultiplicationToken),
                typeof(DivisionToken),
                typeof(ModulusToken)
            },
            new HashSet<Type> { typeof(PowerToken) }
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

            if (this._currentToken is VarCharToken vchar) {
                throw new UnexpectedTokenException(typeof(EOLToken), vchar);
            }
        }

        private ISyntaxTreeNode Statement() {
            return this._currentToken switch {
                VarCharToken vchar => Assignment(vchar),
                _                  => Expression()
            };
        }
        
        private ISyntaxTreeNode Assignment(VarCharToken vchar) {
            this._currentToken = this._lexer.GetNextToken();
            if (this._currentToken is AssignmentToken) {
                this._currentToken = this._lexer.GetNextToken();
                return new AssignmentNode(vchar, Expression());
            }

            throw new UnexpectedTokenException(typeof(AssignmentToken), this._currentToken);
        }


        private ISyntaxTreeNode Expression(int precedence = 0) {
            ISyntaxTreeNode node;
            if (precedence == OperatorPrecendence.Length) {
                switch (this._currentToken) {
                    case DoubleToken number:
                        this._currentToken = this._lexer.GetNextToken();
                        return new DoubleNode(number);

                    case OpenParenthesesToken _:
                        this._currentToken = this._lexer.GetNextToken();
                        node = Expression(0);
                        if (this._currentToken is CloseParenthesesToken) {
                            this._currentToken = this._lexer.GetNextToken();
                            return node;
                        }

                        throw new UnexpectedTokenException(typeof(CloseParenthesesToken), this._currentToken);

                    case IUnaryOperatorToken unary:
                        this._currentToken = this._lexer.GetNextToken();
                        return new UnaryOperatorNode(unary, Expression(0));

                    case VarCharToken vchar:
                        this._currentToken = this._lexer.GetNextToken();
                        return new VariableNode(vchar);
                    
                    default:
                        throw new UnexpectedTokenException(this._currentToken, this._lexer.Position);
                }
            }

            node = Expression(precedence + 1);
            HashSet<Type> operators = OperatorPrecendence[precedence];
            while (this._currentToken is IBinaryOperatorToken op && operators.Contains(op.GetType())) {
                this._currentToken = this._lexer.GetNextToken();
                node = new BinaryOperaterNode(node, op, Expression(precedence + 1));
            }

            return node;
        }
    }
}