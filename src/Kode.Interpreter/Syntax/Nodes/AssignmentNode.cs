namespace Kode {
    internal readonly struct AssignmentNode : ISyntaxTreeNode {
        public VarCharToken Name { get; }
        public ISyntaxTreeNode Value { get; }

        public AssignmentNode(VarCharToken name, ISyntaxTreeNode value) {
            Name = name;
            Value = value;
        }

        public override bool Equals(object obj) {
            return obj is AssignmentNode assignment && assignment.Name.Equals(Name) && assignment.Value.Equals(Value);
        }

        public override string ToString() {
            return $"ASSIGNMENT {Name} = {Value}";
        }
    }
}