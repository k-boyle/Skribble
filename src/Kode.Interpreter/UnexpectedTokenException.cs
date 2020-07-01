using System;

namespace Kode {
    public sealed class UnexpectedTokenException : Exception {
        public UnexpectedTokenException(Type expected, IToken actual) : base($"Expected {expected} but got {actual}") {
        }

        public UnexpectedTokenException(IToken token, int position) : base($"{token} was not expected at position {position}") {
        }
    }
}