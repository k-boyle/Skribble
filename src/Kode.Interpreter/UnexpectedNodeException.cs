using System;

namespace Kode {
    public class UnexpectedNodeException : Exception {
        public UnexpectedNodeException(ISyntaxTreeNode node) : base($"{node} was unexpected") {
        }
    }
}