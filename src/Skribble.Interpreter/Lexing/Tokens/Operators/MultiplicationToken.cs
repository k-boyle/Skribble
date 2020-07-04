namespace Skribble {
    internal readonly struct MultiplicationToken : IBinaryOperatorToken {
        public static readonly MultiplicationToken Instance = new MultiplicationToken();

        public double Calculate(double left, double right) {
            return left * right;
        }
        
        public override string ToString() {
            return "OPERATOR MULTIPLICATION";
        }
    }
}