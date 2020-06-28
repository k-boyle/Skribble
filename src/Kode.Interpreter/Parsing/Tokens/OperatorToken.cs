namespace Kode {
    public abstract class OperatorToken : Token {
        public abstract int Calculate(int left, int right);
    }
}