namespace Kode {
    public sealed class EOFToken : Token {
        public static readonly EOFToken Instance = new EOFToken();

        private EOFToken() {
        }

        public override string ToString() {
            return "EOF";
        }
    }
}