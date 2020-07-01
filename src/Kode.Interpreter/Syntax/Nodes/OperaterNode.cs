namespace Kode {
    public readonly struct OperaterNode : ISyntaxTreeNode {
        public ISyntaxTreeNode Left { get; }
        public IOperatorToken Operator { get; }
        public ISyntaxTreeNode Right { get; }

        public OperaterNode(ISyntaxTreeNode left, IOperatorToken @operator, ISyntaxTreeNode right) {
            Left = left;
            Operator = @operator;
            Right = right;
        }

        public override bool Equals(object obj) {
            if (obj is OperaterNode op) {
                return op.Operator.GetType() == Operator.GetType() && op.Left.Equals(Left) && op.Right.Equals(Right);
            }

            return false;
        }
    }
}