using System;
using System.Collections.Generic;

namespace Skribble {
    internal sealed class Lexer {
        private static readonly Dictionary<char, IToken> UniqueSingleCharacterTokenMap = new Dictionary<char, IToken> {
            ['+'] = PlusToken.Instance,
            ['-'] = MinusToken.Instance,
            ['/'] = DivisionToken.Instance,
            ['('] = OpenParenthesesToken.Instance,
            [')'] = CloseParenthesesToken.Instance,
            ['%'] = ModulusToken.Instance,
            ['='] = AssignmentToken.Instance,
            ['\n'] = EOLToken.Instance
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

        public IToken GetNextToken() {
            SkipSpaces();

            if (!this._currentChar.HasValue) {
                return EOFToken.Instance;
            }

            var current = this._currentChar.Value;
            int length;
            if (char.IsDigit(current)) {
                length = GetNumberLength();
                return new DoubleToken(ParseDouble(length));
            }

            if (UniqueSingleCharacterTokenMap.TryGetValue(current, out var nextToken)) {
                Increment();
                return nextToken;
            }

            if (TryGetVariableLengthToken(current, out nextToken)) {
                return nextToken;
            }

            if (char.IsLetter(current)) {
                length = GetStringLength();
                return new VarCharToken(this._text.Slice(this._position - length, length).ToString());
            }
                
            throw new InvalidTokenException(current, this._position);
        }

        private void SkipSpaces() {
            while (this._currentChar.HasValue && this._currentChar.Value == ' ') {
                Increment();
            }
        }

        private void Increment() {
            this._currentChar = this._position++ < this._text.Length - 1
                ? this._text.Span[this._position]
                : (char?) null;
        }
        
        private int GetNumberLength() {
            var digitLength = 0;
            do {
                Increment();
                digitLength++;
                
                if (this._currentChar == '.') {
                    digitLength++;
                    Increment();
                }
            } while (this._currentChar.HasValue && char.IsDigit(this._currentChar.Value));

            return digitLength;
        }

        private bool TryGetVariableLengthToken(char current, out IToken nextToken) {
            nextToken = null;
            var asSpan = this._text.Slice(this._position).Span;
            switch (current) {
                case '\r':
                    if (asSpan.Length == 1 || asSpan[1] != '\n') {
                        break;
                    }

                    Increment();
                    Increment(); 
                    nextToken = EOLToken.Instance;
                    return true;

                case '*':
                    if (asSpan.Length > 1 && asSpan[1] == '*') {
                        Increment();
                        Increment();
                        nextToken = PowerToken.Instance;
                        return true;
                    }

                    Increment();
                    nextToken = MultiplicationToken.Instance;
                    return true;

                case 'p':
                    if (asSpan.Length < 3 || asSpan[1] != 'o' || asSpan[2] != 'w') {
                        break;
                    }

                    Increment();
                    Increment();
                    Increment();
                    nextToken = PowerToken.Instance;
                    return true;

                case '<':
                    if (asSpan.Length == 1 || asSpan[1] != '<') {
                        break;
                    }

                    Increment();
                    Increment();
                    nextToken = LeftBitshiftToken.Instance;
                    return true;

                case '>':
                    if (asSpan.Length == 1 || asSpan[1] != '>') {
                        break;
                    }

                    Increment();
                    Increment();
                    nextToken = RightBitshiftToken.Instance;
                    return true;

                case 's':
                    if (asSpan.Length < 3 || asSpan[1] != 'i' || asSpan[2] != 'n') {
                        break;
                    }

                    Increment();
                    Increment();
                    Increment();
                    nextToken = SineToken.Instance;
                    return true;

                case 'c':
                    if (asSpan.Length < 3 || asSpan[1] != 'o' || asSpan[2] != 's') {
                        break;
                    }

                    Increment();
                    Increment();
                    Increment();
                    nextToken = CosineToken.Instance;
                    return true;

                case 't':
                    if (asSpan.Length < 3 || asSpan[1] != 'a' || asSpan[2] != 'n') {
                        break;
                    }

                    Increment();
                    Increment();
                    Increment();
                    nextToken = TangentToken.Instance;
                    return true;
                
                case 'f':
                    if (asSpan.Length < 2 || asSpan[1] != 'n') {
                        break;
                    }
                    
                    Increment();
                    Increment();
                    nextToken = FunctionToken.Instance;
                    return true;
                
                case 'r':
                    if (asSpan.Length < 3 || asSpan[1] != 'e' || asSpan[2] != 't') {
                        break;
                    }
                    
                    Increment();
                    Increment();
                    Increment();
                    nextToken = ReturnToken.Instance;
                    return true;
            }

            return false;
        }

        private int GetStringLength() {
            var stringLength = 0;
            do {
                Increment();
                stringLength++;
            } while (this._currentChar.HasValue 
                  && (char.IsLetter(this._currentChar.Value)
                  || char.IsDigit(this._currentChar.Value)));

            return stringLength;
        }

        private double ParseDouble(int length) {
            if (double.TryParse(this._text.Slice(this._position - length, length).Span, out var d)) {
                return d;
            }
            
            throw new NumberParseFailedException(this._text.Slice(this._position - length, length).ToString(), typeof(double));
        }
    }
}