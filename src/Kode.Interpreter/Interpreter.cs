using System;
using System.Collections.Generic;

namespace Kode {
    public sealed class Interpreter {
        private static readonly Dictionary<int, HashSet<Type>> OperatorPrecendence = new Dictionary<int, HashSet<Type>> {
            [2] = new HashSet<Type> { typeof(AdditionToken), typeof(MinusToken) },
            [1] = new HashSet<Type> { typeof(MultiplicationToken), typeof(DivisionToken), typeof(ModulusToken) }
        };

        private readonly Lexer _lexer;

        private IToken _currentToken;

        private Interpreter(string input) {
            this._lexer = new Lexer(input);
            this._currentToken = this._lexer.GetNextToken();
        }
        
        public static object Evaluate(string input) {
            var interpreter = new Interpreter(input);
            var result = interpreter.Calculate();
            return interpreter._currentToken is EOFToken
                ? result
                : throw new UnexpectedTokenException(interpreter._currentToken, interpreter._lexer.Position);
        }

        private dynamic Calculate() {
            return Calculate(OperatorPrecendence[2], () => Calculate(OperatorPrecendence[1], () => GetFactor()));
        }
        
        private dynamic Calculate(HashSet<Type> operators, Func<dynamic> next) {
            dynamic result = next();
            
            while (this._currentToken is IOperatorToken op && operators.Contains(op.GetType())) {
                this._currentToken = this._lexer.GetNextToken();
                result = op.Calculate(result, next());
            }
            
            return result;
        }

        private dynamic GetFactor() {
            switch (this._currentToken) {
                case INumberToken number:
                    this._currentToken = this._lexer.GetNextToken();
                    return number.Value;
                
                case OpenParenthesesToken _:
                    this._currentToken = this._lexer.GetNextToken();
                    dynamic result = Calculate();
                    if (this._currentToken is CloseParenthesesToken) {
                        this._currentToken = this._lexer.GetNextToken();
                        return result;
                    }
            
                    throw new UnexpectedTokenException(typeof(CloseParenthesesToken), this._currentToken);
                
                default:
                    throw new UnexpectedTokenException(this._currentToken, this._lexer.Position);
            }
        }
    }
}