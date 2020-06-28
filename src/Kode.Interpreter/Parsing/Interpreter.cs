using System.Collections.Generic;
using System;

namespace Kode {
    internal ref struct Interpreter {
        private static readonly Dictionary<char, OperatorToken> Operators = new Dictionary<char, OperatorToken> {
            ['+'] = AdditionToken.Instance,
            ['-'] = MinusToken.Instance
        };

        private readonly ReadOnlySpan<char> _text;
        private int _position;

        public Interpreter(string text) {
            this._text = text.AsSpan();
            this._position = 0;
        }

        private bool IsDigit() {
            char current = this._text[this._position];
            return current >= '0' && current <= '9';
        }

        private bool IsSpace() {
            return this._text[this._position] == ' ';
        }

        private void SkipSpaces() {
            while (IsSpace()) {
                this._position++;
            }
        }

        private bool HasNext() {
            return this._position < this._text.Length;
        }

        public Token GetNextToken() {
            if (this._position == this._text.Length) {
                return EOFToken.Instance;
            }
            
            SkipSpaces();

            if (IsDigit()) {
                int digitLength = 0;
                do {
                    this._position++;
                    digitLength++;
                } while (HasNext() && IsDigit());

                return new IntegerToken(int.Parse(this._text.Slice(this._position - digitLength, digitLength)));
            }
            
            if (Operators.TryGetValue(this._text[this._position], out var token)) {
                this._position++;
                return token;
            }

            throw new InvalidTokenException(this._text[this._position]);
        }
    }
}