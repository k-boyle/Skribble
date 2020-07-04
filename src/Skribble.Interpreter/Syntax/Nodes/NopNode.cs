namespace Skribble {
    internal readonly struct NopNode : ISyntaxTreeNode {
        public static readonly NopNode Instance = new NopNode();

        public override string ToString() {
            return "NOP";
        }
    }
}