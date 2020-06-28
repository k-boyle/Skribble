namespace Kode {
    public interface IOperatorToken : IToken {
        int Calculate(int left, int right);
    }
}