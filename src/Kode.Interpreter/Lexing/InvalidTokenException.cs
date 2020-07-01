using System;

namespace Kode {
    public sealed class InvalidTokenException : Exception {
        public InvalidTokenException(char token, int position) : base($"`{token}` ({(int) token}) is not expected at position {position}") {
        }
    }
}