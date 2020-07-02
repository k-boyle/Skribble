﻿using System;
using System.Collections.Generic;

namespace Kode {
    internal sealed class Lexer {
        private static readonly Dictionary<char, IToken> TokenMap = new Dictionary<char, IToken> {
            ['+'] = PositiveToken.Instance,
            ['-'] = NegativeToken.Instance,
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
        
        private int GetNumberLength() {
            int digitLength = 0;
            bool alreadyFloating = false;
            do {
                Increment();
                digitLength++;
                
                if (this._currentChar == '.') {
                    alreadyFloating = !alreadyFloating 
                        ? true
                        : throw new InvalidTokenException(this._currentChar.Value, this._position);
                    digitLength++;
                    Increment();
                }
            } while (this._currentChar.HasValue && char.IsDigit(this._currentChar.Value));

            return digitLength;
        }

        private void SkipSpaces() {
            while (this._currentChar.HasValue && char.IsWhiteSpace(this._currentChar.Value)) {
                Increment();
            }
        }

        private void Increment() {
            this._currentChar = this._position++ < this._text.Length - 1
                ? this._text.Span[this._position]
                : (char?) null;
        }
        
        public IToken GetNextToken() {
            SkipSpaces();
            
            if (this._currentChar.HasValue) {
                char current = this._currentChar.Value;
                
                if (char.IsDigit(current)) {
                    var length = GetNumberLength();
                    return new DoubleToken(ParseDouble(length));
                }
            
                if (TokenMap.TryGetValue(current, out var token)) {
                    Increment();
                    return token;
                }

                throw new InvalidTokenException(current, this._position);
            }

            return EOFToken.Instance;
        }
        
        private double ParseDouble(int length) {
            if (double.TryParse(this._text.Slice(this._position - length, length).Span, out var d)) {
                return d;
            }
            
            throw new NumberParseFailedException(this._text.Slice(this._position - length, length).ToString(), typeof(double));
        }
    }
}