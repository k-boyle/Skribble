namespace Kode {
    internal readonly struct AdditionToken : IOperatorToken {
        public static readonly AdditionToken Instance = new AdditionToken();

        public long Calculate(long left, long right) {
            return unchecked(left + right);
        }

        public double Calculate(double left, double right) {
            return left + right;
        }

        public double Calculate(long left, double right) {
            return left + right;
        }

        public double Calculate(double left, long right) {
            return left + right;
        }

        public override string ToString() {
            return "OPERATOR ADDITION";
        }
    }
}