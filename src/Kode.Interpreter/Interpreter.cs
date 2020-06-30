using System;
using System.Collections.Generic;

namespace Kode {
    public ref struct Interpreter {
        private static readonly HashSet<Type>[] OperatorPrecendence = {
            new HashSet<Type> { typeof(AdditionToken), typeof(MinusToken) },
            new HashSet<Type> { typeof(MultiplicationToken), typeof(DivisionToken), typeof(ModulusToken) }
        };

        private readonly Lexer _lexer;

        private IToken _currentToken;

        private Interpreter(string input) {
            this._lexer = new Lexer(input);
            this._currentToken = this._lexer.GetNextToken();
        }
        
        public static object Evaluate(string input) {
            var interpreter = new Interpreter(input);
            var result = interpreter.Calculate(0);
            return interpreter._currentToken is EOFToken
                ? result
                : throw new UnexpectedTokenException(interpreter._currentToken, interpreter._lexer.Position);
        }

        private dynamic Calculate(int precedence) {
            dynamic result;
            if (precedence == OperatorPrecendence.Length) {
                switch (this._currentToken) {
                    case INumberToken number:
                        this._currentToken = this._lexer.GetNextToken();
                        return number.Value;
                
                    case OpenParenthesesToken _:
                        this._currentToken = this._lexer.GetNextToken();
                        result = Calculate(0);
                        if (this._currentToken is CloseParenthesesToken) {
                            this._currentToken = this._lexer.GetNextToken();
                            return result;
                        }
            
                        throw new UnexpectedTokenException(typeof(CloseParenthesesToken), this._currentToken);
                
                    default:
                        throw new UnexpectedTokenException(this._currentToken, this._lexer.Position);
                }

            }

            result = Calculate(precedence + 1);
            HashSet<Type> operators = OperatorPrecendence[precedence];
            while (this._currentToken is IOperatorToken op && operators.Contains(op.GetType())) {
                this._currentToken = this._lexer.GetNextToken();
                result = op.Calculate(result, Calculate(precedence + 1));
            }

            return result;
        }
    }
}