using System;

namespace Kode {
    class Program {
        static void Main(string[] args) {
            var interpreter = new Interpreter();
            Console.WriteLine(interpreter.Execute("    23 +          46"));
            Console.WriteLine(interpreter.Execute("23    - 46"));
        }
    }
}
