namespace Kode {
    internal static class Interpreter {
        public static int Evaluate(string input) {
            var lexer = new Lexer(input);
            int result = lexer.GetNextToken<IntegerToken>();

            while (lexer.GetNextToken() is OperatorToken op) {
                result = op.Calculate(result, lexer.GetNextToken<IntegerToken>());
            }
            
            lexer.GetNextToken<EOFToken>();

            return result;
        }
    }
}