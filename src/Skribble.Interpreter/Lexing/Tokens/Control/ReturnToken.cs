namespace Skribble {
    internal readonly struct ReturnToken : IToken {
        public static readonly ReturnToken Instance = new ReturnToken();

        public override string ToString() {
            return "RETURN";
        }
    }
}