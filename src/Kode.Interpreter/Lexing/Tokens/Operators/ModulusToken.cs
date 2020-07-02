namespace Kode {
    internal readonly struct ModulusToken : IBinaryOperatorToken {
        public static readonly ModulusToken Instance = new ModulusToken();

        public double Calculate(double left, double right) {
            return left % right;
        }

        public override string ToString() {
            return "OPERATOR MODULUS";
        }
    }
}