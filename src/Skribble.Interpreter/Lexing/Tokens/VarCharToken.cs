namespace Skribble {
    internal readonly struct VarCharToken : IToken {
        public string Value { get; }

        public VarCharToken(string value) {
            Value = value;
        }

        public override bool Equals(object obj) {
            return obj is VarCharToken vchar && Value == vchar.Value;
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        public override string ToString() {
            return $"VARCHAR {Value}";
        }
    }
}