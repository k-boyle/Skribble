namespace Kode {
    public static class Interpreter {
        public static object Evaluate(string input) {
            var lexer = new Lexer(input);
            dynamic result = RecursiveCalculation(false);

            dynamic RecursiveCalculation(bool brackets) {
                IToken start = lexer.GetNextToken();
                dynamic total = start switch {
                    OpenParenthesesToken _ => RecursiveCalculation(true),
                    INumberToken number    => number.Value,
                    _                      => throw new UnexpectedTokenException(start, lexer.Position)
                };

                IToken currentToken;
                while ((currentToken = lexer.GetNextToken()) is IOperatorToken op) {
                    IToken nextToken = lexer.GetNextToken();
                    total = nextToken switch {
                        INumberToken number    => op.Calculate(total, number.Value),
                        OpenParenthesesToken _ => op.Calculate(total, RecursiveCalculation(true)),
                        _                      => throw new UnexpectedTokenException(op, nextToken)
                    };
                }

                if (!brackets && currentToken is CloseParenthesesToken) {
                    throw new UnexpectedTokenException(currentToken, lexer.Position);
                }
                
                if (brackets && !(currentToken is CloseParenthesesToken)) {
                    throw new ExpectedTokenException(')', lexer.Position);
                }
                
                return total;
            }
            
            lexer.GetNextToken<EOFToken>();

            return result;
        }
    }
}