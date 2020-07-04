namespace Skribble {
    internal readonly struct OpenParenthesesToken : IToken {
        public static readonly OpenParenthesesToken Instance = new OpenParenthesesToken();

        public override string ToString() {
            return "OPEN PARANTHESES";
        }
    }
}