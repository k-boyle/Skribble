namespace Kode {
    public readonly struct ModulusToken : IOperatorToken {
        public static readonly ModulusToken Instance = new ModulusToken();

        public dynamic Calculate(dynamic left, dynamic right) {
            return unchecked(left % right);
        }

        public override string ToString() {
            return "OPERATOR MODULUS";
        }
    }
}