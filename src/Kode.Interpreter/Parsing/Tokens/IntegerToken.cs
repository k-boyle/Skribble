namespace Kode {
    public sealed class IntegerToken : Token {
        public int Value { get; }

        public IntegerToken(int value) {
            Value = value;
        }

        public static implicit operator int(IntegerToken token) {
            return token.Value;
        }

        public override string ToString() {
            return $"INTEGER: {Value}";
        }
    }
}