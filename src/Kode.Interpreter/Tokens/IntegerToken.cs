namespace Kode {
    public readonly struct IntegerToken : IToken {
        private readonly int _value;

        public IntegerToken(int value) {
            this._value = value;
        }

        public static implicit operator int(IntegerToken token) {
            return token._value;
        }

        public override string ToString() {
            return $"INTEGER: {this._value}";
        }
    }
}