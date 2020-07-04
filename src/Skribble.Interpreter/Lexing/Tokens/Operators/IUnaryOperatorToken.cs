namespace Skribble {
    internal interface IUnaryOperatorToken : IToken {
        double Apply(double number);
    }
}