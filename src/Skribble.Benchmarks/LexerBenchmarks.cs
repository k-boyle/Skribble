using BenchmarkDotNet.Attributes;

namespace Skribble.Benchmark {
    [MemoryDiagnoser]
    public class LexerBenchmarks {
        [Params("123", "+", "3 + 5", "3 + (4 + 4)", "3 + (4 + (4 * 2))")]
        public string Input { get; set; }
        
        public void TokenBenchmark() {
            var lexer = new Lexer(Input);
            while (!(lexer.GetNextToken() is EOFToken)) ;
        }
    }
}