using BenchmarkDotNet.Running;
using System.Reflection;

namespace Kode.Benchmarks {
    class Program {
        static void Main(string[] args) {
            BenchmarkRunner.Run(Assembly.GetEntryAssembly());
        }
    }
}