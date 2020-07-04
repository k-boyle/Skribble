namespace Skribble {
    internal readonly struct BinaryOperaterNode : ISyntaxTreeNode {
        public ISyntaxTreeNode Left { get; }
        public IBinaryOperatorToken BinaryOperator { get; }
        public ISyntaxTreeNode Right { get; }

        public BinaryOperaterNode(ISyntaxTreeNode left, IBinaryOperatorToken binaryOperator, ISyntaxTreeNode right) {
            Left = left;
            BinaryOperator = binaryOperator;
            Right = right;
        }

        public override bool Equals(object obj) {
            if (obj is BinaryOperaterNode op) {
                return op.BinaryOperator.GetType() == BinaryOperator.GetType() && op.Left.Equals(Left) && op.Right.Equals(Right);
            }

            return false;
        }

        public override string ToString() {
            return $"BINARY NODE {Left} {BinaryOperator} {Right}";
        }
    }
}