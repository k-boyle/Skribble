namespace Kode {
    public sealed class AdditionToken : OperatorToken {
        public static readonly AdditionToken Instance = new AdditionToken();

        private AdditionToken() {
        }

        public override int Calculate(int left, int right) {
            return left + right;
        }

        public override string ToString() {
            return "OPERATOR: +";
        }
    }
}