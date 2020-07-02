namespace Kode {
    internal readonly struct DoubleNode : ISyntaxTreeNode {
        public DoubleToken Number { get; }

        public DoubleNode(DoubleToken number) {
            Number = number;
        }
        
        public DoubleNode(double d) {
            Number = new DoubleToken(d);
        }

        public override bool Equals(object obj) {
            if (obj is DoubleNode number) {
                return number.Number.Value.Equals(Number.Value);
            }

            return false;
        }

        public override string ToString() {
            return $"NUMER NODE {Number}";
        }
    }
}