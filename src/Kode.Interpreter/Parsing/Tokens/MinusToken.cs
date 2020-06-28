namespace Kode {
    public readonly struct MinusToken : IOperatorToken {
        public static readonly MinusToken Instance = new MinusToken();
        
        public int Calculate(int left, int right) {
            return left - right;
        }

        public override string ToString() {
            return "OPERATOR MINUS";
        }
    }
}