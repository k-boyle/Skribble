namespace Kode {
    internal interface IUnaryOperatorToken : IToken {
        double Apply(double number);
    }
}