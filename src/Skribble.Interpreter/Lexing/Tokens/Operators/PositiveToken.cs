namespace Skribble {
    internal readonly struct PositiveToken : IBinaryOperatorToken, IUnaryOperatorToken {
        public static readonly PositiveToken Instance = new PositiveToken();

        public double Calculate(double left, double right) {
            return left + right;
        }
        
        public double Apply(double number) {
            return number;
        }

        public override string ToString() {
            return "TOKEN POSITIVE";
        }
    }
}