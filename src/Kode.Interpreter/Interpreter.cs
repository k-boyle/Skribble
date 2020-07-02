namespace Kode {
    public readonly struct Interpreter {
        private readonly Parser _parser;

        private Interpreter(Parser parser) {
            this._parser = parser;
        }
        
        public static object Evaluate(string input) {
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);
            return interpreter.NavigateSyntaxTree() is { } res && parser.CurrentToken is EOFToken
                ? res
                : throw new UnexpectedTokenException(parser.CurrentToken, lexer.Position);
        }
        
        private object NavigateSyntaxTree() {
            ISyntaxTreeNode tree = this._parser.Parse();
            return Visit(tree);
        }
        
        private dynamic Visit(ISyntaxTreeNode node) {
            return node switch {
                NumberNode number => VisitNumberNode(number),
                OperaterNode op   => VisitOperatorNode(op),
                _                 => throw new UnexpectedNodeException(node)
            };
        }
        
        private object VisitOperatorNode(OperaterNode node) {
            return node.Operator.Calculate(Visit(node.Left), Visit(node.Right));
        }
        
        private object VisitNumberNode(NumberNode node) {
            return node.Number.Value;
        }
    }
}