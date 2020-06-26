namespace Kode
{
    public sealed class Interpreter {
        public int Execute(string input) {
            var state = new InterpreterState(input);
            IntegerToken left = (IntegerToken) state.MoveNext();
            OperatorToken op = (OperatorToken) state.MoveNext();
            IntegerToken right = (IntegerToken) state.MoveNext();
            

            return op.Calculate(left, right);
        }
    }
}