using System;

namespace Kode {
    public sealed class UnexpectedTokenException : Exception {
        public UnexpectedTokenException(Type expected, Token actual) : base($"Expected {expected} but got {actual}") {
        }
    }
}