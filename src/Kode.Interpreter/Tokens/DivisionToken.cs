namespace Kode {
    public readonly struct DivisionToken : IOperatorToken {
        public static readonly DivisionToken Instance = new DivisionToken();
        
        public int Calculate(int left, int right) {
            return left / right;
        }

        public override string ToString() {
            return "OPERATOR DIVISION";
        }
    }
}