namespace Kode {
    internal interface IOperatorToken : IToken {
        dynamic Calculate(dynamic left, dynamic right);
    }
}