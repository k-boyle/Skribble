namespace Kode {
    public readonly struct Interpreter {
        private readonly Parser _parser;

        private Interpreter(Parser parser) {
            this._parser = parser;
        }
        
        public static double Evaluate(string input) {
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);
            return interpreter.NavigateSyntaxTree() is { } res && parser.CurrentToken is EOFToken
                ? res
                : throw new UnexpectedTokenException(parser.CurrentToken, lexer.Position);
        }
        
        private double NavigateSyntaxTree() {
            ISyntaxTreeNode tree = this._parser.Parse();
            return Visit(tree);
        }

        private double Visit(ISyntaxTreeNode node) {
            return node switch {
                DoubleNode number       => VisitNumberNode(number),
                BinaryOperaterNode op   => VisitOperatorNode(op),
                UnaryOperatorNode unary => VisitUnaryNode(unary),
                _                       => throw new UnexpectedNodeException(node)
            };
        }
        
        private double VisitOperatorNode(BinaryOperaterNode node) {
            return node.BinaryOperator.Calculate(Visit(node.Left), Visit(node.Right));
        }
        
        private double VisitNumberNode(DoubleNode node) {
            return node.Number.Value;
        }
        
        private double VisitUnaryNode(UnaryOperatorNode node) {
            return node.UnaryOperator.Apply(Visit(node.Node));
        }
    }
}