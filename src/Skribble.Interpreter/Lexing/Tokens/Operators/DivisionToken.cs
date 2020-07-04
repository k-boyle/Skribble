namespace Skribble {
    internal readonly struct DivisionToken : IBinaryOperatorToken {
        public static readonly DivisionToken Instance = new DivisionToken();
        
        public double Calculate(double left, double right) {
            return left / right;
        }
        
        public override string ToString() {
            return "OPERATOR DIVISION";
        }
    }
}