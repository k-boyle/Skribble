namespace Kode {
    internal readonly struct NegativeToken : IBinaryOperatorToken, IUnaryOperatorToken {
        public static readonly NegativeToken Instance = new NegativeToken();
        
        public long Calculate(long left, long right) {
            return unchecked(left - right);
        }

        public double Calculate(double left, double right) {
            return left - right;
        }

        public double Calculate(long left, double right) {
            return left - right;
        }

        public double Calculate(double left, long right) {
            return left - right;
        }
        
        public long Apply(long number) {
            return unchecked(-number);
        }

        public double Apply(double number) {
            return -number;
        }
        
        public override string ToString() {
            return "TOKEN NEGATIVE";
        }
    }
}