namespace Kode {
    internal readonly struct PositiveToken : IBinaryOperatorToken, IUnaryOperatorToken {
        public static readonly PositiveToken Instance = new PositiveToken();

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

        public long Apply(long number) {
            return number;
        }

        public double Apply(double number) {
            return number;
        }

        public override string ToString() {
            return "TOKEN POSITIVE";
        }
    }
}