namespace Kode {
    internal static class Interpreter {
        public static unsafe int Evaluate(string input) {
            var lexer = new Lexer(input);
            int result = lexer.GetNextToken<IntegerToken>();
            result = RecursiveCalculation(lexer, &result);

            static int RecursiveCalculation(Lexer lexer, int* rollingTotal) {
                while (lexer.GetNextToken() is IOperatorToken op) {
                    IToken nextToken = lexer.GetNextToken();
                    switch (nextToken) {
                        case IntegerToken i:
                            *rollingTotal = op.Calculate(*rollingTotal, i);
                            break;
                    
                        case OpenParenthesesToken _:
                            int paranthesesResult = lexer.GetNextToken<IntegerToken>();
                            *rollingTotal = op.Calculate(*rollingTotal, RecursiveCalculation(lexer, &paranthesesResult));
                            break;
                        
                        case CloseParenthesesToken _:
                            return *rollingTotal;
                    
                        default:
                            throw new UnexpectedTokenException(op, nextToken);
                    }
                }

                return *rollingTotal;
            }
            
            lexer.GetNextToken<EOFToken>();

            return result;
        }
    }
}