namespace Kode {
    public static class Kode {
        public static int Execute(string input) {
            var state = new Interpreter(input);
            IntegerToken left = (IntegerToken) state.GetNextToken();
            OperatorToken op = (OperatorToken) state.GetNextToken();
            IntegerToken right = (IntegerToken) state.GetNextToken();

            return op.Calculate(left, right);
        }
    }
}