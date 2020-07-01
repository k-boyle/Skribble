using System;

namespace Kode {
    public class NumberParseFailedException : Exception {
        public NumberParseFailedException(string str, Type type) : base($"Failed to parse {str} as {type}") {
        }
    }
}