namespace Kode {
    public sealed class MinusToken : OperatorToken {
        public static readonly MinusToken Instance = new MinusToken();
        
        private MinusToken() {
        }

        public override int Calculate(int left, int right) {
            return left - right;
        }

        public override string ToString() {
            return "OPERATOR: -";
        }
    }
}