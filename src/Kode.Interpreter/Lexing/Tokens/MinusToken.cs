namespace Kode {
    internal readonly struct MinusToken : IOperatorToken {
        public static readonly MinusToken Instance = new MinusToken();
        
        public dynamic Calculate(dynamic left, dynamic right) {
            return unchecked(left - right);
        }

        public override string ToString() {
            return "OPERATOR MINUS";
        }
    }
}