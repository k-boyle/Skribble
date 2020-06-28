using System;

namespace Kode {
    public sealed class UnexpectedTokenException : Exception {
        public UnexpectedTokenException(Type expected, IToken actual) : base($"Expected {expected} but got {actual}") {
        }

        public UnexpectedTokenException(IToken before, IToken after) : base($"{after} was not expected after {before}") {
        }
    }
}