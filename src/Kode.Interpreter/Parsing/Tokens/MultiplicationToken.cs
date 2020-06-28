namespace Kode {
    public readonly struct MultiplicationToken : IOperatorToken {
        public static readonly MultiplicationToken Instance = new MultiplicationToken();

        public int Calculate(int left, int right) {
            return left * right;
        }

        public override string ToString() {
            return "OPERATOR MULTIPLICATION";
        }
    }
}