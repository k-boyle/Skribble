namespace Kode {
    public readonly struct LeftBitshiftToken : IBinaryOperatorToken {
        public static readonly LeftBitshiftToken Instance = new LeftBitshiftToken();

        public double Calculate(double left, double right) {
            return (int) left << (int) right;
        }

        public override string ToString() {
            return "OPERATOR LEFT BITSHIFT";
        }
    }
}