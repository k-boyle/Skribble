namespace Kode {
    internal readonly struct DoubleToken : IToken {
        public double Value { get; }

        public DoubleToken(double value) {
            Value = value;
        }

        public override string ToString() {
            return $"DOUBLE {Value}";
        }
    }
}