using System;

namespace Kode {
    public sealed class UnexpectedTokenException : Exception {
        internal UnexpectedTokenException(Type expected, IToken actual) : base($"Expected {expected} but got {actual}") {
        }

        internal UnexpectedTokenException(IToken token, int position) : base($"{token} was not expected at position {position}") {
        }
    }
}