namespace Kode {
    internal interface IBinaryOperatorToken : IToken {
        double Calculate(double left, double right);
    }
}