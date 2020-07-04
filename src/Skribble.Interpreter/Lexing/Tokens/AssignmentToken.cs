namespace Skribble {
    internal readonly struct AssignmentToken : IToken {
        public static readonly AssignmentToken Instance = new AssignmentToken();

        public override string ToString() {
            return "ASSIGNMENT";
        }
    }
}