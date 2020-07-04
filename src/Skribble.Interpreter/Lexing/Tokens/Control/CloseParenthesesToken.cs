namespace Skribble {
    internal readonly struct CloseParenthesesToken : IToken {
        public static readonly CloseParenthesesToken Instance = new CloseParenthesesToken();

        public override string ToString() {
            return "CLOSE PARANTHESES";
        }
    }
}