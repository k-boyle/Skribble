namespace Skribble {
    internal readonly struct FunctionToken : IToken {
        public static readonly FunctionToken Instance = new FunctionToken();

        public override string ToString() {
            return "FUNCTION";
        }
    }
}