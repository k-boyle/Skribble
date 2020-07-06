using System.Collections.Generic;

namespace Skribble {
    internal readonly struct FunctionNode : ISyntaxTreeNode {
        public VarCharToken Name { get; }
        public List<VarCharToken> Parameters { get; }
        public IEnumerable<ISyntaxTreeNode> Function { get; }

        public FunctionNode(VarCharToken name, List<VarCharToken> parameters, IEnumerable<ISyntaxTreeNode> function) {
            Name = name;
            Parameters = parameters;
            Function = function;
        }

        public override bool Equals(object obj) {
            return obj is FunctionNode other && other.Name.Value == Name.Value && other.Parameters.Count == Parameters.Count;
        }

        public override string ToString() {
            return $"FUNCTION {Name} PARAMETERS {string.Join(' ', Parameters)}";
        }
    }
}