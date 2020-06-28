namespace Kode {
    public class MultiplicationToken : OperatorToken {
        public static readonly MultiplicationToken Instance = new MultiplicationToken();

        private MultiplicationToken() {
        }

        public override int Calculate(int left, int right) {
            return left * right;
        }

        public override string ToString() {
            return "OPERATOR: *";
        }
    }
}