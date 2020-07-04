namespace Skribble {
    internal readonly struct VariableNode : ISyntaxTreeNode {
        public VarCharToken Name { get; }

        public VariableNode(VarCharToken name) {
            Name = name;
        }

        public override string ToString() {
            return $"VARIABLE {Name}";
        }
    }
}