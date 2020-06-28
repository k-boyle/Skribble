namespace Kode {
    public readonly struct ModulusToken : IOperatorToken {
        public static readonly ModulusToken Instance = new ModulusToken();

        public int Calculate(int left, int right) {
            return left % right;
        }
    }
}