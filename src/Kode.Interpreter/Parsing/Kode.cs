namespace Kode {
    public static class Kode {
        public static int Execute(string input) {
            var interpreter = new Interpreter(input);
            return interpreter.Evaluate();
        }
    }
}