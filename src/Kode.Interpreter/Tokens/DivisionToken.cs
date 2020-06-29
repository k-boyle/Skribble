namespace Kode {
    public readonly struct DivisionToken : IOperatorToken {
        public static readonly DivisionToken Instance = new DivisionToken();
        
        public dynamic Calculate(dynamic left, dynamic right) {
            return unchecked(left / right);
        }

        public override string ToString() {
            return "OPERATOR DIVISION";
        }
    }
}