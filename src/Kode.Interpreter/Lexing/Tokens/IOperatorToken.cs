namespace Kode {
    internal interface IOperatorToken : IToken {
        long Calculate(long left, long right);
        double Calculate(double left, double right);
        double Calculate(double left, long right);
        double Calculate(long left, double right);
    }
}