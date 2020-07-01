using System;

namespace Kode {
    public class UnexpectedNodeException : Exception {
        internal UnexpectedNodeException(ISyntaxTreeNode node) : base($"{node} was unexpected") {
        }
    }
}