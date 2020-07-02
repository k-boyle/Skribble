using System;
using System.Collections.Generic;

namespace Kode {
    internal class Parser {
        private static readonly HashSet<Type>[] OperatorPrecendence = {
            new HashSet<Type> { typeof(PositiveToken), typeof(NegativeToken) },
            new HashSet<Type> { typeof(MultiplicationToken), typeof(DivisionToken), typeof(ModulusToken) }
        };

        public IToken CurrentToken => this._currentToken;
        
        private readonly Lexer _lexer;

        private IToken _currentToken;

        public Parser(Lexer lexer) {
            this._lexer = lexer;
            this._currentToken = this._lexer.GetNextToken();
        }
        
        public ISyntaxTreeNode Parse() {
            return Parse(0);
        }
        
        private ISyntaxTreeNode Parse(int precedence) {
            ISyntaxTreeNode node;
            if (precedence == OperatorPrecendence.Length) {
                switch (this._currentToken) {
                    case DoubleToken number:
                        this._currentToken = this._lexer.GetNextToken();
                        return new DoubleNode(number);
                    
                    case OpenParenthesesToken _:
                        this._currentToken = this._lexer.GetNextToken();
                        node = Parse(0);
                        if (this._currentToken is CloseParenthesesToken) {
                            this._currentToken = this._lexer.GetNextToken();
                            return node;
                        }
                        
                        throw new UnexpectedTokenException(typeof(CloseParenthesesToken), this._currentToken);
                    
                    case IUnaryOperatorToken unary:
                        this._currentToken = this._lexer.GetNextToken();
                        return new UnaryOperatorNode(unary, Parse(0));

                    default:
                        throw new UnexpectedTokenException(this._currentToken, this._lexer.Position);
                }
            }

            node = Parse(precedence + 1);
            HashSet<Type> operators = OperatorPrecendence[precedence];
            while (this._currentToken is IBinaryOperatorToken op && operators.Contains(op.GetType())) {
                this._currentToken = this._lexer.GetNextToken();
                node = new BinaryOperaterNode(node, op, Parse(precedence + 1));
            }

            return node;
        }
    }
}