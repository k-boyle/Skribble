namespace Skribble {
    internal readonly struct EOLToken : IToken {
        public static readonly EOLToken Instance = new EOLToken();

        public override string ToString() {
            return "EOL";
        }
    }
}