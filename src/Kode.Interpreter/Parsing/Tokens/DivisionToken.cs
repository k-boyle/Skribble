namespace Kode {
    public class DivisionToken : OperatorToken {
        public static readonly DivisionToken Instance = new DivisionToken();
        
        private DivisionToken() {
        }
        
        public override int Calculate(int left, int right) {
            return left / right;
        }

        public override string ToString() {
            return "OPERATOR /";
        }
    }
}