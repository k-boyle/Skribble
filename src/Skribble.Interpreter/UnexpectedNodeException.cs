using System;

namespace Skribble {
    public class UnexpectedNodeException : Exception {
        internal UnexpectedNodeException(ISyntaxTreeNode node) : base($"{node} was unexpected") {
        }
    }
}