using BenchmarkDotNet.Running;
using System.Reflection;

namespace Skribble.Benchmark {
    class Program {
        static void Main(string[] args) {
            BenchmarkRunner.Run(Assembly.GetEntryAssembly());
        }
    }
}