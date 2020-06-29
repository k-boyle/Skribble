namespace Kode {
    public readonly struct LongToken : INumberToken {
        public dynamic Value { get; }

        public LongToken(long value) {
            Value = value;
        }

        public override string ToString() {
            return $"LONG {Value}";
        }
    }
}