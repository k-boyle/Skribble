namespace Skribble {
    internal readonly struct MinusToken : IBinaryOperatorToken, IUnaryOperatorToken {
        public static readonly MinusToken Instance = new MinusToken();
        
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