namespace Kode {
    internal readonly struct NumberNode : ISyntaxTreeNode {
        public INumberToken Number { get; }

        public NumberNode(INumberToken number) {
            Number = number;
        }
        
        public NumberNode(double d) {
            Number = new DoubleToken(d);
        }
        
        public NumberNode(long l) {
            Number = new LongToken(l);
        }

        public override bool Equals(object obj) {
            if (obj is NumberNode number) {
                return number.Number.Value.Equals(Number.Value);
            }

            return false;
        }
    }
}