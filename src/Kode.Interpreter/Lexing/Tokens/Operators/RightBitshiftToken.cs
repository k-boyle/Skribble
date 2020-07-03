namespace Kode {
    public readonly struct RightBitshiftToken : IBinaryOperatorToken {
        public static readonly RightBitshiftToken Instance = new RightBitshiftToken();

        public double Calculate(double left, double right) {
            return (int) left >> (int) right;
        }

        public override string ToString() {
            return "OPERATOR RIGHT BITSHIFT";
        }
    }
}