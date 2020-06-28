using System;
using System.Collections.Generic;

namespace Kode {
    internal ref struct Interpreter {
        private static readonly Dictionary<char, OperatorToken> Operators = new Dictionary<char, OperatorToken> {
            ['+'] = AdditionToken.Instance,
            ['-'] = MinusToken.Instance,
            ['*'] = MultiplicationToken.Instance,
            ['/'] = DivisionToken.Instance
        };

        private readonly ReadOnlySpan<char> _text;
        private int _position;

        private char? _currentChar;

        public Interpreter(string text) {
            this._text = text.AsSpan();
            this._position = 0;
            this._currentChar = this._text[0];
        }

        private int GetDigitLength() {
            int digitLength = 0;
            do {
                Increment();
                digitLength++;
            } while (this._currentChar.HasValue && char.IsDigit(this._currentChar.Value));

            return digitLength;
        }

        private void SkipSpaces() {
            while (this._currentChar.HasValue && char.IsWhiteSpace(this._currentChar.Value)) {
                Increment();
            }
        }

        private void Increment() {
            if (this._position < this._text.Length - 1) {
                this._currentChar = this._text[++this._position];
            } else {
                this._position++;
                this._currentChar = null;
            }
        }
        
        public Token GetNextToken() {
            SkipSpaces();
            
            if (this._currentChar.HasValue) {
                char current = this._currentChar.Value;
                
                if (char.IsDigit(current)) {
                    var digitLength = GetDigitLength();
                    return new IntegerToken(int.Parse(this._text.Slice(this._position - digitLength, digitLength)));
                }
            
                if (Operators.TryGetValue(current, out var token)) {
                    Increment();
                    return token;
                }

                throw new InvalidTokenException(current);
            }

            return EOFToken.Instance;
        }

        private T GetNextToken<T>() where T : Token {
            Token nextToken = GetNextToken();
            if (nextToken is T token) {
                return token;
            }
            
            throw new UnexpectedTokenException(typeof(T), nextToken);
        }
        
        public int Evaluate() {
            int result = GetNextToken<IntegerToken>();

            while (GetNextToken() is OperatorToken op) {
                result = op.Calculate(result, GetNextToken<IntegerToken>());
            }
            
            GetNextToken<EOFToken>();

            return result;
        }
    }
}