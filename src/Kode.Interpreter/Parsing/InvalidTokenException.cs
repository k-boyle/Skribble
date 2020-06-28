using System;

namespace Kode {
    public sealed class InvalidTokenException : Exception {
        public InvalidTokenException(char token) : base($"{token} is not registered as a valid token") {
        }
    }
}