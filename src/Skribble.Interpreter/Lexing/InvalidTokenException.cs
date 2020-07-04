using System;

namespace Skribble {
    public sealed class InvalidTokenException : Exception {
        internal InvalidTokenException(char token, int position) : base($"`{token}` ({(int) token}) is not expected at position {position}") {
        }
    }
}