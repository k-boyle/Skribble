using System.Collections.Generic;
using System;

namespace Kode
{
    internal ref struct InterpreterState {
        private static readonly Dictionary<char, OperatorToken> _operators = new Dictionary<char, OperatorToken> {
            ['+'] = AdditionToken.Instance,
            ['-'] = MinusToken.Instance
        };

        private readonly ReadOnlySpan<char> _text;
        private int _position;

        public InterpreterState(string text) {
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

        public bool HasNext() {
            return this._position < this._text.Length;
        }

        public Token MoveNext() {
            SkipSpaces();

            if (IsDigit()) {
                int digitLength = 0;
                do {
                    this._position++;
                    digitLength++;
                } while (HasNext() && IsDigit());

                return new IntegerToken(int.Parse(this._text.Slice(this._position - digitLength, digitLength)));
            } else if (_operators.TryGetValue(this._text[this._position], out var token)) {
                this._position++;
                return token;
            } else {
                return EOFToken.Instance;
            }
        }
    }
}