using System;
using System.Collections.Generic;

namespace Kode {
    internal class Lexer {
        private static readonly Dictionary<char, IToken> TokenMap = new Dictionary<char, IToken> {
            ['+'] = AdditionToken.Instance,
            ['-'] = MinusToken.Instance,
            ['*'] = MultiplicationToken.Instance,
            ['/'] = DivisionToken.Instance,
            ['('] = OpenParenthesesToken.Instance,
            [')'] = CloseParenthesesToken.Instance,
            ['%'] = ModulusToken.Instance
        };

        public int Position => this._position;

        private readonly ReadOnlyMemory<char> _text;
        private int _position;

        private char? _currentChar;

        public Lexer(string text) {
            this._text = text.AsMemory();
            this._position = 0;
            this._currentChar = text.Length > 0 ? (char?) this._text.Span[0] : null;
        }
        
        private (int Length, bool FloatingPoint) GetNumberInformation() {
            int digitLength = 0;
            bool floatingPoint = false;
            do {
                Increment();
                digitLength++;
                
                if (this._currentChar == '.') {
                    floatingPoint = !floatingPoint ? true : throw new Exception();
                    digitLength++;
                    Increment();
                }
            } while (this._currentChar.HasValue && char.IsDigit(this._currentChar.Value));

            return (digitLength, floatingPoint);
        }

        private void SkipSpaces() {
            while (this._currentChar.HasValue && char.IsWhiteSpace(this._currentChar.Value)) {
                Increment();
            }
        }

        private void Increment() {
            if (this._position < this._text.Length - 1) {
                this._currentChar = this._text.Span[++this._position];
            } else {
                this._position++;
                this._currentChar = null;
            }
        }
        
        public IToken GetNextToken() {
            SkipSpaces();
            
            if (this._currentChar.HasValue) {
                char current = this._currentChar.Value;
                
                if (char.IsDigit(current)) {
                    var (length, floating) = GetNumberInformation();
                    return floating
                        ? new DoubleToken(double.Parse(this._text.Slice(this._position - length, length).Span))
                        : new LongToken(long.Parse(this._text.Slice(this._position - length, length).Span)) as INumberToken;
                }
            
                if (TokenMap.TryGetValue(current, out var token)) {
                    Increment();
                    return token;
                }

                throw new InvalidTokenException(current, this._position);
            }

            return EOFToken.Instance;
        }
        
        public T GetNextToken<T>() where T : IToken {
            IToken nextToken = GetNextToken();
            if (nextToken is T token) {
                return token;
            }
            
            throw new UnexpectedTokenException(typeof(T), nextToken);
        }
    }
}