namespace Kode {
    public readonly struct AdditionToken : IOperatorToken {
        public static readonly AdditionToken Instance = new AdditionToken();

        public dynamic Calculate(dynamic left, dynamic right) {
            return unchecked(left + right);
        }

        public override string ToString() {
            return "OPERATOR ADDITION";
        }
    }
}