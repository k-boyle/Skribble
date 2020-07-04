using System;

namespace Skribble {
    public class NumberParseFailedException : Exception {
        internal NumberParseFailedException(string str, Type type) : base($"Failed to parse {str} as {type}") {
        }
    }
}