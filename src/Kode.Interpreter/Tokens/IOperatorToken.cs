namespace Kode {
    public interface IOperatorToken : IToken {
        dynamic Calculate(dynamic left, dynamic right);
    }
}