namespace Kode {
    internal readonly struct MultiplicationToken : IOperatorToken {
        public static readonly MultiplicationToken Instance = new MultiplicationToken();

        public dynamic Calculate(dynamic left, dynamic right) {
            return unchecked(left * right);
        }

        public override string ToString() {
            return "OPERATOR MULTIPLICATION";
        }
    }
}