namespace Skribble {
    internal readonly struct UnaryOperatorNode : ISyntaxTreeNode {
        public IUnaryOperatorToken UnaryOperator { get; }
        public ISyntaxTreeNode Node { get; }

        public UnaryOperatorNode(IUnaryOperatorToken unaryOperator, ISyntaxTreeNode node) {
            UnaryOperator = unaryOperator;
            Node = node;
        }

        public override string ToString() {
            return $"UNARY NODE {UnaryOperator} {Node}";
        }
    }
}