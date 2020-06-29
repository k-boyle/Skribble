namespace Kode {
    public readonly struct DoubleToken : INumberToken {
        public dynamic Value { get; }

        public DoubleToken(double value) {
            Value = value;
        }

        public override string ToString() {
            return $"DOUBLE {Value}";
        }
    }
}