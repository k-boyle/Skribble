namespace Kode {
    public readonly struct AdditionToken : IOperatorToken {
        public static readonly AdditionToken Instance = new AdditionToken();

        public int Calculate(int left, int right) {
            return left + right;
        }

        public override string ToString() {
            return "OPERATOR ADDITION";
        }
    }
}