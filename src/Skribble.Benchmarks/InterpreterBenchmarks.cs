﻿using BenchmarkDotNet.Attributes;

namespace Skribble.Benchmark {
    [MemoryDiagnoser]
    public class InterpreterBenchmarks {
        [Params(
            "9",
            "3+4",
            "    1    +     4    ",
            "10 + 5 - 3 * 2",
            "10 + (3 + 4)",
            "10 + (3 + (4 * 2))",
            "9223372036854775807 + 1",
            "--2",
            "5 * -2",
            "2 ** 2",
            "2 pow 2")]
        public string Calculation { get; set; }

        [Benchmark]
        public void CalculationBenchmark() {
            Interpreter.Evaluate(Calculation);
        }
    }
}