namespace Kode {
    internal readonly struct ModulusToken : IBinaryOperatorToken {
        public static readonly ModulusToken Instance = new ModulusToken();

        public long Calculate(long left, long right) {
            return left % right;
        }

        public double Calculate(double left, double right) {
            return left % right;
        }

        public double Calculate(long left, double right) {
            return left % right;
        }

        public double Calculate(double left, long right) {
            return left % right;
        }

        public override string ToString() {
            return "OPERATOR MODULUS";
        }
    }
}