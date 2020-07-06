using System.Collections.Generic;

/*
*     fn square a
*     ret a ** 2
*
*     numbers = 2 to 10
*     squares = numbers mapped square
*     ret squares sum* 
*/

/*
 *    myList = []
 *    for 1 to 10
 *    myList@i = i ** i
 *    end
 *
 *    ret myList 
 */

/*
 *     numbers = 2 to 10
 *     {
 *          numbers = numbers.Select(i => i  *= 10).Where(i => i > 30);
 *     }
 *
 *     ret numbers sum
 */

namespace Skribble {
    public readonly struct Interpreter {
        private readonly Parser _parser;
        private readonly Dictionary<VarCharToken, double> _globalScopedVariables;
        private readonly Dictionary<VarCharToken, FunctionNode> _methods;

        private Interpreter(Parser parser) {
            this._parser = parser;
            this._globalScopedVariables = new Dictionary<VarCharToken, double>();
            this._methods = new Dictionary<VarCharToken, FunctionNode>();
        }
        
        public static object Evaluate(string input) {
            //todo i should be consistent with var vs types
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);
            var res = interpreter.NavigateSyntaxTree();
            return parser.CurrentToken is EOFToken
                ? res
                : throw new UnexpectedTokenException(parser.CurrentToken, lexer.Position);
        }
        
        private double? NavigateSyntaxTree() {
            RootNode tree = this._parser.Parse();
            double? last = null;
            foreach (var node in tree.ChildNodes) {
                last = Visit(node);
            }
            return last;
        }
        
        private double? Visit(ISyntaxTreeNode node) {
            return node switch {
                DoubleNode number       => VisitNumberNode(number),
                BinaryOperaterNode op   => VisitBinaryOperatorNode(op),
                UnaryOperatorNode unary => VisitUnaryNode(unary),
                AssignmentNode ass      => VisitAssignmentNode(ass),
                VariableNode var        => VisitVariableNode(var),
                FunctionNode func       => VisitFunctionNode(func),
                _                       => throw new UnexpectedNodeException(node)
            };
        }

        private double? VisitNumberNode(DoubleNode node) {
            return node.Number.Value;
        }

        private double? VisitBinaryOperatorNode(BinaryOperaterNode node) {
            return node.BinaryOperator.Calculate(Visit(node.Left)!.Value, Visit(node.Right)!.Value);
        }

        private double? VisitUnaryNode(UnaryOperatorNode node) {
            return node.UnaryOperator.Apply(Visit(node.Node)!.Value);
        }
        
        private double? VisitAssignmentNode(AssignmentNode node) {
            this._globalScopedVariables[node.Name] = Visit(node.Value)!.Value;
            return null;
        }
        
        private double? VisitVariableNode(VariableNode node) {
            if (this._globalScopedVariables.TryGetValue(node.Name, out var value)) {
                return value;
            }
            
            throw new UnknownVariableException(node.Name.Value);
        }
        
        private double? VisitFunctionNode(FunctionNode node) {
            this._methods[node.Name] = node;
            return null;
        }
    }
}