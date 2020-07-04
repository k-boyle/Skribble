namespace Skribble {
    internal readonly struct PlusToken : IBinaryOperatorToken, IUnaryOperatorToken {
        public static readonly PlusToken Instance = new PlusToken();

        public double Calculate(double left, double right) {
            return left + right;
        }
        
        public double Apply(double number) {
            return number;
        }

        public override string ToString() {
            return "TOKEN PLUS";
        }
    }
}