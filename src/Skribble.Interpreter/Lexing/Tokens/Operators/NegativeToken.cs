namespace Skribble {
    internal readonly struct NegativeToken : IBinaryOperatorToken, IUnaryOperatorToken {
        public static readonly NegativeToken Instance = new NegativeToken();
        
        public double Calculate(double left, double right) {
            return left - right;
        }
        
        public double Apply(double number) {
            return -number;
        }
        
        public override string ToString() {
            return "TOKEN NEGATIVE";
        }
    }
}