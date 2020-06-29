using System;

namespace Kode {
    public class ExpectedTokenException : Exception {
        public ExpectedTokenException(char token, int position) : base($"Expected token {token} ({(int) token}) at position {position}") {
        }
    }
}