namespace Skribble {
    internal readonly struct EOFToken : IToken {
        public static readonly EOFToken Instance = new EOFToken();

        public override string ToString() {
            return "EOF";
        }
    }
}